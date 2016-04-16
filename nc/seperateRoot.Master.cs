using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace nc.root
{
    public partial class seperateRoot : System.Web.UI.MasterPage
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected int intResult;
        protected SqlParameter lParam;
        private string hashAlgorithm = "Clear";
        private string passwordToCompare;

        private void Page_Load(object sender, EventArgs e)
        {
            // There is 2 parts to every page.  1. A logged in page and 2. an unsecure page.
        }

        protected void login_Click(object sender, EventArgs e)
        {
            intResult = dbAuthenticate(uname.Text, pwd.Text, "User");
            if (intResult > 0)
            {
                if ((hashAlgorithm != null) && (hashAlgorithm != "Clear"))
                {
                    passwordToCompare = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, hashAlgorithm);
                }
                else
                {
                    passwordToCompare = pwd.Text;
                }
                var ticket = new FormsAuthenticationTicket(
                    1,
                    uname.Text,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    intResult.ToString()
                    );
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var formCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(formCookie);
                Response.Redirect("UserItems/Welcome.aspx", true);
            }
        }

        protected void vlogin_Click(object sender, EventArgs e)
        {
            if (dbAuthenticate(vname.Text, vpwd.Text, "Venue") > 0)
            {
            }
        }

        private int dbAuthenticate(string uname, string pwd, string state)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand lcmd = new SqlCommand("Authenticate_sp", conn);
            lcmd.CommandType = CommandType.StoredProcedure;

            lcmd.Parameters.AddWithValue("@un", uname);
            lcmd.Parameters.AddWithValue("@pwd", pwd);
            lcmd.Parameters.AddWithValue("@userType", state);

            lParam = lcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            lParam.Direction = ParameterDirection.ReturnValue;

            lcmd.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(lParam.Value.ToString());
        }
    }
}