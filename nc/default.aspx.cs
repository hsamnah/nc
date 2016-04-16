using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace nc.root
{
    public partial class defaultroot : System.Web.UI.Page
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private bool userComplete
        {
            get { return (!string.IsNullOrEmpty(Request.QueryString["rt"])) ? Convert.ToBoolean(Request.QueryString["rt"]) : false; }
        }

        protected int intResult;
        protected int intVResult;
        protected SqlParameter lParam;
        protected SqlParameter vParam;
        protected SqlParameter vTypeParam;
        private string hashAlgorithm = "Clear";
        private string passwordToCompare;
        private string UserType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                g_blog.DataSource = gb();
                g_blog.DataBind();
                if (userComplete)
                {
                    regCom.Text = "Welcome!  You will be able to configure your settings upon logging in.<br />";
                    regCom.Visible = true;
                }
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            UserType = uType.Items[uType.SelectedIndex].Value;
            if (UserType == "User")
            {
                intResult = dbAuthenticate(un.Text, pwd.Text, UserType);
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
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        un.Text,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        intResult.ToString()
                        );
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie formCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(formCookie);
                    if (HttpContext.Current.Request.Url.Query.Length > 0)
                    {
                        Response.Redirect("/UserItems/UserWelcome.aspx" + HttpContext.Current.Request.Url.Query, true);
                    }
                    else
                    {
                        Response.Redirect("/UserItems/UserWelcome.aspx", true);
                    }
                }
                else
                {
                    selectUtype.Visible = true;
                    selectUtype.Text = "Login failed. Please try again.";
                    accessCor.Visible = true;
                    accessCor.Text = "<br />Having trouble logging in?";
                    pwdRec.Visible = true;
                }
            }
            else if (UserType == "Venue")
            {
                intVResult = dbVAuthenticate(un.Text, pwd.Text);
                if (intVResult > 0)
                {
                    if ((hashAlgorithm != null) && (hashAlgorithm != "Clear"))
                    {
                        passwordToCompare = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Text, hashAlgorithm);
                    }
                    else
                    {
                        passwordToCompare = pwd.Text;
                    }
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        un.Text,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        intVResult.ToString()
                        );
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie formCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(formCookie);
                    if (HttpContext.Current.Request.Url.Query.Length > 0)
                    {
                        Response.Redirect("/Venues/VenueWelcome.aspx" + HttpContext.Current.Request.Url.Query, true);
                    }
                    else
                    {
                        Response.Redirect("/Venues/VenueWelcome.aspx", true);
                    }
                }
                else
                {
                    selectUtype.Visible = true;
                    selectUtype.Text = "Login failed. Please try again.";
                    accessCor.Visible = true;
                    accessCor.Text = "<br />Having trouble logging in?";
                    pwdRec.Visible = true;
                }
            }
            else
            {
                selectUtype.Visible = true;
                selectUtype.Text = "Please select a user type and try again.";
            }
        }

        private int dbAuthenticate(string uname, string lpwd, string state)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand lcmd = new SqlCommand("Authenticate_sp", conn);
            lcmd.CommandType = CommandType.StoredProcedure;

            lcmd.Parameters.AddWithValue("@un", uname);
            lcmd.Parameters.AddWithValue("@pwd", lpwd);
            lcmd.Parameters.AddWithValue("@userType", state);

            lParam = lcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            lParam.Direction = ParameterDirection.ReturnValue;

            lcmd.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(lParam.Value.ToString());
        }

        private int dbVAuthenticate(string uname, string vpwd)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand vcmd = new SqlCommand("vAuthenticate_sp", conn);
            vcmd.CommandType = CommandType.StoredProcedure;

            vcmd.Parameters.AddWithValue("@un", uname);
            vcmd.Parameters.AddWithValue("@pwd", vpwd);

            vParam = vcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            vParam.Direction = ParameterDirection.ReturnValue;

            vcmd.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(vParam.Value.ToString());
        }

        private string desc;
        private string rdesc;
        private string rem;
        private int count;

        public string getDesc(object data)
        {
            desc = DataBinder.Eval(data, "BD").ToString();
            count = desc.Length;
            if (desc.Length > 150)
            {
                rem = desc.Remove(150);
                rdesc = rem + "...";
            }
            else
            {
                rdesc = desc;
            }
            return rdesc;
        }

        private DataTable _gb = new DataTable();

        public DataTable gb()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand gbCmd = new SqlCommand("get_general_blog_sp", conn);
            gbCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter gda = new SqlDataAdapter(gbCmd);
            gda.Fill(_gb);
            conn.Close();
            return _gb;
        }

        protected void pwdRec_Click(object sender, EventArgs e)
        {
            Server.Transfer("logrec.aspx", false);
        }
    }
}