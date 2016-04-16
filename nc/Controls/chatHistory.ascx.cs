using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class chatHistory : System.Web.UI.UserControl
    {
        public string userName { get; set; }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private DataTable chatUserList()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand fCmd = new SqlCommand("get_chatFriendList_sp", con);
            fCmd.CommandType = CommandType.StoredProcedure;
            fCmd.Parameters.AddWithValue("@yourUName", userName);
            SqlDataAdapter fDA = new SqlDataAdapter(fCmd);
            con.Close();
            fDA.Fill(dt);
            return dt;
        }

        private DataTable cmsgs(string friendsName)
        {
            DataTable fdt = new DataTable();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand msgcmd = new SqlCommand("get_chatMsgs_sp", con);
            msgcmd.CommandType = CommandType.StoredProcedure;
            msgcmd.Parameters.AddWithValue("@yourUName", userName);
            msgcmd.Parameters.AddWithValue("@friendUName", friendsName);
            msgcmd.Parameters.AddWithValue("@Destination", "messages");
            SqlDataAdapter msgDA = new SqlDataAdapter(msgcmd);
            con.Close();
            msgDA.Fill(fdt);

            return fdt;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("ch"))
            {
                csm.RegisterClientScriptInclude("ch", "/scripts/chScript.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChatUsers.DataSource = chatUserList();
                ChatUsers.DataBind();
            }
        }

        protected void ChatUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = true;
                string user = ChatUsers.DataKeys[e.Row.RowIndex].Value.ToString();
                DataList msgDL = (DataList)e.Row.FindControl("Messages");
                msgDL.DataSource = cmsgs(user);
                msgDL.DataBind();
            }
        }
    }
}