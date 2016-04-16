using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class vReg : Page
    {
        private int intResult;
        private string _Dir;
        private SqlParameter isUniqueEmail;
        private SqlParameter uAccessParam;

        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private string getDir(int vID)
        {
            // this method returns the venue created directory.  It is currently incomplete.
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand dircmd = new SqlCommand("get_venueDirectory_sp", conn);
            dircmd.CommandType = CommandType.StoredProcedure;

            dircmd.Parameters.AddWithValue("@vID", vID);
            _Dir = (string)dircmd.ExecuteScalar();

            return _Dir;
        }

        private SqlParameter pResult;

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

        private int regIndex
        {
            get { return (string.IsNullOrEmpty(Request.QueryString["in"])) ? 0 : Convert.ToInt32(Request.QueryString["in"]); }
        }

        private int queryTmpID
        {
            get { return Convert.ToInt32(Request.QueryString["dent"]); }
        }

        private int vCode
        {
            get { return Convert.ToInt32((string.IsNullOrEmpty(Request.QueryString["gc"])) ? "0" : Request.QueryString["gc"]); }
        }

        private int storeVenueID
        {
            get
            {
                int vid = (int)ViewState["vid"];
                return (vid <= 0) ? 0 : vid;
            }
            set { ViewState["vid"] = value; }
        }

        private bool isEmailUnique(string email)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand emailCmd = new SqlCommand("isEmailUnique_sp", conn);
            emailCmd.CommandType = CommandType.StoredProcedure;

            emailCmd.Parameters.AddWithValue("@email", email);
            isUniqueEmail = emailCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Bit));
            isUniqueEmail.Direction = ParameterDirection.ReturnValue;
            emailCmd.ExecuteNonQuery();
            conn.Close();
            return Convert.ToBoolean(isUniqueEmail.Value);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vRegWizard.ActiveStepIndex = regIndex;
                ListItem vmEB = new ListItem();
                vmEB.Text = "Email Broadcasting";
                vmEB.Value = ((int)Enumerators.VM.EmailBroadcasting).ToString();
                VM.Items.Add(vmEB);
                ListItem vmBP = new ListItem();
                vmBP.Text = "Banner Publishing";
                vmBP.Value = ((int)Enumerators.VM.BannerPublishing).ToString();
                VM.Items.Add(vmBP);
                ListItem vmFW = new ListItem();
                vmFW.Text = "Frontend Website";
                vmFW.Value = ((int)Enumerators.VM.FrontendWebsite).ToString();
                VM.Items.Add(vmFW);
                ListItem vmWP = new ListItem();
                vmWP.Text = "Website Positioning";
                vmWP.Value = ((int)Enumerators.VM.WebSitePositioning).ToString();
                VM.Items.Add(vmWP);
                ListItem vmMR = new ListItem();
                vmMR.Text = "Market Research";
                vmMR.Value = ((int)Enumerators.VM.MarketResearch).ToString();
                VM.Items.Add(vmMR);

                ListItem epPG = new ListItem();
                epPG.Text = "Photo Gallery";
                epPG.Value = ((int)Enumerators.EP.photogallery).ToString();
                EP.Items.Add(epPG);
                ListItem epEB = new ListItem();
                epEB.Text = "Email Broadcasting";
                epEB.Value = ((int)Enumerators.EP.EmailBroadcasting).ToString();
                EP.Items.Add(epEB);
                ListItem epTS = new ListItem();
                epTS.Text = "Ticket Sales";
                epTS.Value = ((int)Enumerators.EP.ticketSales).ToString();
                EP.Items.Add(epTS);
                ListItem epER = new ListItem();
                epER.Text = "Event Reservatopms";
                epER.Value = ((int)Enumerators.EP.Reservations).ToString();
                EP.Items.Add(epER);
                ListItem epEC = new ListItem();
                epEC.Text = "Public Event Calendar";
                epEC.Value = ((int)Enumerators.EP.EventCalendar).ToString();
                EP.Items.Add(epEC);

                ListItem crRTC = new ListItem();
                crRTC.Text = "Real Time Chat";
                crRTC.Value = ((int)Enumerators.CR.RealTimeChat).ToString();
                CR.Items.Add(crRTC);
                ListItem crEB = new ListItem();
                crEB.Text = "Email Broadcasting";
                crEB.Value = ((int)Enumerators.CR.EmailBroadcasting).ToString();
                CR.Items.Add(crEB);
                ListItem crNR = new ListItem();
                crNR.Text = "News Releases";
                crNR.Value = ((int)Enumerators.CR.newsreleases).ToString();
                CR.Items.Add(crNR);

                ListItem rsJB = new ListItem();
                rsJB.Text = "Job Board";
                rsJB.Value = ((int)Enumerators.RS.jobBoard).ToString();
                RS.Items.Add(rsJB);
                ListItem rsES = new ListItem();
                rsES.Text = "Event Scheduling";
                rsES.Value = ((int)Enumerators.RS.EventScheduling).ToString();
                RS.Items.Add(rsES);
                ListItem rsComm = new ListItem();
                rsComm.Text = "Communication";
                rsComm.Value = ((int)Enumerators.RS.Communication).ToString();
                RS.Items.Add(rsComm);
                get_CountryList();
            }
        }

        protected void vRegWizard_ActiveStepChanged(object sender, EventArgs e)
        {
        }

        private int tmpID;
        private SqlParameter vParamResult;

        protected void EmailConfirm_Activate(object sender, EventArgs e)
        {
            int RandCode = getRandomCode();
            if (regIndex <= 0)
            {
                // I need to create a temp venue table.
                SqlConnection conn = new SqlConnection(con);
                conn.Open();
                SqlCommand tvcmd = new SqlCommand("insert_Venue_tmp_sp", conn);
                tvcmd.CommandType = CommandType.StoredProcedure;

                tvcmd.Parameters.AddWithValue("@venueName", VenueName.Text);
                tvcmd.Parameters.AddWithValue("@vun", UN.Text);
                tvcmd.Parameters.AddWithValue("@emailAdd", pEmail.Text);
                tvcmd.Parameters.AddWithValue("@pwd", pwd.Text);
                tvcmd.Parameters.AddWithValue("@secQ", secQuest.Text);
                tvcmd.Parameters.AddWithValue("@secA", secAnswer.Text);
                tvcmd.Parameters.AddWithValue("@secH", secHint.Text);
                tvcmd.Parameters.AddWithValue("@conCode", RandCode);
                vParamResult = tvcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                vParamResult.Direction = ParameterDirection.ReturnValue;
                tvcmd.ExecuteNonQuery();
                conn.Close();
                tmpID = Convert.ToInt32(vParamResult.Value.ToString());
                if (tmpID > 0)
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("register@i-underground.com");
                    mail.To.Add(pEmail.Text);
                    mail.Subject = "The Underground Validation.";
                    StringBuilder vRegBody = new StringBuilder();
                    vRegBody.Append(@"Welcome to The Underground Network.  The future of evening entertainment is upon us." + "\n");
                    vRegBody.Append("\n");
                    vRegBody.Append(@"Please click on the link below to validate your registration with i-underground.com:" + "\n");
                    vRegBody.Append(@"http://www.i-underground.com/vRegistry.aspx?in=1&dent=" + tmpID + "&gc=" + RandCode + "\n");
                    vRegBody.Append(@"\n");
                    vRegBody.Append(@"Regards," + "\n");
                    vRegBody.Append(@"Registration, The Underground Network-" + "\n\n");
                    vRegBody.Append(@"If this email has been sent to your email address by mistake or this email address does not belong to you" + "\n" + "please disregard and accept our apologies for any inconvenience." + "\n");
                    mail.Body = vRegBody.ToString();
                    SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                    NetworkCredential vcred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                    smtp.Credentials = vcred;
                    smtp.Send(mail);
                    test.Text = "An email has been sent to: " + pEmail.Text + ". Please click on the provided link to validate your registration.";
                }
            }
            else {
                test.Text = "Welcome back.  Thank you for validating your email address.  Your account has been configured, please click on next to proceed and complete your registration.";
            }
        }

        private SqlParameter vParam;
        private SqlParameter vNameParam;
        private int confirmParam;
        private string vName;

        protected void EmailConfirm_Deactivate(object sender, EventArgs e)
        {
            // get temp id and compare code with email code
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand confirmCmd = new SqlCommand("get_venueTmp4Code_sp", conn);
            confirmCmd.CommandType = CommandType.StoredProcedure;

            confirmCmd.Parameters.AddWithValue("@tmpID", queryTmpID);
            vParam = confirmCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            vParam.Direction = ParameterDirection.ReturnValue;

            confirmCmd.ExecuteNonQuery();
            conn.Close();
            confirmParam = Convert.ToInt32(vParam.Value.ToString());

            if (confirmParam == vCode)
            {
                conn.Open();
                SqlCommand vConfirmed = new SqlCommand("transfer_tmp2venue", conn);
                vConfirmed.CommandType = CommandType.StoredProcedure;

                vConfirmed.Parameters.AddWithValue("@tmpID", queryTmpID);

                vParam = vConfirmed.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                vParam.Direction = ParameterDirection.ReturnValue;

                vNameParam = vConfirmed.Parameters.Add(new SqlParameter("@venueName", SqlDbType.VarChar, 255));
                vNameParam.Direction = ParameterDirection.Output;

                vConfirmed.ExecuteNonQuery();
                conn.Close();

                vName = vNameParam.Value.ToString();
                newVenueID.Value = vParam.Value.ToString();
                Session.Abandon();
                storeVenueID = Convert.ToInt32(vParam.Value.ToString());

                string dPath = "Venues/venueUploads/" + vName + "/";
                if (!Directory.Exists(Server.MapPath(dPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(dPath));
                    conn.Open();
                    SqlCommand dcmd = new SqlCommand("set_Venue_Directory_sp", conn);
                    dcmd.CommandType = CommandType.StoredProcedure;

                    dcmd.Parameters.AddWithValue("@vID", vParam.Value.ToString());
                    dcmd.Parameters.AddWithValue("@vDir", dPath);

                    dcmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "var dname=prompt('You already have a directory assigned to you. Please provide a new directory name:');", true);
                    // I need to create a hidden field to hold the new directory name that will persist through events.
                }
            }
            else
            {
                vRegWizard.ActiveStepIndex = 1;
            }
        }

        protected void Unnamed_Deactivate(object sender, EventArgs e)
        {
            string summary = sDesc.Text;
            string fullDesc = fDesc.Text;
            int vIdentifier = storeVenueID;

            string ws = website.Text;
            string _genre = genre.Text;
            string _days = daysOpen.Text;
            string _time = timeControl1.eh + ":" + timeControl1.em + ":" + timeControl1.es + ":" + timeControl1.dp;
            int _cap = Convert.ToInt32(cap.Text);
            int _age = Convert.ToInt32(ageLimit.Text);
            string dcode = DressCode.Text;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand fsCmd = new SqlCommand("insert_VDescriptions_sp", conn);
            fsCmd.CommandType = CommandType.StoredProcedure;

            fsCmd.Parameters.AddWithValue("@vid", vIdentifier);
            fsCmd.Parameters.AddWithValue("@sDesc", summary);
            fsCmd.Parameters.AddWithValue("@fDesc", fullDesc);

            fsCmd.Parameters.AddWithValue("@ws", ws);
            fsCmd.Parameters.AddWithValue("@genre", _genre);
            fsCmd.Parameters.AddWithValue("@days", _days);
            fsCmd.Parameters.AddWithValue("@time", _time);
            fsCmd.Parameters.AddWithValue("@cap", _cap);
            fsCmd.Parameters.AddWithValue("@age", _age);
            fsCmd.Parameters.AddWithValue("@dcode", dcode);

            fsCmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void com_change(object sender, EventArgs e)
        {
        }

        protected void Unique_UN_TextChanged(object sender, EventArgs e)
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

        protected void country_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void subdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCity(subdivision.SelectedValue, "subdivision");
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

        private static Random rand = new Random();
        private int intRanResult;

        protected int getRandomCode()
        {
            for (int i = 0; i < 1; i++)
            {
                intRanResult = rand.Next(1000, 9999);
            }
            return intRanResult;
        }

        protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand conCmd = new SqlCommand("insert_VenueContact_sp", conn);
            conCmd.CommandType = CommandType.StoredProcedure;
            conCmd.Parameters.AddWithValue("@vID", newVenueID.Value);
            conCmd.Parameters.AddWithValue("@numType", ddlContact.SelectedValue);
            conCmd.Parameters.AddWithValue("@contactNum", TextBox6.Text);
            conCmd.ExecuteNonQuery();
            conn.Close();

            DataList1.DataSource = getConList(newVenueID.Value);
            DataList1.DataMember = "comList";
            DataList1.DataBind();
            TextBox6.Text = "";
            ddlContact.SelectedIndex = 0;
        }

        protected void vLocation_Activate(object sender, EventArgs e)
        {
            DataList1.DataSource = getConList(newVenueID.Value);
            DataList1.DataMember = "comList";
            DataList1.DataBind();
        }

        protected void vInfo_Deactivate(object sender, EventArgs e)
        {
            string str1 = TextBox2.Text;
            string str2 = (string.IsNullOrEmpty(TextBox3.Text)) ? string.Empty : TextBox3.Text;
            string Country = country.SelectedValue;
            string subDivision = subdivision.SelectedValue;
            string city = City.SelectedItem.Text;
            string locationID = City.SelectedValue;
            string postal = TextBox5.Text;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("add_venue_address_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vid", Convert.ToInt32(newVenueID.Value));
            cmd.Parameters.AddWithValue("@str1", str1);
            cmd.Parameters.AddWithValue("@str2", str2);
            cmd.Parameters.AddWithValue("@country", Country);
            cmd.Parameters.AddWithValue("@subdiv", subDivision);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@postal", postal);
            cmd.Parameters.AddWithValue("@loID", locationID);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void cs_Deactivate(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            foreach (ListItem vmli in VM.Items)
            {
                string VM = "VM";
                if (vmli.Selected)
                {
                    SqlCommand vmCmd = new SqlCommand("add_venueServices_sp", conn);
                    vmCmd.CommandType = CommandType.StoredProcedure;

                    vmCmd.Parameters.AddWithValue("@vid", storeVenueID);
                    vmCmd.Parameters.AddWithValue("@cat", VM);
                    vmCmd.Parameters.AddWithValue("@srv", Convert.ToInt32(vmli.Value.ToString()));

                    vmCmd.ExecuteNonQuery();
                }
            }
            foreach (ListItem epli in EP.Items)
            {
                string EP = "EP";
                if (epli.Selected)
                {
                    SqlCommand epCmd = new SqlCommand("add_venueServices_sp", conn);
                    epCmd.CommandType = CommandType.StoredProcedure;

                    epCmd.Parameters.AddWithValue("@vid", storeVenueID);
                    epCmd.Parameters.AddWithValue("@cat", EP);
                    epCmd.Parameters.AddWithValue("@srv", Convert.ToInt32(epli.Value.ToString()));

                    epCmd.ExecuteNonQuery();
                }
            }
            foreach (ListItem crli in CR.Items)
            {
                string cr = "CR";
                if (crli.Selected)
                {
                    SqlCommand crCmd = new SqlCommand("add_venueServices_sp", conn);
                    crCmd.CommandType = CommandType.StoredProcedure;

                    crCmd.Parameters.AddWithValue("@vid", storeVenueID);
                    crCmd.Parameters.AddWithValue("@cat", cr);
                    crCmd.Parameters.AddWithValue("@srv", Convert.ToInt32(crli.Value.ToString()));

                    crCmd.ExecuteNonQuery();
                }
            }
            foreach (ListItem rsli in RS.Items)
            {
                string rs = "RS";
                if (rsli.Selected)
                {
                    SqlCommand rsCmd = new SqlCommand("add_venueServices_sp", conn);
                    rsCmd.CommandType = CommandType.StoredProcedure;

                    rsCmd.Parameters.AddWithValue("@vid", storeVenueID);
                    rsCmd.Parameters.AddWithValue("@cat", rs);
                    rsCmd.Parameters.AddWithValue("@srv", Convert.ToInt32(rsli.Value.ToString()));

                    rsCmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }

        protected void UploadBkgrndLink_Click(object sender, EventArgs e)
        {
            if (clrSelector.Text != "Select Color")
            {
                if (!string.IsNullOrEmpty(newVenueID.Value))
                {
                    bgContainer.Style.Add(HtmlTextWriterStyle.BackgroundColor, clrSelector.Text);
                    // this information has to be saved somewhere.
                }
                else
                {
                    UpbgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
            else if (bgUpload.HasFile)
            {
                clrSelector.Text = "Select Color";
                if (!string.IsNullOrEmpty(newVenueID.Value))
                {
                    string fp = getDir(Convert.ToInt32(newVenueID.Value)) + bgUpload.FileName;
                    bgUpload.SaveAs(Server.MapPath(fp));
                    var bgPath = getDir(Convert.ToInt32(newVenueID.Value)) + imgName(Convert.ToInt32(newVenueID.Value), fp, bgUpload.FileName, "background");

                    UpbgStatus.Text = "Transfer status: Transfer successful to: " + fp;
                    bgContainer.Style.Add(HtmlTextWriterStyle.BackgroundImage, bgPath);
                }
                else
                {
                    UpbgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
            clrSelector.Text = "Select Color";
        }

        protected void UploadLogoLink_Click(object sender, EventArgs e)
        {
            if (logoUpload.HasFile)
            {
                if (!string.IsNullOrEmpty(newVenueID.Value))
                {
                    string fp = getDir(Convert.ToInt32(newVenueID.Value)) + logoUpload.FileName;
                    logoUpload.SaveAs(Server.MapPath(fp));
                    uplgStatus.Text = "Transfer status: Transfer successful to: " + fp;
                    var logoPath = getDir(Convert.ToInt32(newVenueID.Value)) + imgName(Convert.ToInt32(newVenueID.Value), fp, logoUpload.FileName, "logo");
                    logoContainer.InnerHtml = "<img id=\"lgimg\" width=\"645px\" src=\"" + logoPath + "\" \\>";
                }
                else
                {
                    uplgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
        }

        protected void UploadBannerLink_Click(object sender, EventArgs e)
        {
            if (bannerUpload.HasFile)
            {
                if (!string.IsNullOrEmpty(newVenueID.Value))
                {
                    string fp = getDir(Convert.ToInt32(newVenueID.Value)) + bannerUpload.FileName;
                    bannerUpload.SaveAs(Server.MapPath(fp));
                    bnrUpload.Text = "Transfer status: Transfer successful to: " + fp;
                    var bannerPath = getDir(Convert.ToInt32(newVenueID.Value)) + imgName(Convert.ToInt32(newVenueID.Value), fp, bannerUpload.FileName, "banner");
                    bnrContainer.InnerHtml = "<img id=\"lgimg\" width=\"645px\" src=\"" + bannerPath + "\" \\>";
                }
                else
                {
                    bnrUpload.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
        }

        private SqlParameter iParam;

        private string imgName(int vID, string imgPath, string imgTitle, string imgType)
        {
            int dirIndex = imgPath.LastIndexOf("/");
            string imgDirectory = imgPath.Remove(dirIndex);

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand imgCmd = new SqlCommand("attach_vIMG_sp", conn);
            imgCmd.CommandType = CommandType.StoredProcedure;

            imgCmd.Parameters.AddWithValue("@vid", vID);
            imgCmd.Parameters.AddWithValue("@title", imgTitle);
            imgCmd.Parameters.AddWithValue("@imgDir", imgDirectory);
            imgCmd.Parameters.AddWithValue("@type", imgType);
            iParam = imgCmd.Parameters.Add(new SqlParameter("@imgName", SqlDbType.VarChar, 800));
            iParam.Direction = ParameterDirection.Output;

            imgCmd.ExecuteNonQuery();
            conn.Close();

            return iParam.Value.ToString();
        }

        protected void cs_Activate(object sender, EventArgs e)
        {
        }

        protected void proDesign_Deactivate(object sender, EventArgs e)
        {
        }

        private DataTable conLista = new DataTable("comList");

        private DataTable getConList(string identifier)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand lcmd = new SqlCommand("get_venue_contact_sp", conn);
            lcmd.CommandType = CommandType.StoredProcedure;

            lcmd.Parameters.AddWithValue("@vID", identifier);

            SqlDataAdapter da = new SqlDataAdapter(lcmd);
            conn.Close();
            da.Fill(conLista);
            return conLista;
        }

        protected void vRegWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
        }
    }
}