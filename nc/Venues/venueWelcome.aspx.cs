using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Venues
{
    public partial class welcome : System.Web.UI.Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected int UIdentifier
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData.ToString());
            }
        }

        public string venueName
        {
            get
            {
                SqlParameter retParam;
                SqlParameter retBannerParam;
                string strResult;
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_venueName_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vid", UIdentifier);

                retParam = cmd.Parameters.Add(new SqlParameter("@vN", SqlDbType.VarChar, 255));
                retParam.Direction = ParameterDirection.Output;
                retBannerParam = cmd.Parameters.Add(new SqlParameter("@vB", SqlDbType.VarChar, 255));
                retBannerParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                con.Close();
                strResult = retParam.Value.ToString();
                return (strResult == "na") ? string.Empty : strResult;
            }
        }

        protected int SelectedVenue
        {
            get
            {
                int s = (int)ViewState["VID"];
                return (string.IsNullOrEmpty(s.ToString()) || s < 0) ? -1 : s;
            }
            set { ViewState["VID"] = value; }
        }

        public void postReset()
        {
            userPosts.reset();
        }

        private DataTable _vListDT = new DataTable();

        private DataTable vListDT(int ui)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand uvCmd = new SqlCommand("get_venueSDescription_sp", con);
            uvCmd.CommandType = CommandType.StoredProcedure;

            uvCmd.Parameters.AddWithValue("@uid", UIdentifier);

            SqlDataAdapter uvDA = new SqlDataAdapter(uvCmd);
            con.Close();
            uvDA.Fill(_vListDT);
            return _vListDT;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            posterCtrl.siteSide = "Venue";
            posterCtrl.con = conn;
            posterCtrl.userID = UIdentifier;
            posterCtrl.userName = venueName;
            posterCtrl.venueName = venueName;
            posterCtrl.siteSide = "Venue";
            userPosts.loc = "profile";

            // The direction is "Venue" Because the recipient is a venue.
            // User and venue is differentiated to make sure post user id's do not overlap.

            userPosts.direction = "Venue";
            userPosts.uId = UIdentifier;
            userPosts.loc = "profile";
            userPosts.ctrlHeight = 420;

            imgGallery.dirName = venueName;
            imgGallery.side = "Venue";
            if (!Page.IsPostBack)
            {
                venueMV.Visible = true;
                venueMV.ActiveViewIndex = 0;
                SelectedVenue = -1;
            }
        }

        protected void rootDir_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "sendPost":
                    break;

                case "UploadBanner":
                    if (bannerLoad.HasFile)
                    {
                        SqlParameter imgParam;
                        string iPath = "/eventImages/";
                        string eBannerDirectory = iPath;
                        string eBannerName = bannerLoad.FileName.ToString();
                        string eventBanner = eBannerDirectory + eBannerName;
                        bannerLoad.SaveAs(Server.MapPath("/Venues/" + eventBanner));

                        SqlConnection con0 = new SqlConnection(conn);
                        con0.Open();

                        SqlCommand cmd = new SqlCommand("add_venue_newImg_sp", con0);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@vid", UIdentifier);
                        cmd.Parameters.AddWithValue("@iName", bannerLoad.FileName.ToString());
                        cmd.Parameters.AddWithValue("@iType", "Event Banner");
                        int dIndex = eventBanner.LastIndexOf('/');
                        string directory = eventBanner.Remove(dIndex);
                        cmd.Parameters.AddWithValue("@directory", "Venues" + directory);

                        imgParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                        imgParam.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        con0.Close();

                        ViewState["imgIdentifier"] = imgParam.Value.ToString();
                        ViewState["bannerPath"] = "../Venues/" + eventBanner;
                        imgID.Value = (string)ViewState["imIdentifier"];

                        imgBanner.ImageUrl = (string)ViewState["bannerPath"];
                        imgBanner.Visible = true;
                    }
                    break;

                case "postEvent":
                    if (eventTitle.Text != string.Empty)
                    {
                        string eTitle = eventTitle.Text;
                        int bannerIdentifier = Convert.ToInt32((string)ViewState["imgIdentifier"]);
                        string selectedDate = ((TextBox)datePicker1.FindControl("eventDate")).Text;
                        string h = timeControl1.eh;
                        string m = timeControl1.em;
                        string s = timeControl1.es;
                        string ed = timeControl1.dp;
                        string eventTime = h + ":" + m + ":" + s + ed;
                        bool recurring = (recuring0.Checked) ? true : false;
                        string eDescription = eventDescription.Value;

                        SqlConnection con2 = new SqlConnection(conn);
                        con2.Open();

                        SqlCommand eventCmd = new SqlCommand("addEvent_sp", con2);
                        eventCmd.CommandType = CommandType.StoredProcedure;

                        eventCmd.Parameters.AddWithValue("@vid", UIdentifier);
                        eventCmd.Parameters.AddWithValue("@Title", eTitle);
                        eventCmd.Parameters.AddWithValue("@bannerID", bannerIdentifier);
                        eventCmd.Parameters.AddWithValue("@eventDate", selectedDate);
                        eventCmd.Parameters.AddWithValue("@Time", eventTime);
                        eventCmd.Parameters.AddWithValue("@recurring", recurring);
                        eventCmd.Parameters.AddWithValue("@eDes", eDescription);

                        eventCmd.ExecuteNonQuery();
                        con2.Close();

                        ViewState["imgIdentifier"] = "";
                        ViewState["bannerPat"] = "";

                        eventTitle.Text = "";
                        imgBanner.ImageUrl = "";
                        imgID.Value = "";
                        ((TextBox)datePicker1.FindControl("eventDate")).Text = "";
                        recuring0.Checked = false;
                        Div1.InnerHtml = "";
                        eventDescription.Value = "";
                    }
                    else
                    {
                        throw new ArgumentNullException("Unable to post a new event.  Please fill out all fields and try again.");
                    }
                    break;

                case "DeleteEvent":
                    string img2Delete;
                    int eventID = Convert.ToInt32(e.CommandArgument.ToString());
                    SqlParameter img;

                    SqlConnection con3 = new SqlConnection(conn);
                    con3.Open();

                    SqlCommand deCmd = new SqlCommand("delete_venue_event_sp", con3);
                    deCmd.CommandType = CommandType.StoredProcedure;

                    deCmd.Parameters.AddWithValue("@eid", eventID);
                    img = deCmd.Parameters.Add(new SqlParameter("@img2Delete", SqlDbType.VarChar, 1000));
                    img.Direction = ParameterDirection.Output;

                    deCmd.ExecuteNonQuery();
                    con3.Close();

                    img2Delete = img.Value.ToString();
                    if (!string.IsNullOrEmpty(img2Delete))
                    {
                        FileInfo f2d = new FileInfo(img2Delete);
                        f2d.Delete();
                        errLbl.Visible = false;
                    }
                    else
                    {
                        errLbl.Visible = true;
                        errLbl.Text = "File is either missing or has been moved.";
                    }

                    string EventDate = VenueEvents0.SelectedDate.ToShortDateString();

                    con3.Open();

                    SqlCommand ecmd = new SqlCommand("get_DetailedEvent4Venue_sp", con3);
                    ecmd.CommandType = CommandType.StoredProcedure;

                    ecmd.Parameters.AddWithValue("@vid", UIdentifier);
                    ecmd.Parameters.AddWithValue("@eDate", EventDate);

                    SqlDataAdapter eda = new SqlDataAdapter(ecmd);
                    con3.Close();
                    DataTable edt = new DataTable();
                    eda.Fill(edt);

                    eventDetails.SelectedIndex = -1;
                    eventDetails.DataSource = edt;
                    eventDetails.DataBind();
                    break;
            }
        }

        protected void render_vDay(object sender, DayRenderEventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand eventCmd = new SqlCommand("get_event4Venue_sp", con);
            eventCmd.CommandType = CommandType.StoredProcedure;
            eventCmd.Parameters.AddWithValue("@vid", UIdentifier);
            SqlDataAdapter eventDA = new SqlDataAdapter(eventCmd);
            con.Close();
            DataTable eventDT = new DataTable();
            eventDA.Fill(eventDT);

            foreach (DataRow dr in eventDT.Rows)
            {
                if (!e.Day.IsOtherMonth)
                {
                    if (e.Day.Date.ToString() == dr["eDate"].ToString())
                    {
                        e.Day.IsSelectable = true;
                        e.Cell.BackColor = System.Drawing.Color.FromArgb(146, 153, 159);
                    }
                }
                else
                {
                    e.Cell.Text = "";
                    e.Day.IsSelectable = false;
                }
            }
        }

        protected void Event_Change(object sender, EventArgs e)
        {
            string EventDate = VenueEvents0.SelectedDate.ToShortDateString();

            SqlConnection con = new SqlConnection(conn);
            con.Open();

            SqlCommand ecmd = new SqlCommand("get_DetailedEvent4Venue_sp", con);
            ecmd.CommandType = CommandType.StoredProcedure;

            ecmd.Parameters.AddWithValue("@vid", UIdentifier);
            ecmd.Parameters.AddWithValue("@eDate", EventDate);

            SqlDataAdapter eda = new SqlDataAdapter(ecmd);
            con.Close();
            DataTable edt = new DataTable();
            eda.Fill(edt);

            eventDetails.DataSource = edt;
            eventDetails.DataBind();
        }

        public string subDescription(object DescriptionItem)
        {
            string Description = (string)(DataBinder.Eval(DescriptionItem, "evDesc"));
            if (Description.Length >= 75)
            {
                return Description.Remove(75) + "...";
            }
            else
            {
                return Description;
            }
        }

        public string getEventImage(object Directory, object ImgName)
        {
            string directory = (string)(DataBinder.Eval(Directory, "ebDir"));
            string Name = (string)(DataBinder.Eval(ImgName, "eBName"));
            return "/" + directory + "/" + Name;
        }

        protected void sendPost_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void venueBtn_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "vProfile":
                    venueMV.ActiveViewIndex = 0;
                    break;

                case "vShared":
                    venueMV.ActiveViewIndex = 1;
                    break;

                case "vGallery":
                    venueMV.ActiveViewIndex = 2;
                    break;

                case "vEvents":
                    venueMV.ActiveViewIndex = 3;
                    break;

                case "vReviews":
                    venueMV.ActiveViewIndex = 4;
                    break;

                case "vMessages":
                    venueMV.ActiveViewIndex = 5;
                    break;
            }
        }

        protected void VenueStaffVenue_Activate(object sender, EventArgs e)
        {
        }

        protected void venueView_Activate(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();

            SqlCommand vuaCmd = new SqlCommand("get_Venue_Userlist_sp", con);
            vuaCmd.CommandType = CommandType.StoredProcedure;

            vuaCmd.Parameters.AddWithValue("@vID", UIdentifier);

            DataTable vuaDT = new DataTable();
            SqlDataAdapter vuaDA = new SqlDataAdapter(vuaCmd);
            con.Close();
            vuaDA.Fill(vuaDT);

            cVUsers.SelectedIndex = -1;
            cVUsers.DataSource = vuaDT;
            cVUsers.DataBind();
        }

        private int intRanResult;
        private static Random rand = new Random();

        protected int getRandomCode()
        {
            for (int i = 0; i < 1; i++)
            {
                intRanResult = rand.Next(1000, 9999);
            }
            return intRanResult;
        }

        // This has to be re-tooled.
        protected void addVenueUser_Click(object sender, EventArgs e)
        {
            eCheck.Text = "";
            string ea = emailVAddy.Text;
            string UFN = UserFName.Text;
            string ULN = UserLName.Text;
            string vRole = VenueRoles.SelectedValue.ToString();
            int randCode4Authent = getRandomCode();
            int intResult;

            SqlParameter uAccessParam;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand vuRegCmd = new SqlCommand("venue_register_user_sp", con);
            vuRegCmd.CommandType = CommandType.StoredProcedure;

            vuRegCmd.Parameters.AddWithValue("@vID", UIdentifier);
            vuRegCmd.Parameters.AddWithValue("@ue", ea);
            vuRegCmd.Parameters.AddWithValue("@FN", UFN);
            vuRegCmd.Parameters.AddWithValue("@LN", ULN);
            vuRegCmd.Parameters.AddWithValue("@role", vRole);
            vuRegCmd.Parameters.AddWithValue("@ackCode", randCode4Authent);

            uAccessParam = vuRegCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            uAccessParam.Direction = ParameterDirection.ReturnValue;

            vuRegCmd.ExecuteNonQuery();
            intResult = Convert.ToInt32(uAccessParam.Value.ToString());

            SqlCommand vuaCmd = new SqlCommand("get_Venue_Userlist_sp", con);
            vuaCmd.CommandType = CommandType.StoredProcedure;

            vuaCmd.Parameters.AddWithValue("@vID", UIdentifier);

            DataTable vuaDT = new DataTable();
            SqlDataAdapter vuaDA = new SqlDataAdapter(vuaCmd);
            con.Close();
            vuaDA.Fill(vuaDT);

            cVUsers.SelectedIndex = -1;
            cVUsers.DataSource = vuaDT;
            cVUsers.DataBind();
            emailVAddy.Text = "";
            UserFName.Text = "";
            UserLName.Text = "";
            if (intResult > 0)
            {
                eCheck.Text = "A message has been sent to the user for acknolodgement.";
            }
            else
            {
                eCheck.Text = "An invitation to join the network has been sent.  Acknowledgement can be issued once registration has been completed.";
            }
        }

        public string isAcknowledged(object isChecked)
        {
            bool ack = Convert.ToBoolean(DataBinder.Eval(isChecked, "Acknowledged"));
            if (ack)
            {
                return "../imgs/checked.png";
            }
            else
            {
                return "../imgs/uncheck.png";
            }
        }

        protected void cVUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int vuaid = Convert.ToInt32(cVUsers.DataKeys[e.RowIndex].Value.ToString());
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand removeU = new SqlCommand("remove_userFromVenue_sp", con);
            removeU.CommandType = CommandType.StoredProcedure;

            removeU.Parameters.AddWithValue("@vuaID", vuaid);

            removeU.ExecuteNonQuery();

            cVUsers.EditIndex = -1;
            SqlCommand vuaCmd = new SqlCommand("get_Venue_Userlist_sp", con);
            vuaCmd.CommandType = CommandType.StoredProcedure;

            vuaCmd.Parameters.AddWithValue("@vID", UIdentifier);

            DataTable vuaDT = new DataTable();
            SqlDataAdapter vuaDA = new SqlDataAdapter(vuaCmd);
            con.Close();
            vuaDA.Fill(vuaDT);

            cVUsers.DataSource = vuaDT;
            cVUsers.DataBind();
        }
    }
}