using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

namespace nc
{
    public partial class vregistration : System.Web.UI.Page
    {
        private int queryTmpID
        {
            get { return Convert.ToInt32(Request.QueryString["dent"]); }
        }

        private int vCode
        {
            get { return Convert.ToInt32((string.IsNullOrEmpty(Request.QueryString["gc"])) ? "0" : Request.QueryString["gc"]); }
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

        protected string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        // prevent duplicate venue names
        private SqlParameter pResult;

        private bool isUnique(string un)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("checkUN_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@un", un);
            pResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int, 4));
            pResult.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            con.Close();
            if (Convert.ToInt32(pResult.Value.ToString()) <= 0)
            {
                pResult.Value = null;
                return true;
            }
            else
            {
                pResult.Value = null;
                return false;
            }
        }

        private bool isEmailUnique(string email)
        {
            SqlParameter isUniqueEmail = new SqlParameter();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand emailCmd = new SqlCommand("isEmailUnique_sp", con);
            emailCmd.CommandType = CommandType.StoredProcedure;

            emailCmd.Parameters.AddWithValue("@email", email);
            isUniqueEmail = emailCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Bit));
            isUniqueEmail.Direction = ParameterDirection.ReturnValue;
            emailCmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToBoolean(isUniqueEmail.Value);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("rPwd"))
            {
                csm.RegisterClientScriptInclude("rPwd", "/scripts/jsRegister.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Venue_register_Click(object sender, EventArgs e)
        {
            if (isEmailUnique(email.Text))
            {
                if (Page.IsValid)
                {
                    SqlParameter param = new SqlParameter();
                    int tmpID = new int();
                    string vN = vName.Text;
                    string puName = un.Text;
                    string pemail = email.Text;
                    string pwd = pwd1.Text;
                    string vsq = sq.Text;
                    string vsa = sa.Text;
                    string vsh = sh.Text;
                    int RandCode = getRandomCode();

                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert_Venue_tmp_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@venueName", vN);
                    cmd.Parameters.AddWithValue("@vun", puName);
                    cmd.Parameters.AddWithValue("@emailAdd", pemail);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.Parameters.AddWithValue("@secQ", vsq);
                    cmd.Parameters.AddWithValue("@secA", vsa);
                    cmd.Parameters.AddWithValue("@secH", vsh);
                    cmd.Parameters.AddWithValue("@conCode", RandCode);
                    param = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                    param.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    con.Close();

                    tmpID = Convert.ToInt32(param.Value.ToString());
                    if (tmpID > 0)
                    {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("register@i-underground.com");
                        mail.To.Add(pemail);
                        mail.Subject = "The Underground Validation.";
                        StringBuilder vRegBody = new StringBuilder();
                        vRegBody.Append(@"Welcome to The Underground Network.  The future of evening entertainment is upon us." + "\n");
                        vRegBody.Append("\n");
                        vRegBody.Append(@"Please click on the link below to validate your registration with i-underground.com:" + "\n");
                        vRegBody.Append(@"http://www.i-underground.com/vRegCon.aspx?dent=" + tmpID + "&gc=" + RandCode + "\n");
                        vRegBody.Append(@"\n");
                        vRegBody.Append(@"Regards," + "\n");
                        vRegBody.Append(@"Registration, The Underground Network-" + "\n\n");
                        vRegBody.Append(@"If this email has been sent to your email address by mistake or this email address does not belong to you" + "\n" + "please disregard and accept our apologies for any inconvenience." + "\n");
                        mail.Body = vRegBody.ToString();
                        SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                        NetworkCredential vcred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                        smtp.Credentials = vcred;
                        smtp.Send(mail);
                        sMsg.Text = "An email has been sent to: " + pemail + ". Please click on the provided link to validate your registration.";
                    }
                }
            }
            else
            {
                errMsg.Text = "Your venues email address already exists in our records.";
            }
        }
    }
}