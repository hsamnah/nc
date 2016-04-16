using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class userEvent : System.Web.UI.UserControl
    {
        protected string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        public int uID { get; set; }

        public string cCtrlID
        {
            get { return containerID.Value; }
        }

        public HtmlInputHidden ctrlState
        {
            get { return ecState; }
            set { ecState = value; }
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

        protected DataTable flDT(int userID)
        {
            DataTable _fl = new DataTable();
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

        protected DataTable getEventList(int uid)
        {
            DataTable _dt = new DataTable();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_userOrganized_Events_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", uid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(_dt);
            con.Close();
            return _dt;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("UE"))
            {
                csm.RegisterClientScriptInclude("UE", "/scripts/jsUEvent.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int intIndex = new int();
            datePicker.containerID = cCtrlID;

            intIndex = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow dr in flDT(uID).Rows)
            {
                if (intIndex == 0)
                {
                    sb.Append("{\"Name\":\"" + dr["fn"].ToString() + " " + dr["ln"].ToString() + "\",\"Email\":\"" + dr["email"].ToString() + "\"}");
                }
                else
                {
                    sb.Append(",{\"Name\":\"" + dr["fn"].ToString() + " " + dr["ln"].ToString() + "\",\"Email\":\"" + dr["email"].ToString() + "\"}");
                }
                intIndex++;
            }
            sb.Append("]");
            friendList.Value = sb.ToString();
            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
            }
            if (!Page.IsPostBack)
            {
                EventListing.DataSource = getEventList(uID);
                EventListing.DataBind();
            }
        }

        protected void WhosRSVPd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void sinvites_Click(object sender, EventArgs e)
        {
            int eID = 0;
            string eLabel = elbl.Text;
            string str = street.Text;
            string c = city.Text;
            string sub = province.Text;
            string coun = country.Text;
            string postal = pz.Text;
            string date = datePicker.DateSelection;
            string eventTime = timeControl.eh + ":" + timeControl.em + ":" + timeControl.es + " " + timeControl.dp;
            string eDescription = desc.Text;
            string invCollection = invited.Value;

            char[] delimiter1 = new char[] { ',' };
            string[] nep = invCollection.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
            char[] delimiter2 = new char[] { ':' };

            SqlConnection con = new SqlConnection(conn);
            con.Open();

            SqlCommand eCmd = new SqlCommand("userEvent_setup_sp", con);
            eCmd.CommandType = CommandType.StoredProcedure;

            eCmd.Parameters.AddWithValue("@eLbl", eLabel);
            eCmd.Parameters.AddWithValue("@str", str);
            eCmd.Parameters.AddWithValue("@city", c);
            eCmd.Parameters.AddWithValue("@sub", sub);
            eCmd.Parameters.AddWithValue("@country", coun);
            eCmd.Parameters.AddWithValue("@postal", postal);
            eCmd.Parameters.AddWithValue("@eDate", date);
            eCmd.Parameters.AddWithValue("@eTime", eventTime);
            eCmd.Parameters.AddWithValue("@eDesc", eDescription);
            eCmd.Parameters.AddWithValue("@organ", uID);

            SqlParameter rvParam = new SqlParameter();
            rvParam = eCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            rvParam.Direction = ParameterDirection.ReturnValue;

            eCmd.ExecuteNonQuery();
            eID = Convert.ToInt32(rvParam.Value.ToString());
            for (int index1 = 0; index1 < nep.Length; index1++)
            {
                string[] strsne = nep[index1].Split(delimiter2);

                int genCode = getRandomCode();
                SqlCommand iCmd = new SqlCommand("user_invite_sp", con);
                iCmd.CommandType = CommandType.StoredProcedure;

                iCmd.Parameters.AddWithValue("@name", strsne[0].ToString());
                iCmd.Parameters.AddWithValue("@email", strsne[1].ToString());
                iCmd.Parameters.AddWithValue("@ueID", eID);
                iCmd.Parameters.AddWithValue("@org", uID);
                iCmd.Parameters.AddWithValue("comCode", genCode);

                SqlParameter mParam = new SqlParameter();
                mParam = iCmd.Parameters.Add(new SqlParameter("@emailTO", SqlDbType.Bit));
                mParam.Direction = ParameterDirection.Output;

                SqlParameter tParam = new SqlParameter();
                tParam = iCmd.Parameters.Add(new SqlParameter("@tIDOut", SqlDbType.Int));
                tParam.Direction = ParameterDirection.Output;

                iCmd.ExecuteNonQuery();
                //  Here is where the email is registered.
                string body = PopulateBody(strsne[0].ToString(), uID.ToString(), eLabel, eDescription, genCode.ToString(), tParam.Value.ToString(), eID.ToString());

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("events@i-underground.com");
                mail.To.Add(strsne[1].ToString());
                mail.Subject = eLabel;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                smtp.Credentials = cred;
                smtp.Send(mail);
            }
            con.Close();
            ecState.Value = "false";

            elbl.Text = "";
            street.Text = "";
            city.Text = "";
            province.Text = "";
            country.Text = "";
            pz.Text = "";
            datePicker.DateSelection = "";
            desc.Text = "";
            invited.Value = "";

            EventListing.SelectedIndex = -1;
            EventListing.DataSource = getEventList(uID);
            EventListing.DataBind();
        }

        private string PopulateBody(string name, string issuer, string lbl, string description, string genCode, string tmpUID, string eid)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/emailTemplates/emailInvitation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{FN}", name);
            body = body.Replace("{eLabel}", lbl);
            body = body.Replace("{Description}", description);
            body = body.Replace("{issuer}", issuer);
            body = body.Replace("{gencode}", genCode);
            body = body.Replace("{tuid}", (string.IsNullOrEmpty(tmpUID)) ? "-1" : tmpUID);
            body = body.Replace("{eventID}", eid);
            return body;
        }

        protected void EventListing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int eid = Convert.ToInt32(EventListing.DataKeys[e.Row.RowIndex].Value.ToString());
                CheckBoxList inList = e.Row.FindControl("EventInvites") as CheckBoxList;

                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand inv = new SqlCommand("event_Invites_sp", con);
                inv.CommandType = CommandType.StoredProcedure;
                inv.Parameters.AddWithValue("@eid", eid);
                SqlDataAdapter invDA = new SqlDataAdapter(inv);
                DataTable dtInv = new DataTable();
                invDA.Fill(dtInv);
                con.Close();
                if (dtInv.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInv.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["iFName"].ToString()) || (!string.IsNullOrEmpty(dr["iLName"].ToString())))
                        {
                            inList.Items.Add(new ListItem(dr["iFName"].ToString() + " " + dr["iLName"].ToString() + " RSVP: " + ((!(bool)dr["isRSVPD"]) ? "No, not yet." : "Yes"), dr["inviteID"].ToString()));
                        }
                    }
                }
            }
        }

        protected void mgmt_Command(object sender, CommandEventArgs e)
        {
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            GridViewRow gvr = lbtn.NamingContainer as GridViewRow;
            CheckBoxList cbl = gvr.FindControl("EventInvites") as CheckBoxList;

            cbl.SelectedIndex = -1;
            invitedMgmt2.Value = string.Empty;
        }

        protected string getEventLabel(int eID)
        {
            string strLbl = null;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_EventLbl_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eID", eID);
            SqlDataReader lbl = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (lbl.Read())
            {
                strLbl = lbl["Title"].ToString();
            }
            lbl.Close();
            return strLbl;
        }

        protected string getEventDescription(int eID)
        {
            string strDesc = null;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_EventDescription_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eID", eID);
            SqlDataReader Desc = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (Desc.Read())
            {
                strDesc = Desc["eventDesc"].ToString();
            }
            Desc.Close();
            return strDesc;
        }

        protected void sInvitations_Click(object sender, EventArgs e)
        {
            string strResult = null;

            int genCode = getRandomCode();
            char[] delimiter1 = new char[] { ',' };
            char[] delimiter2 = new char[] { ':' };

            LinkButton lbtn = (LinkButton)sender;
            GridViewRow gvr = lbtn.NamingContainer as GridViewRow;

            string invitedUsers = invitedMgmt2.Value;
            CheckBoxList cbl = gvr.FindControl("EventInvites") as CheckBoxList;
            int EIndex = Convert.ToInt32(EventListing.DataKeys[gvr.RowIndex].Value.ToString());

            int comIndex = invitedUsers.LastIndexOf(",");
            if (invitedUsers.Length != comIndex)
            {
                strResult = invitedUsers;
                string[] strUsers = strResult.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strUsers.Length; i++)
                {
                    string[] Users = strUsers[i].Split(delimiter2, StringSplitOptions.RemoveEmptyEntries);
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand iCmd = new SqlCommand("user_invite_sp", con);
                    iCmd.CommandType = CommandType.StoredProcedure;

                    iCmd.Parameters.AddWithValue("name", Users[0]);
                    iCmd.Parameters.AddWithValue("@email", Users[1]);
                    iCmd.Parameters.AddWithValue("@ueID", EIndex);
                    iCmd.Parameters.AddWithValue("@org", uID);
                    iCmd.Parameters.AddWithValue("@comCode", genCode);

                    SqlParameter mParam = new SqlParameter();
                    mParam = iCmd.Parameters.Add(new SqlParameter("@emailTO", SqlDbType.Bit));
                    mParam.Direction = ParameterDirection.Output;

                    SqlParameter tParam = new SqlParameter();
                    tParam = iCmd.Parameters.Add(new SqlParameter("@tIDOut", SqlDbType.Int));
                    tParam.Direction = ParameterDirection.Output;

                    iCmd.ExecuteNonQuery();
                    con.Close();

                    string body = PopulateBody(Users[0], uID.ToString(), getEventLabel(EIndex), getEventDescription(EIndex), genCode.ToString(), tParam.Value.ToString(), EIndex.ToString());

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("Events@i-underground.com");
                    mail.To.Add(Users[1]);
                    mail.Subject = getEventLabel(EIndex);
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                    NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                    smtp.Credentials = cred;
                    smtp.Send(mail);
                }
            }
            else
            {
                strResult = invitedUsers.Remove(comIndex);
                string[] strUsers = strResult.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strUsers.Length; i++)
                {
                    string[] Users = strUsers[i].Split(delimiter2, StringSplitOptions.RemoveEmptyEntries);
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand iCmd = new SqlCommand("user_invite_sp", con);
                    iCmd.CommandType = CommandType.StoredProcedure;

                    iCmd.Parameters.AddWithValue("name", Users[0]);
                    iCmd.Parameters.AddWithValue("@email", Users[1]);
                    iCmd.Parameters.AddWithValue("@ueID", EIndex);
                    iCmd.Parameters.AddWithValue("@org", uID);
                    iCmd.Parameters.AddWithValue("@comCode", genCode);

                    SqlParameter mParam = new SqlParameter();
                    mParam = iCmd.Parameters.Add(new SqlParameter("@emailTO", SqlDbType.Bit));
                    mParam.Direction = ParameterDirection.Output;

                    SqlParameter tParam = new SqlParameter();
                    tParam = iCmd.Parameters.Add(new SqlParameter("@tIDOut", SqlDbType.Int));
                    tParam.Direction = ParameterDirection.Output;

                    iCmd.ExecuteNonQuery();
                    con.Close();

                    string body = PopulateBody(Users[0], uID.ToString(), getEventLabel(EIndex), getEventDescription(EIndex), genCode.ToString(), tParam.Value.ToString(), EIndex.ToString());

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("Events@i-underground.com");
                    mail.To.Add(Users[1]);
                    mail.Subject = getEventLabel(EIndex);
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                    NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                    smtp.Credentials = cred;
                    smtp.Send(mail);
                }
            }
            foreach (ListItem cb in cbl.Items)
            {
                if (cb.Selected)
                {
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("remove_invite_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@euID", Convert.ToInt32(cb.Value.ToString()));
                    cmd.ExecuteNonQuery();
                }
            }
            cbl.Dispose();
        }

        protected void EventListing_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eIndex = Convert.ToInt32(EventListing.DataKeys[e.RowIndex].Value.ToString());
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("cancel_gevent_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eID", eIndex);
            cmd.Parameters.AddWithValue("@orgID", uID);
            cmd.ExecuteNonQuery();
            con.Close();

            EventListing.SelectedIndex = -1;
            EventListing.DataSource = getEventList(uID);
            EventListing.DataBind();
        }
    }
}