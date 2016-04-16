using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace nc.userSet
{
    public partial class rootMaster : System.Web.UI.MasterPage
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected int UserIdentification
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        private string un;

        protected string UserName
        {
            get
            {
                SqlConnection conn = new SqlConnection(con);
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_Logged_Name_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", UserIdentification);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    un = dr["firstName"].ToString() + ":" + dr["lastName"].ToString() + ":" + dr["UName"].ToString();
                }
                dr.Close();
                return un;
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FriendList.DataSource = flDT(UserIdentification);
                FriendList.DataBind();

                string[] Udata = UserName.Split(':');
                loggedUser.Value = Udata[2].ToLower();
                myUname.Value = Udata[2];

                StringBuilder ud_sb = new StringBuilder();
                ud_sb.Append("[{\"uid\":\"" + UserIdentification.ToString() + "\",\"uName\":\"" + Udata[2] + "\",\"Fname\":\"" + Udata[0] + "\",\"Lname\":\"" + Udata[1] + "\"}]");
                currentUser.Value = ud_sb.ToString();
            }
            
            StringBuilder fl_sb = new StringBuilder();
            fl_sb.Append("\"Friends\":[");
            DataRowCollection drc = fl2(UserIdentification).Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                if (i != 0)
                {
                    fl_sb.Append(",{\"Friend\":\"" + drc[i].Field<string>("fn") + "_" + drc[i].Field<string>("ln") + "_" + drc[i].Field<int>("userID").ToString() + "\"}");
                }
                else
                {
                    fl_sb.Append("{\"Friend\":\"" + drc[i].Field<string>("fn") + "_" + drc[i].Field<string>("ln") + "_" + drc[i].Field<int>("userID").ToString() + "\"}");
                }
            }
            drc.Clear();
            fl_sb.Append("]");
            lUFriends.Value = fl_sb.ToString();
        }
        
        protected DataTable _fl = new DataTable();
        // Use this DataTable to get the user name and identifier.
        protected DataTable flDT(int userID)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand flCmd = new SqlCommand("get_FriendList_sp", conn);
            flCmd.CommandType = CommandType.StoredProcedure;

            flCmd.Parameters.AddWithValue("@userID", userID);

            SqlDataAdapter flDA = new SqlDataAdapter(flCmd);
            flDA.Fill(_fl);
            conn.Close();
            return _fl;
        }
        private DataTable _fl2 = new DataTable();
        protected DataTable fl2(int uid)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand flCmd = new SqlCommand("get_FriendList_sp", conn);
            flCmd.CommandType = CommandType.StoredProcedure;

            flCmd.Parameters.AddWithValue("@userID", uid);

            SqlDataAdapter flDA = new SqlDataAdapter(flCmd);
            flDA.Fill(_fl2);
            conn.Close();
            return _fl2;
        }
        protected void signout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("../default.aspx", true);
        }

        protected void get_chatHistory_dBound(object sender, DataListItemEventArgs e)
        {
            string friendUN = ((HtmlInputHidden)e.Item.FindControl("SelectedFriend")).Value;
            string[] udata = UserName.Split(':');
            string User = udata[2];
            HtmlInputHidden chatContainer = ((HtmlInputHidden)e.Item.FindControl("fc"));

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand getChat = new SqlCommand("get_chatMsgs_sp", conn);
            getChat.CommandType = CommandType.StoredProcedure;
            getChat.Parameters.AddWithValue("@yourUName", User);
            getChat.Parameters.AddWithValue("@friendUName", friendUN);
            getChat.Parameters.AddWithValue("@Destination", "chat");

            SqlDataReader chatDR = getChat.ExecuteReader(CommandBehavior.CloseConnection);

            StringBuilder chat = new StringBuilder();
            while (chatDR.Read())
            {
                chat.Append("<span class=\"msgName\">" + chatDR["incoming"].ToString() + "</span><br />");
                chat.Append("<span class\"chatMessage\">" + chatDR["msg"].ToString() + "</span><br />");
            }
            chatContainer.Value = chat.ToString();
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            string[] rec = recievers.Text.Split(new char[','],StringSplitOptions.RemoveEmptyEntries);
            foreach (string reciever in rec)
            {
                int genCode = getRandomCode();
                string[] cUser = UserName.Split(':');
                string body = populateBody(cUser[0] + " " + cUser[1], cUser[2]);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("register@i-underground.com");
                mail.To.Add(reciever);
                mail.Subject = "Invitation to join The Underground Network";
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                smtp.Credentials = cred;
                smtp.Send(mail);
            }
        }
        private string populateBody(string senderName, string userName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/emailTemplates/userMembershipInvite.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{senderName}", senderName);
            body = body.Replace("{userName}", userName);
            return body;
        }
    }
}