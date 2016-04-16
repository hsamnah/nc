using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.UserItems
{
    public partial class vGuestServices : System.Web.UI.Page
    {
        private string vID
        {
            get { return Request.QueryString["gsv"]; }
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

        private SqlParameter vNParam;
        private SqlParameter vBParam;

        private string getVItem(string itemType)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_venueName_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vid", vID);

            vNParam = cmd.Parameters.Add(new SqlParameter("@vN", SqlDbType.VarChar, 8000));
            vNParam.Direction = ParameterDirection.Output;
            vBParam = cmd.Parameters.Add(new SqlParameter("@vB", SqlDbType.VarChar, 8000));
            vBParam.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            con.Close();

            if (itemType == "venueName")
            {
                return vNParam.Value.ToString();
            }
            else if (itemType == "venueBanner")
            {
                return vBParam.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                venueName.Text = getVItem("venueName");
                vBanner.ImageUrl = (string.IsNullOrEmpty(getVItem("venueBanner"))) ? "/imgs/vbna_full.jpg" : getVItem("venueBanner");

                friendList_chkbxlist.DataSource = dt();
                friendList_chkbxlist.DataTextField = "FullName";
                friendList_chkbxlist.DataValueField = "fID";
                friendList_chkbxlist.DataBind();
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

        protected DataTable invited(int eventID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("invites_SentTo_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eid", eventID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);
            return dt;
        }

        protected void DaysinOp_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Cell.Text = "";
                e.Day.IsSelectable = false;
            }
            string dayName = Enum.GetName(typeof(DayOfWeek), e.Day.Date.DayOfWeek);

            char[] delimiter = { ',' };
            string[] daysOpen = dOpen(Convert.ToInt32(vID)).Split(delimiter);
            foreach (string strDay in daysOpen)
            {
                if (dayName == strDay)
                {
                    e.Day.IsSelectable = true;
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(56, 70, 82);
                    e.Cell.ForeColor = System.Drawing.Color.White;
                }
                if (e.Day.IsSelected)
                {
                    e.Cell.BackColor = Color.FromArgb(36, 50, 62);
                    e.Cell.ForeColor = Color.White;
                }
            }
        }

        private SqlParameter daysParam;

        private String dOpen(int vi)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();

            SqlCommand cmd = new SqlCommand("get_days_Open_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vID", vID);
            daysParam = cmd.Parameters.Add(new SqlParameter("@DaysOpen", SqlDbType.VarChar, 8000));
            daysParam.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            con.Close();

            return daysParam.Value.ToString();
        }

        protected void sendInvites_Click(object sender, EventArgs e)
        {
            SqlParameter eParam;
            int uid = uIdentifier;
            string EventDay = DaysinOp.SelectedDate.ToShortDateString();
            bool isPublic = (!ipub.Checked) ? false : true;
            string eLbl = eventLbl.Text;
            string eDesc = EventDescription.Text;
            string cmnt = comments.Text;

            string h = timeControl.eh;
            string m = timeControl.em;
            string s = timeControl.es;
            string ed = timeControl.dp;
            string eventTime = h + ":" + m + ":" + s + ed;

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand inCmd = new SqlCommand("create_user_event_sp", con);
            inCmd.CommandType = CommandType.StoredProcedure;

            inCmd.Parameters.AddWithValue("@title", eLbl);
            inCmd.Parameters.AddWithValue("@eDate", EventDay);
            inCmd.Parameters.AddWithValue("@eTime", eventTime);
            inCmd.Parameters.AddWithValue("@eDesc", eDesc);
            inCmd.Parameters.AddWithValue("@public", isPublic);
            inCmd.Parameters.AddWithValue("@vID", Convert.ToInt32(vID));

            eParam = inCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            eParam.Direction = ParameterDirection.ReturnValue;

            inCmd.ExecuteNonQuery();
            con.Close();

            int eventID = Convert.ToInt32(eParam.Value.ToString());
            foreach (ListItem chk in friendList_chkbxlist.Items)
            {
                if (chk.Selected)
                {
                    con.Open();
                    SqlCommand fCmd = new SqlCommand("send_EventInvites_sp", con);
                    fCmd.CommandType = CommandType.StoredProcedure;

                    fCmd.Parameters.AddWithValue("@uid", uIdentifier);
                    fCmd.Parameters.AddWithValue("@fid", chk.Value.ToString());
                    fCmd.Parameters.AddWithValue("@eid", eventID);
                    fCmd.Parameters.AddWithValue("@comments", cmnt);

                    fCmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            EventDescription.Text = "";
            comments.Text = "";
            eventLbl.Text = "";
            ipub.Checked = false;

            // show friends who have been invited.

            Invites.DataSource = invited(eventID);
            Invites.DataBind();
        }
    }
}