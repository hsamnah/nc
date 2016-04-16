using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace nc.userSet.venue
{
    public partial class rootMaster : System.Web.UI.MasterPage
    {
        private string con
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

        private string un;

        protected string UserName
        {
            get
            {
                SqlConnection conn = new SqlConnection(con);
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_Logged_Name_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", UIdentifier);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    un = dr["firstName"].ToString() + " " + dr["lastName"].ToString();
                }
                return un;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FriendList.DataSource = flDT(UIdentifier);
                FriendList.DataBind();
                loggedUser.Value = UserName;
            }
        }

        protected DataTable _fl = new DataTable();

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

        protected void signout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.RedirectPermanent("../default.aspx");
        }
    }
}