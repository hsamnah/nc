using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace nc.UserItems
{
    public partial class guestServices : System.Web.UI.Page
    {
        private string eventID
        {
            get { return Request.QueryString["m"]; }
        }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int uIdentifier
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                friends2Invite.DataSource = dt();
                friends2Invite.DataTextField = "FullName";
                friends2Invite.DataValueField = "fID";
                friends2Invite.DataBind();

                sEvent.DataSource = idt();
                sEvent.DataBind();

                InvitesSent.DataSource = edt();
                InvitesSent.DataBind();
            }
        }

        private DataTable _dt = new DataTable();

        protected DataTable dt()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_Friendlist4Invite_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("uid", uIdentifier);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(_dt);
            _dt.Columns.Add("FullName", typeof(string), "FirstName + ' ' + LastName");
            return _dt;
        }

        private DataTable _idt = new DataTable();

        protected DataTable idt()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand iCmd = new SqlCommand("get_selectedEvent_sp", con);
            iCmd.CommandType = CommandType.StoredProcedure;

            iCmd.Parameters.AddWithValue("@veid", eventID);

            SqlDataAdapter iDA = new SqlDataAdapter(iCmd);
            con.Close();
            iDA.Fill(_idt);
            return _idt;
        }

        private DataTable _edt = new DataTable();

        protected DataTable edt()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand ecmd = new SqlCommand("get_eventInvited_sp", con);
            ecmd.CommandType = CommandType.StoredProcedure;

            ecmd.Parameters.AddWithValue("@uid", uIdentifier);
            ecmd.Parameters.AddWithValue("@eid", eventID);

            SqlDataAdapter eDA = new SqlDataAdapter(ecmd);
            eDA.Fill(_edt);
            return _edt;
        }

        public string getEventImgs(object Directory, object Name)
        {
            string dir = DataBinder.Eval(Directory, "imgDir").ToString();
            string name = DataBinder.Eval(Name, "imgName").ToString();
            int rPathIndex = dir.LastIndexOf("eventImages");
            string rPath = dir.Remove(0, rPathIndex).Replace(@"\", "/");
            // Replace \ with / and correct path to include ../Venues
            return "/Venues/" + rPath + "/" + name;
        }

        protected void sendInvites_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            for (int i = 0; i < friends2Invite.Items.Count; i++)
            {
                if (friends2Invite.Items[i].Selected)
                {
                    SqlCommand iecmd = new SqlCommand("send_EventInvites_sp", con);
                    iecmd.CommandType = CommandType.StoredProcedure;

                    iecmd.Parameters.AddWithValue("@uid", uIdentifier);
                    iecmd.Parameters.AddWithValue("@fid", Convert.ToInt32(friends2Invite.Items[i].Value.ToString()));
                    iecmd.Parameters.AddWithValue("@eid", eventID);
                    iecmd.Parameters.AddWithValue("@Comments", tbox.Text);
                    iecmd.ExecuteNonQuery();
                }
            }
            con.Close();

            tbox.Text = "";
            InvitesSent.SelectedIndex = -1;
            InvitesSent.DataSource = edt();
            InvitesSent.DataBind();
        }
    }
}