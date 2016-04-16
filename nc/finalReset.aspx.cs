using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class finalReset: Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }
        private string recID
        {
            get { return Request.QueryString["a"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand uCmd = new SqlCommand("pwd_reset_sp", con);
                uCmd.CommandType = CommandType.StoredProcedure;
                uCmd.Parameters.AddWithValue("@recID", recID);
                uCmd.Parameters.AddWithValue("@pwd", npwd.Text.Trim());
                SqlParameter param = new SqlParameter();
                param = uCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                param.Direction = ParameterDirection.ReturnValue;
                uCmd.ExecuteNonQuery();
                con.Close();
                int intResult = Convert.ToInt32(param.Value.ToString());
                if (intResult == 1)
                {
                    Server.Transfer("default.aspx?reset=true", true);
                }
                else
                {
                    resultmsg.Visible = true;
                    resultmsg.Text = "There has been a problem transmitting the update.  Please try again.";
                }
            }
            else
            {
                resultmsg.Visible = true;
                resultmsg.Text = "Please make sure both fields are the same and contain a value.";
            }
        }
    }
}