using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace nc.UserItems
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string qsUiResolve
        {
            get { return Request.QueryString["uiResolve"]; }
        }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int ui
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
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

        private void addIP()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmdip = new SqlCommand("add_ipList_sp", con);
            cmdip.CommandType = CommandType.StoredProcedure;
            cmdip.Parameters.AddWithValue("@uid", ui);
            cmdip.Parameters.AddWithValue("@ip", ip);
            cmdip.ExecuteNonQuery();
            con.Close();
        }
        private string getSecQuestion(int id)
        {
            string secQ;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_secQ_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", id);
            SqlParameter Param = new SqlParameter();
            Param = cmd.Parameters.Add(new SqlParameter("@secQ", SqlDbType.VarChar, 255));
            Param.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            con.Close();
            secQ = Param.Value.ToString();
            return secQ;
        }
        private string secAnswered(string answer)
        {
            SqlParameter aParam = new SqlParameter();
            SqlParameter bParam = new SqlParameter();
            int intResult = new int();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_secAnswerHint_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", ui);
            cmd.Parameters.AddWithValue("@answer", secA.Text);
            aParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            aParam.Direction = ParameterDirection.ReturnValue;
            bParam = cmd.Parameters.Add(new SqlParameter("@ah", SqlDbType.VarChar, 8000));
            bParam.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            con.Close();
            intResult = Convert.ToInt32(aParam.Value.ToString());
            if (intResult == 0)
            {
                return bParam.Value.ToString();
            }
            else
            {
                addIP();
                Response.Redirect(qsUiResolve, true);
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SecQ.Text = getSecQuestion(ui);
        }

        protected void ip_register_Click(object sender, EventArgs e)
        {
            secHint.Text = "Hint: " + secAnswered(secA.Text);
        }
    }
}