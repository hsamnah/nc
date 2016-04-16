using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nc.UserItems
{
    public partial class userwelcome : Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int UI
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        private int FUI
        {
            get { return Convert.ToInt32((string.IsNullOrEmpty(Request.QueryString["uPage"]) || (string.IsNullOrWhiteSpace(Request.QueryString["uPage"])) ? "0" : Request.QueryString["uPage"])); }
        }

        private int CUI
        {
            get
            {
                return (FUI == 0) ? UI : FUI;
            }
        }

        private string userName
        {
            get
            {
                string un;
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand uCmd = new SqlCommand("get_userName_sp", con);
                uCmd.CommandType = CommandType.StoredProcedure;

                uCmd.Parameters.AddWithValue("@uid", UI);

                un = uCmd.ExecuteScalar().ToString();
                con.Close();
                return un;
            }
        }

        private string ip
        {
            get
            {
                string ipaddress;

                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                return ipaddress;
            }
        }

        private string storedIP
        {
            get
            {
                SqlParameter param = new SqlParameter();
                SqlParameter retParam = new SqlParameter();
                int intResult = new int();
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand ipcmd = new SqlCommand("testIP_sp", con);
                ipcmd.CommandType = CommandType.StoredProcedure;
                ipcmd.Parameters.AddWithValue("@uid", UI);
                ipcmd.Parameters.AddWithValue("@ipAddress", ip);
                param = ipcmd.Parameters.Add(new SqlParameter("@ipOut", SqlDbType.VarChar, 20));
                param.Direction = ParameterDirection.Output;
                retParam = ipcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                retParam.Direction = ParameterDirection.ReturnValue;

                ipcmd.ExecuteNonQuery();
                con.Close();

                intResult = Convert.ToInt32(retParam.Value);
                if (intResult > 0)
                {
                    return param.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private string curUser
        {
            get
            {
                string un;
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand uCmd = new SqlCommand("get_userName_sp", con);
                uCmd.CommandType = CommandType.StoredProcedure;

                uCmd.Parameters.AddWithValue("@uid", CUI);

                un = uCmd.ExecuteScalar().ToString();
                con.Close();
                return un;
            }
        }

        private SqlParameter lparamID;

        private int userLocationID
        {
            get
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_userLocationID_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", CUI);

                lparamID = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                lparamID.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                con.Close();

                return Convert.ToInt32(lparamID.Value.ToString());
            }
        }

        private string rootP
        {
            get
            {
                return "userUploads/" + userName + UI + "/";
            }
        }

        private string _np;

        private string newPath
        {
            get { return (_np == null) ? string.Empty : _np; }
            set { _np = value; }
        }

        private string strResult;

        private string av
        {
            get
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_avatar_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@uid", CUI);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    strResult = dr["avatar"].ToString();
                }
                if (!string.IsNullOrEmpty(strResult))
                {
                    return "/" + strResult;
                }
                else
                {
                    return "/avCollection/avUnavailable.jpg";
                }
            }
        }

        public void postReset()
        {
            userPosts1.reset();
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

        private DataTable _vListDT = new DataTable();

        private DataTable vListDT(int ui)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand uvCmd = new SqlCommand("get_venueSDescription_sp", con);
            uvCmd.CommandType = CommandType.StoredProcedure;

            uvCmd.Parameters.AddWithValue("@uid", UI);

            SqlDataAdapter uvDA = new SqlDataAdapter(uvCmd);
            con.Close();
            uvDA.Fill(_vListDT);
            return _vListDT;
        }

        protected DataTable _fl = new DataTable();

        protected DataTable flDT(int userID)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand flCmd = new SqlCommand("get_FriendList_sp", con);
            flCmd.CommandType = CommandType.StoredProcedure;

            flCmd.Parameters.AddWithValue("@userID", userID);

            SqlDataAdapter flDA = new SqlDataAdapter(flCmd);
            flDA.Fill(_fl);
            con.Close();
            return _fl;
        }

        public HtmlInputHidden stateBag
        {
            get { return userEvent1.ctrlState; }
            set { userEvent1.ctrlState = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (storedIP == null)
            {
                Response.Redirect("/UserItems/uiSys.aspx?uiResolve=/UserItems/UserWelcome.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userImg.ImageUrl = av;
            avatarUpload.userIdentifier = UI;
            avatarUpload.avString = av;
            avatarUpload.avImg = userImg;
            posterCtrl.siteSide = "User";
            posterCtrl.con = conn;
            posterCtrl.userName = userName;
            posterCtrl.userID = UI;

            userPosts1.uId = UI;
            userPosts1.loc = "profile";
            userPosts2.loc = "share";
            // Direction is "User" because the recipient is a General User.
            // User and venue is differentiated to make sure post user id's do not overlap.
            userPosts1.direction = "User";
            userPosts2.direction = "User";

            userPosts1.ctrlHeight = 420;
            userPosts2.ctrlHeight = 600;
            chatHistory.userName = userName;
            userEvent1.uID = UI;
            imgGallery.dirName = userName + UI;
            imgGallery.side = "User";
            if (!Page.IsPostBack)
            {
                UserInformation.ActiveViewIndex = 0;

                if (e4u(DateTime.Now).Rows.Count > 0)
                {
                    eventList.DataSource = e4u((DateTime)DateTime.Now);
                    eventList.DataBind();
                }
                else
                {
                    availableEvents.Visible = true;
                    availableEvents.Text = "There are no events available for this date.";
                }
                uv.DataSource = vListDT(UI);
                uv.DataBind();
                SelectedVenue = -1;
                if (vListDT(UI).Rows.Count > 0)
                {
                    uv.Visible = true;
                }
                else
                {
                    uv.Visible = false;
                }

                if (frCount() > 0)
                {
                    cr.Visible = true;
                    cr.Text = "Connections: " + frCount().ToString() + "&nbsp;";
                }
                else
                {
                    cr.Visible = false;
                }
                friendRequests.DataSource = fRequest();
                friendRequests.DataBind();
            }
            if (CUI != UI)
            {
                followRow.Visible = true;
            }
            else
            {
                followRow.Visible = false;
            }

            photoGCollection((string.IsNullOrEmpty((string)ViewState["path"])) ? Server.MapPath(rootP) : (string)ViewState["path"]);
            currentUser1.Text = curUser;
            eventGallery1.Identifier = UI;
            VenueListing1.userIdentity = UI;
            VenueListing1.locationIdentifier = userLocationID;
            Connect1.fId = FUI;
            Connect1.uID = UI;
        }

        protected void profile_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }

                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 0;
        }

        protected void homeinfo_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 1;
        }

        protected void ClubEvents_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 2;
        }

        protected void gallery_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 4;
        }

        protected void Venue_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 5;
        }

        protected void wReview_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 6;
        }

        protected void msgs_Click(object sender, EventArgs e)
        {
            if (uv.Visible)
            {
                if (SelectedVenue != -1)
                {
                    DataListItem dli = uv.Items[SelectedVenue] as DataListItem;
                    uv.SelectedIndex = SelectedVenue;
                    dli.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                    dli.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                }
                else
                {
                    uv.SelectedIndex = -1;
                }
                uv.SelectedIndex = SelectedVenue;
            }
            UserInformation.ActiveViewIndex = 7;
        }

        protected void searchBtn_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void userProfile_Activate(object sender, EventArgs e)
        {
        }

        protected void Share_Activate(object sender, EventArgs e)
        {
        }

        protected void Gallery_Activate(object sender, EventArgs e)
        {
        }

        private void photoGCollection(string Path)
        {
        }

        protected void vlbtn_Command(object sender, CommandEventArgs e)
        {
            LinkButton lbItem = (sender as LinkButton);
            DataListItem item = lbItem.NamingContainer as DataListItem;
            int vIndex = item.ItemIndex;

            if (e.CommandName == "Select")
            {
                item.BackColor = System.Drawing.Color.FromArgb(189, 206, 225);
                item.ForeColor = System.Drawing.Color.FromArgb(62, 86, 106);
                uv.SelectedIndex = vIndex;
                SelectedVenue = uv.SelectedIndex;
            }
        }

        protected void Unselect_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Unselect")
            {
                uv.SelectedIndex = -1;
                SelectedVenue = uv.SelectedIndex;
            }
        }

        protected void Review_Activate(object sender, EventArgs e)
        {
        }

        protected void Messages_Activate(object sender, EventArgs e)
        {
        }

        private DataTable _dt = new DataTable();

        private DataTable fRequest()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand fcmd = new SqlCommand("get_fRequests_sp", con);
            fcmd.CommandType = CommandType.StoredProcedure;

            fcmd.Parameters.AddWithValue("@ui", UI);

            SqlDataAdapter fDA = new SqlDataAdapter(fcmd);
            fDA.Fill(_dt);
            con.Close();
            return _dt;
        }

        private int frCount()
        {
            int fCount;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_fRequests_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter paramResult = new SqlParameter();
            cmd.Parameters.AddWithValue("@ui", UI);
            paramResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            paramResult.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            fCount = Convert.ToInt32(paramResult.Value.ToString());
            return fCount;
        }

        protected void eventDay_Render(object sender, DayRenderEventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand eDCmd = new SqlCommand("get_event_sp", con);
            eDCmd.CommandType = CommandType.StoredProcedure;
            eDCmd.Parameters.AddWithValue("@uid", UI);
            SqlDataAdapter eDDA = new SqlDataAdapter(eDCmd);
            con.Close();
            DataTable eDDT = new DataTable();
            eDDA.Fill(eDDT);

            if (!e.Day.IsOtherMonth)
            {
                foreach (DataRow drv in eDDT.Rows)
                {
                    if (e.Day.Date.ToShortDateString() == Convert.ToDateTime(drv["eDate"].ToString()).ToShortDateString())
                    {
                        e.Day.IsSelectable = true;
                        e.Cell.BackColor = System.Drawing.Color.FromArgb(146, 153, 159);
                    }
                }
            }
            else
            {
                e.Cell.Text = "";
            }
        }

        protected void eventsAvailable_Change(object sender, EventArgs e)
        {
            availableEvents.Visible = false;
            eventList.DataSource = e4u(Calendar1.SelectedDate);
            eventList.DataBind();
        }

        private DataTable _e4u = new DataTable();

        protected DataTable e4u(DateTime d)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand listcmd = new SqlCommand("get_events4User_sp", con);
            listcmd.CommandType = CommandType.StoredProcedure;

            listcmd.Parameters.AddWithValue("@eventDate", d.ToShortDateString());
            listcmd.Parameters.AddWithValue("@uid", UI);

            SqlDataAdapter listDA = new SqlDataAdapter(listcmd);
            con.Close();
            listDA.Fill(_e4u);
            return _e4u;
        }

        protected string getStatus(object dataItem)
        {
            bool status = Convert.ToBoolean(DataBinder.Eval(dataItem, "Status").ToString());
            if (status)
            {
                return "checked.png";
            }
            else
            {
                return "uncheck.png";
            }
        }

        protected string getRSVP(object dataItem)
        {
            bool rsvp = Convert.ToBoolean(DataBinder.Eval(dataItem, "rsvp").ToString());
            if (rsvp)
            {
                return "checked.png";
            }
            else
            {
                return "uncheck.png";
            }
        }

        public string getProperImg(object venue, object imgName)
        {
            string v = DataBinder.Eval(venue, "Venue").ToString();
            string img = DataBinder.Eval(imgName, "venueIMG").ToString();
            if (!string.IsNullOrEmpty(img))
            {
                StringBuilder vImg = new StringBuilder();
                vImg.Append("<img src=\"../Venues/venueUploads/" + v + "/" + img + "\" width=\"75px\" />");
                return vImg.ToString();
            }
            else
            {
                return "n/a";
            }
        }

        public string getPropertyDescription(object sd)
        {
            string des = DataBinder.Eval(sd, "sDescription").ToString();
            if (des.Length > 75)
            {
                return des.Remove(0, 75) + "&nbsp;...";
            }
            else
            {
                return des;
            }
        }

        protected void eventList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectEvent")
            {
                UserInformation.ActiveViewIndex = 0;
                if (pSelectedEvent.Visible == false)
                {
                    pSelectedEvent.Visible = true;
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("get_EventDetails_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@inID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@uid", UI);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Close();
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();
                }
                else
                {
                    pSelectedEvent.Visible = false;
                }
            }
        }

        protected string getEventImg(object iDirect, object iName)
        {
            // set default img
            string strName = null;
            string strDir = null;
            if (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(iName, "BannerName"))))
            {
                strName = "EventImgNA.png";
                strDir = "/imgs/";
                return strDir + strName;
            }
            else
            {
                strDir = (string)DataBinder.Eval(iDirect, "BannerDirectory");
                strName = (string)DataBinder.Eval(iName, "BannerName");
                int index = strDir.LastIndexOf("eventImages");
                string dir = strDir.Remove(0, index);
                return "../Venues/" + dir.Replace(@"\", "/") + "/" + strName;
            }
        }

        protected void friendsPosts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
        }

        private void childNodes(TreeNode parentNode, int parentID, int postID)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand nodeCmd = new SqlCommand("get_postComment_sp", con);
            nodeCmd.CommandType = CommandType.StoredProcedure;

            nodeCmd.Parameters.AddWithValue("@pid", parentID);
            nodeCmd.Parameters.AddWithValue("@identifier", postID);

            SqlDataAdapter nodeDA = new SqlDataAdapter(nodeCmd);
            DataTable nodeDT = new DataTable();
            nodeDA.Fill(nodeDT);
            con.Close();

            foreach (DataRow node in nodeDT.Rows)
            {
                TreeNode nodeTN = new TreeNode();
                nodeTN.Text = node["AuthorFN"].ToString() + "&nbsp;" + node["AuthorLN"].ToString();
                nodeTN.Value = node["commentID"].ToString();
                childNodes(nodeTN, Convert.ToInt32(node["commentID"].ToString()), postID);
                parentNode.ChildNodes.Add(nodeTN);
            }
        }

        protected void commentTree_Change(object sender, EventArgs e)
        {
        }

        protected void postCommentBtn_Click(object sender, EventArgs e)
        {
        }

        protected void commentBtn_Click(object sender, EventArgs e)
        {
        }

        public void uploadImage_Click(object sender, ImageClickEventArgs e)
        {
            windowResponseHelper.Redirect(Response, "/UserItems/userPhotos.aspx", "_blank", "menubar = 0, scrollbars = 1, titlebar=0,fullscreen=0, width = 998, height = 705, top = 10");
        }

        protected string urlConversion(object path)
        {
            string uriBase = Server.MapPath(rootP);
            string url = (DataBinder.Eval(path, "FullName").ToString());

            var url2Ref = new Uri(url);
            var reference = new Uri(uriBase);

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = "userUploads/" + userName + UI + "/" + conversion;
            return returnStr;
        }

        protected string urlConversion2(string path)
        {
            var url2Ref = new Uri(path);
            var reference = new Uri(Server.MapPath(rootP));

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = "UserItems/userUploads/" + userName + UI + "/" + conversion;
            return returnStr;
        }

        protected void backDir_Click(object sender, EventArgs e)
        {
            photoGCollection(Server.MapPath(rootP));
            ViewState["path"] = "";
        }

        protected void rsvp_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RSVP")
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();

                SqlCommand rsvpcmd = new SqlCommand("event_RSVP_sp", con);
                rsvpcmd.CommandType = CommandType.StoredProcedure;

                rsvpcmd.Parameters.AddWithValue("@inID", e.CommandArgument.ToString());
                rsvpcmd.Parameters.AddWithValue("@rsvp", true);

                rsvpcmd.ExecuteNonQuery();

                SqlCommand resetcmd = new SqlCommand("get_EventDetails_sp", con);
                resetcmd.CommandType = CommandType.StoredProcedure;

                resetcmd.Parameters.AddWithValue("@inID", e.CommandArgument.ToString());
                resetcmd.Parameters.AddWithValue("@uid", UI);

                SqlDataAdapter da = new SqlDataAdapter(resetcmd);
                DataTable dt = new DataTable();
                con.Close();
                da.Fill(dt);

                DetailsView1.DataSource = dt;
                DetailsView1.DataBind();
            }
            if (e.CommandName == "unRSVP")
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();

                SqlCommand rsvpcmd = new SqlCommand("event_RSVP_sp", con);
                rsvpcmd.CommandType = CommandType.StoredProcedure;

                rsvpcmd.Parameters.AddWithValue("@inID", e.CommandArgument.ToString());
                rsvpcmd.Parameters.AddWithValue("@rsvp", false);

                rsvpcmd.ExecuteNonQuery();

                SqlCommand resetcmd = new SqlCommand("get_EventDetails_sp", con);
                resetcmd.CommandType = CommandType.StoredProcedure;

                resetcmd.Parameters.AddWithValue("@inID", e.CommandArgument.ToString());
                resetcmd.Parameters.AddWithValue("@uid", UI);

                SqlDataAdapter da = new SqlDataAdapter(resetcmd);
                DataTable dt = new DataTable();
                con.Close();
                da.Fill(dt);

                DetailsView1.DataSource = dt;
                DetailsView1.DataBind();
            }
        }

        protected void venueList_Activate(object sender, EventArgs e)
        {
        }

        protected void myPLace_Click(object sender, EventArgs e)
        {
            Response.Redirect("userWelcome.aspx");
        }

        protected void friendRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int reqIdentifier = Convert.ToInt32(friendRequests.DataKeys[index].Value);

            DataList fdl = Master.FindControl("FriendList") as DataList;

            cr.Text = "";

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            switch (e.CommandName)
            {
                case "AcceptRequest":
                    SqlCommand acmd = new SqlCommand("accept_request_sp", con);
                    acmd.CommandType = CommandType.StoredProcedure;
                    acmd.Parameters.AddWithValue("@pfrId", reqIdentifier);
                    acmd.ExecuteNonQuery();

                    friendRequests.SelectedIndex = -1;
                    friendRequests.DataSource = fRequest();
                    friendRequests.DataBind();
                    cr.Text = string.Empty;
                    if (frCount() > 0)
                    {
                        cr.Visible = true;
                        cr.Text = "Connections: " + frCount().ToString() + "&nbsp;";
                    }
                    else
                    {
                        cr.Visible = false;
                    }
                    fdl.SelectedIndex = -1;
                    fdl.DataSource = flDT(UI);
                    fdl.DataBind();
                    break;

                case "DeleteRequest":
                    SqlCommand dcmd = new SqlCommand("delete_request_sp", con);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    dcmd.Parameters.AddWithValue("@pfrId", reqIdentifier);
                    dcmd.ExecuteNonQuery();

                    friendRequests.SelectedIndex = -1;
                    friendRequests.DataSource = fRequest();
                    friendRequests.DataBind();
                    cr.Text = string.Empty;
                    if (frCount() > 0)
                    {
                        cr.Visible = true;
                        cr.Text = "Connections: " + frCount().ToString() + "&nbsp;";
                    }
                    else
                    {
                        cr.Visible = false;
                    }
                    fdl.SelectedIndex = -1;
                    fdl.DataSource = flDT(UI);
                    fdl.DataBind();
                    break;
            }
            con.Close();
        }
    }
}