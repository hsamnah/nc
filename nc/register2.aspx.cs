using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class register2 : Page
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private string user
        {
            get { return (Request.QueryString["un"] == "") ? string.Empty : Request.QueryString["un"]; }
        }

        protected string _cc;

        private string concode
        {
            get { return _cc; }
            set { _cc = value; }
        }

        private bool isUnique(string un)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("checkUN_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@un", un);
            pResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int, 4));
            pResult.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            conn.Close();
            if (Convert.ToInt32(pResult.Value.ToString()) <= 0)
            {
                pResult.Value = null;
                return true;
            }
            else
            {
                pResult.Value = null;
                return false;
            }
        }

        private bool isConfirmed(string un)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("isConfirmed_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@un", un);
            pResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Bit));
            pResult.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            conn.Close();
            if (Convert.ToInt16(pResult.Value.ToString()) == 1)
            {
                pResult.Value = null;
                return true;
            }
            else
            {
                pResult.Value = null;
                return false;
            }
        }

        private int regIndex
        {
            get { return (string.IsNullOrEmpty(Request.QueryString["in"])) ? 0 : Convert.ToInt32(Request.QueryString["in"]); }
        }

        private int tmpID
        {
            get { return Convert.ToInt32(Request.QueryString["dent"]); }
        }

        private string vCode
        {
            get { return (string.IsNullOrEmpty(Request.QueryString["gc"])) ? string.Empty : Request.QueryString["gc"]; }
        }

        private static Random rand = new Random();
        private int intRanResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                regWizard.ActiveStepIndex = regIndex;
                get_CountryList();

                venueList.Items.Add(new ListItem("Dance", "Dance"));
                venueList.Items.Add(new ListItem("Rave", "Rave"));
                venueList.Items.Add(new ListItem("Live band/Cover band", "Live band/Cover band"));
                venueList.Items.Add(new ListItem("Karioki", "Karioki"));
                venueList.Items.Add(new ListItem("Lounge", "Lounge"));
                venueList.Items.Add(new ListItem("Sports Bar", "Sports Bar"));
                venueList.Items.Add(new ListItem("Pub/Tavern", "Pub/Tavern"));
                venueList.Items.Add(new ListItem("Other", "Other"));

                musicGenre.Items.Add(new ListItem("Alternative", "Alternative"));
                musicGenre.Items.Add(new ListItem("Blues", "Blues"));
                musicGenre.Items.Add(new ListItem("Country Music", "Country Music"));
                musicGenre.Items.Add(new ListItem("Cultural Music", "Cultural Music"));
                musicGenre.Items.Add(new ListItem("Folk Music", "Folk Music"));
                musicGenre.Items.Add(new ListItem("Hip-hop", "hip-hop"));
                musicGenre.Items.Add(new ListItem("House Music", "House Music"));
                musicGenre.Items.Add(new ListItem("Jazz", "Jazz"));
                musicGenre.Items.Add(new ListItem("Pop Chart Music", "Pop Chart Music"));
                musicGenre.Items.Add(new ListItem("Rap", "Rap"));
                musicGenre.Items.Add(new ListItem("Rock & Roll", "Rock & Roll"));
                musicGenre.Items.Add(new ListItem("Techno", "Techno"));
                musicGenre.Items.Add(new ListItem("Other", "Other"));

                share.Items.Add(new ListItem("Your age", "Your age"));
                share.Items.Add(new ListItem("Contact Information", "Contact Information"));
                share.Items.Add(new ListItem("Description of yourself", "Description of yourself"));
                share.Items.Add(new ListItem("Venues you've attended", "Venues you've attended"));
                share.Items.Add(new ListItem("Venues you plan to visit.", "Venues you plan to visit"));
                share.Items.Add(new ListItem("Your avatar", "Your avatar"));
                share.Items.Add(new ListItem("All of the above.", "All of the above"));
                share.Items.Add(new ListItem("None of the above", "None of the above"));
            }
        }

        private void get_CountryList()
        {
            country.Items.Insert(0, new ListItem("---Country---"));
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand countryCmd = new SqlCommand("get_Country_sp", conn);
            countryCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader countryDR = countryCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (countryDR.Read())
            {
                ListItem li = new ListItem(countryDR["CountryName"].ToString() + " " + countryDR["CountryCode"].ToString(), countryDR["CountryCode"].ToString());
                country.Items.Add(li);
            }
            countryDR.Close();
        }

        protected int intResult = -1;
        protected string strPWD;
        protected string conCode;

        protected void getSub_Click(object sender, EventArgs e)
        {
            subdivision.Items.Clear();
            string cc = country.SelectedValue;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand subCmd = new SqlCommand("get_subdivision_sp", conn);
            subCmd.CommandType = CommandType.StoredProcedure;
            subCmd.Parameters.AddWithValue("@cc", cc);
            SqlDataReader subDR = subCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (subDR.Read())
            {
                if (!string.IsNullOrEmpty(subDR["Subdivision"].ToString()))
                {
                    ListItem li = new ListItem(subDR["Subdivision"].ToString(), subDR["Subdivision"].ToString());
                    subdivision.Items.Add(li);
                }
                else
                {
                    // get all the cities associated with the country.
                    getCity(cc, "Country");
                }
                intResult += 1;
            }
            // This produces cities where the country is not subdivided into provinces or states.
            if (intResult > 0)
            {
                subdivision.Visible = true;
                subdivision.Items.Insert(0, new ListItem("---Province/State---"));
            }
            else
            {
                subdivision.Visible = false;
                getCity(cc, "Country");
            }
            subDR.Close();
        }

        protected void getCity_Click(object sender, EventArgs e)
        {
            getCity(subdivision.SelectedValue, "subdivision");
        }

        protected void getCity(string Identifier, string Region)
        {
            City.Items.Clear();
            SqlConnection conn = new SqlConnection(con);
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            SqlCommand cmd = new SqlCommand("get_city_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item", Identifier);
            cmd.Parameters.AddWithValue("@From", Region);
            SqlDataReader cityDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (cityDR.Read())
            {
                ListItem li = new ListItem(cityDR["City_Town"].ToString(), cityDR["recordID"].ToString());
                City.Items.Add(li);
            }
            cityDR.Close();
            City.Items.Insert(0, new ListItem("---City---"));
            City.Visible = true;
        }

        protected void ChangeStep_Change(object sender, EventArgs e)
        {
            if (regWizard.ActiveStep.StepType == WizardStepType.Start)
            {
                // Register
            }
            else if (regWizard.ActiveStep.StepType == WizardStepType.Step)
            {
                switch (regWizard.ActiveStepIndex)
                {
                    case 1:
                        // Create Account
                        break;

                    case 2:
                        // Email Confirmation
                        break;

                    case 3:
                        // Contact information
                        if (isConfirmed(UN.Text))
                        {
                            regWizard.ActiveStepIndex = 4;
                        }
                        else
                        {
                            regWizard.ActiveStepIndex = 2;
                        }
                        break;

                    case 4:

                        break;

                    case 5:
                        // Preferances
                        break;

                    case 6:
                        // Avatar
                        break;
                }
            }
        }

        private SqlParameter pResult;
        private int intResult2;

        protected void Confirmation_Activate(object sender, EventArgs e)
        {
            if (regIndex == 0)
            {
                // This code has to be sent by email to the new user.
                string genCode = getRandomCode().ToString();

                string strdDate = Month.Items[Month.SelectedIndex].Value + "-" + Day.Items[Day.SelectedIndex].Value + "-" + Year.Items[Year.SelectedIndex].Value;
                string strSex = Sex.Items[Sex.SelectedIndex].Value;
                string strlookinfor = lookinfor.Items[lookinfor.SelectedIndex].Value;
                string strStatus = status.Items[lookinfor.SelectedIndex].Value;
                string strrel = rel.Items[rel.SelectedIndex].Value;
                string strUN = UN.Text;
                string strEmail = Email.Text;

                string strPWD = (string)Session["primaryPWD"];
                string strCPWD = (string)Session["confirmPWD"];

                string strSQ = secQuest.Text;
                string strSA = secAns.Text;
                string strSH = secHint.Text;

                SqlParameter uParam = new SqlParameter();
                SqlConnection conn = new SqlConnection(con);
                conn.Open();

                SqlCommand tmpCmd = new SqlCommand("createTMP_User_sp", conn);
                tmpCmd.CommandType = CommandType.StoredProcedure;

                tmpCmd.Parameters.AddWithValue("@bday", strdDate);
                tmpCmd.Parameters.AddWithValue("@sex", strSex);
                tmpCmd.Parameters.AddWithValue("@look", strlookinfor);
                tmpCmd.Parameters.AddWithValue("@stat", strStatus);
                tmpCmd.Parameters.AddWithValue("@rel", strrel);
                tmpCmd.Parameters.AddWithValue("@un", strUN);
                tmpCmd.Parameters.AddWithValue("@email", strEmail);
                tmpCmd.Parameters.AddWithValue("@pwd", strPWD);
                tmpCmd.Parameters.AddWithValue("@cpwd", strCPWD);

                tmpCmd.Parameters.AddWithValue("@sq", strSQ);
                tmpCmd.Parameters.AddWithValue("@sa", strSA);
                tmpCmd.Parameters.AddWithValue("@sh", strSH);
                tmpCmd.Parameters.AddWithValue("@confirmationCode", genCode);

                pResult = tmpCmd.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
                pResult.Direction = ParameterDirection.ReturnValue;

                tmpCmd.ExecuteNonQuery();
                conn.Close();
                intResult2 = Convert.ToInt32(pResult.Value.ToString());

                if (intResult2 > 0)
                {
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("register@i-underground.com");
                    mail.To.Add(strEmail);
                    mail.Subject = "The Underground validation";

                    StringBuilder regBody = new StringBuilder();
                    regBody.Append(@"Welcome to The Underground Network.  The future of evening entertainment is upon us." + "\n\n");
                    regBody.Append("\n");
                    regBody.Append(@"Please click on the link below to validate your registration with i-Underground.com:" + "\n");
                    regBody.Append(@"http://www.i-underground.com/register.aspx?in=2&dent=" + intResult2 + "&gc=" + genCode + "\n");
                    regBody.Append("\n");
                    regBody.Append(@"Regards," + "\n");
                    regBody.Append(@"Registration, The Underground-" + "\n\n");
                    regBody.Append(@"If ths email has been sent to your email address by mistake or this email address does not belong to you" + "\n" + "please disregard and accept ur apologies for any inconvenience." + "\n");

                    mail.Body = regBody.ToString();

                    SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                    NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                    smtp.Credentials = cred;
                    smtp.Send(mail);

                    test.Visible = true;
                    test.Text = "<br />We have sent you an email with a random confirmation code to the following email address " + strEmail + " please retrieve your confirmation code and type it into the text box below.<br /><br />Please note, if you do not find the code in your inbox try your spam folder.";
                    Session.Abandon();
                }
                else
                {
                    test.Text = "The password fields do not match.  Please return to the previous screen and try again.";
                    test.Visible = true;
                }
            }
        }

        private SqlParameter conParam;
        private SqlParameter unParam;
        private int conIntResult;

        protected void Send_Data(object sender, EventArgs e)
        {
            // The confirmation code will have to be compared to an email rather then a code generated and displayed on the page.
            // I have to retrieve the code from the temp table in the database and compare it with the code typed in the textbox.
            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            SqlCommand confcmd = new SqlCommand("get_vCode_sp", conn);
            confcmd.CommandType = CommandType.StoredProcedure;

            confcmd.Parameters.AddWithValue("@tID", tmpID);

            conParam = confcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            conParam.Direction = ParameterDirection.ReturnValue;

            confcmd.ExecuteNonQuery();
            conn.Close();
            conIntResult = Convert.ToInt32(conParam.Value.ToString());

            if (conIntResult == Convert.ToInt32(vCode))
            {
                // create a stored procedure to transfer temp to primary and clear the temp table.
                conn.Open();
                SqlCommand t2pcmd = new SqlCommand("transfer_tmp2primary_sp", conn);
                t2pcmd.CommandType = CommandType.StoredProcedure;
                t2pcmd.Parameters.AddWithValue("@tmpID", tmpID);

                pResult = t2pcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                pResult.Direction = ParameterDirection.ReturnValue;
                unParam = t2pcmd.Parameters.Add(new SqlParameter("@uName", SqlDbType.VarChar, 255));
                unParam.Direction = ParameterDirection.Output;

                t2pcmd.ExecuteNonQuery();
                conn.Close();
                string strUN = unParam.Value.ToString();

                if (Convert.ToInt32(pResult.Value.ToString()) > 0)
                {
                    newUserID.Value = pResult.Value.ToString();
                    string uPath = "UserItems/userUploads/" + strUN + pResult.Value.ToString();
                    if (!Directory.Exists(Server.MapPath(uPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(uPath));
                    }
                    newUser nu = new newUser
                    {
                        uID = Convert.ToInt32(pResult.Value.ToString()),
                        userName = strUN
                    };

                    // Insert username and identifier to xml file
                    var content = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("xmlData/UserList.json")))
                    {
                        content = reader.ReadToEnd();
                        reader.Close();
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append(",{\"uID\":\"" + nu.uID + "\",\"UserName\":\"" + nu.userName.ToString() + "\"}" + "\n");
                    sb.Append("]" + "\n");
                    content = Regex.Replace(content, "\n]", sb.ToString());

                    using (StreamWriter writer = new StreamWriter(Server.MapPath("xmlData/UserList.json")))
                    {
                        writer.Write(content);
                        writer.Close();
                    }
                }
                else
                {
                    regWizard.ActiveStepIndex = 1;
                    pcomp.Visible = true;
                    pcomp.Text = "<br />Password fields do not match.  Please try again.";
                }
            }
            else
            {
                regWizard.ActiveStepIndex = 2;
            }
        }

        protected void isUnique_Click(object sender, EventArgs e)
        {
            if (!isUnique(UN.Text))
            {
                UN.Text = "";
                errun.Visible = true;
                errun.Text = "The username was taken.";
            }
            else
            {
                UN.Text = UN.Text;
                errun.Visible = false;
                errun.Text = "";
            }
        }

        protected int getRandomCode()
        {
            for (int i = 0; i < 1; i++)
            {
                intRanResult = rand.Next(1000, 9999);
            }
            return intRanResult;
        }

        private string strCity;
        private string strSub;

        protected void Contact_Deactivate(object sender, EventArgs e)
        {
            string strUID = newUserID.Value;
            string strFN = FirstName.Text;
            string strLN = LastName.Text;
            string strSA1 = Add1.Text;
            string strSA2 = Add2.Text;
            string strCountry = country.SelectedValue;
            string strSub = subdivision.SelectedValue;
            string strCity = City.SelectedItem.Text;
            int locID = Convert.ToInt32(City.SelectedValue);
            string strpz = pz.Text;
            string strmob = mobile.Text;
            string strhome = home.Text;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insertUserDetails_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uid", strUID);
            cmd.Parameters.AddWithValue("@fn", strFN);
            cmd.Parameters.AddWithValue("@ln", strLN);
            cmd.Parameters.AddWithValue("@sa1", strSA1);
            cmd.Parameters.AddWithValue("@sa2", strSA2);
            cmd.Parameters.AddWithValue("@city", strCity);
            cmd.Parameters.AddWithValue("@subdiv", strSub);
            cmd.Parameters.AddWithValue("@country", strCountry);
            cmd.Parameters.AddWithValue("@pz", strpz);
            cmd.Parameters.AddWithValue("@mob", strmob);
            cmd.Parameters.AddWithValue("@home", strhome);
            cmd.Parameters.AddWithValue("@locID", locID);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void about_Deactivate(object sender, EventArgs e)
        {
            string venueList = venVisit.Text;
            string[] venList;
            char[] seperator = new char[] { ',' };
            venList = venueList.Split(seperator);

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("udesc_sp", conn);
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@uid", newUserID.Value);
            cmd1.Parameters.AddWithValue("@userDesc", desc.Text);
            cmd1.Parameters.AddWithValue("@bExp", bExp.Text);
            cmd1.Parameters.AddWithValue("@wExp", wExp.Text);

            cmd1.ExecuteNonQuery();

            foreach (string str in venList)
            {
                SqlCommand cmd2 = new SqlCommand("userVenueList_sp", conn);
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd2.Parameters.AddWithValue("@uid", newUserID.Value);
                cmd2.Parameters.AddWithValue("@venue", str.ToString());

                cmd2.ExecuteNonQuery();
            }
            conn.Close();
        }

        protected void Prefs_Deactivate(object sender, EventArgs e)
        {
            string[] artistList;
            char[] seperator2 = new char[] { ',' };
            string artist = artists.Text;
            artistList = artist.Split(seperator2);

            SqlConnection conn = new SqlConnection(con);
            foreach (ListItem vl in venueList.Items)
            {
                if (vl.Selected)
                {
                    conn.Open();
                    SqlCommand cmd0 = new SqlCommand("uVenueType_sp", conn);
                    cmd0.CommandType = CommandType.StoredProcedure;

                    cmd0.Parameters.AddWithValue("@uid", newUserID.Value);
                    cmd0.Parameters.AddWithValue("@vType", vl.Value);

                    cmd0.ExecuteNonQuery();
                    conn.Close();
                }
            }

            foreach (string strA in artistList)
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("faveArtist_sp", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@uid", newUserID.Value);
                cmd1.Parameters.AddWithValue("@Art", strA);

                cmd1.ExecuteNonQuery();
                conn.Close();
            }

            foreach (ListItem mg in musicGenre.Items)
            {
                if (mg.Selected)
                {
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("musicGenre_sp", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@uid", newUserID.Value);
                    cmd2.Parameters.AddWithValue("@mg", mg.Value);

                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }
            }
            foreach (ListItem sh in share.Items)
            {
                if (sh.Selected)
                {
                    conn.Open();
                    SqlCommand cmd3 = new SqlCommand("sharable_sp", conn);
                    cmd3.CommandType = CommandType.StoredProcedure;

                    cmd3.Parameters.AddWithValue("@uid", newUserID.Value);
                    cmd3.Parameters.AddWithValue("@item", sh.Value);

                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        protected void upload_click(object sender, EventArgs e)
        {
            if (avatarUpload.HasFile)
            {
                try
                {
                    string filePath = Server.MapPath("/avCollection/" + avatarUpload.FileName);
                    avatarUpload.SaveAs(filePath);
                    avatarContainer.InnerHtml = "<img id=\"avimg\" width=\"300px\" src=\"/avCollection/" + avatarUpload.FileName + "\">";
                    UpStatus.Text = "Transfer successful.";
                    imgSrc.Value = "avCollection/" + avatarUpload.FileName;
                }
                catch (Exception ex)
                {
                    UpStatus.Text = "File failed to transfer:&nbsp;" + ex.Message.ToString();
                }
            }
        }

        protected void Av_Deactivate(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("userAvatar_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uid", newUserID.Value);
            cmd.Parameters.AddWithValue("@avatar", imgSrc.Value);

            cmd.ExecuteNonQuery();
            conn.Close();
            imgSrc.Value = "";
        }

        protected void Fini_Activate(object sender, EventArgs e)
        {
            // Avatar data
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("get_avatar_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", newUserID.Value);
            SqlDataReader imgDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (imgDR.Read())
            {
                avatarImg.ImageUrl = imgDR["avatar"].ToString();
            }
            imgDR.Close();

            // User Details
            DataTable userTbl = new DataTable("user");
            SqlCommand usercmd = new SqlCommand("get_user_tbl", conn);
            usercmd.CommandType = CommandType.StoredProcedure;
            usercmd.Parameters.AddWithValue("@userID", newUserID.Value);
            SqlDataAdapter userDA = new SqlDataAdapter(usercmd);

            userDA.Fill(userTbl);

            userDetails.DataSource = userTbl;
            userDetails.DataMember = "user";
            userDetails.DataBind();

            // User self description.
            DataTable descTbl = new DataTable("desc");

            SqlCommand descCmd = new SqlCommand("get_userDescription_sp", conn);
            descCmd.CommandType = CommandType.StoredProcedure;

            descCmd.Parameters.AddWithValue("@uID", newUserID.Value);

            SqlDataAdapter descDA = new SqlDataAdapter(descCmd);
            //conn.Close();
            descDA.Fill(descTbl);

            Description.DataSource = descTbl;
            Description.DataMember = "desc";
            Description.DataBind();

            // visited venues
            DataTable vvDT = new DataTable("VisitedVenue");

            SqlCommand vvcmd = new SqlCommand("get_UvenueList_sp", conn);
            vvcmd.CommandType = CommandType.StoredProcedure;

            vvcmd.Parameters.AddWithValue("@uID", newUserID.Value);

            SqlDataAdapter vvDA = new SqlDataAdapter(vvcmd);

            vvDA.Fill(vvDT);

            vvRep.DataSource = vvDT;
            vvRep.DataMember = "VisitedVenue";
            vvRep.DataBind();

            // preferred venue type
            DataTable vt = new DataTable("VenueType");

            SqlCommand vtcmd = new SqlCommand("get_UvenueType_sp", conn);
            vtcmd.CommandType = CommandType.StoredProcedure;

            vtcmd.Parameters.AddWithValue("@uID", newUserID.Value);

            SqlDataAdapter vtDA = new SqlDataAdapter(vtcmd);
            //conn.Close();
            vtDA.Fill(vt);

            vtRep.DataSource = vt;
            vtRep.DataMember = "VenueType";
            vtRep.DataBind();

            // preferred music genre
            DataTable mg = new DataTable("musicGenre");

            SqlCommand mgcmd = new SqlCommand("get_Umusicgenre_sp", conn);
            mgcmd.CommandType = CommandType.StoredProcedure;

            mgcmd.Parameters.AddWithValue("@uID", newUserID.Value);

            SqlDataAdapter mgDA = new SqlDataAdapter(mgcmd);

            mgDA.Fill(mg);

            mgRep.DataSource = mg;
            mgRep.DataMember = "musicGenre";
            mgRep.DataBind();

            // Favorite Artists
            DataTable artDT = new DataTable("art");
            // conn.Open();

            SqlCommand artCmd = new SqlCommand("get_UArtist_sp", conn);
            artCmd.CommandType = CommandType.StoredProcedure;

            artCmd.Parameters.AddWithValue("@uID", newUserID.Value);

            SqlDataAdapter artDA = new SqlDataAdapter(artCmd);

            artDA.Fill(artDT);

            artRep.DataSource = artDT;
            artRep.DataMember = "art";
            artRep.DataBind();

            // Information willing to share
            DataTable shareDT = new DataTable("share");
            // conn.Open();

            SqlCommand shareCmd = new SqlCommand("get_sharable_sp", conn);
            shareCmd.CommandType = CommandType.StoredProcedure;

            shareCmd.Parameters.AddWithValue("@uid", newUserID.Value);

            SqlDataAdapter shareDA = new SqlDataAdapter(shareCmd);
            conn.Close();
            shareDA.Fill(shareDT);

            shareRep.DataSource = shareDT;
            shareRep.DataMember = "share";
            shareRep.DataBind();
        }

        protected void create_Deactivate(object sender, EventArgs e)
        {
            Session["primaryPWD"] = apwd.Text;
            Session["confirmPWD"] = bpwd.Text;
        }

        protected void regWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
        }
    }

    public class newUser
    {
        public int uID { get; set; }
        public string userName { get; set; }
    }
}