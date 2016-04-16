using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace nc.Controls
{
    public partial class Connect : System.Web.UI.UserControl
    {
        public int fId { get; set; }
        public int uID { get; set; }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsStartupScriptRegistered("connect"))
            {
                csm.RegisterClientScriptInclude("connect", "/scripts/jsCon.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            msgTb.Text = "";
        }

        protected void sendRequest_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("requestFriend_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fuIdentifier", fId);
            cmd.Parameters.AddWithValue("@uIdentifier", uID);
            cmd.Parameters.AddWithValue("@comment", msgTb.Text);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}