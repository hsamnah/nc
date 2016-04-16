using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

namespace nc.root
{
    public partial class register : System.Web.UI.Page
    {
        protected string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private static Random rand = new Random();

        protected int getRandomCode()
        {
            int intR = new int();
            for (int i = 0; i < 1; i++)
            {
                intR = rand.Next(1000, 9999);
            }
            return intR;
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void User_register_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if ((Convert.ToInt32(Year.Items[Year.SelectedIndex].Value) == 0) ||
                    (Convert.ToInt32(Month.Items[Month.SelectedIndex].Value) == 0) ||
                    (Convert.ToInt32(Day.Items[Day.SelectedIndex].Value) == 0))
                {
                    errMsg.Text = "Your date of birth is required.";
                    errMsg.Visible = true;
                }
                else
                {
                    string bdate = Year.Items[Year.SelectedIndex].Value + "-" + Month.Items[Month.SelectedIndex].Value + "-" + Day.Items[Day.SelectedIndex].Value;
                    string uname = un.Text;
                    string _em = email.Text;
                    string _pwd = pwd1.Text;
                    string _sq = sq.Text;
                    string _sa = sa.Text;
                    string _sh = sh.Text;
                    string genCode = getRandomCode().ToString();

                    SqlParameter uParam = new SqlParameter();
                    SqlParameter pResult = new SqlParameter();

                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("createTMP_User_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bday", bdate);
                    cmd.Parameters.AddWithValue("@un", uname);
                    cmd.Parameters.AddWithValue("@email", _em);
                    cmd.Parameters.AddWithValue("@pwd", _pwd);
                    cmd.Parameters.AddWithValue("@sq", _sq);
                    cmd.Parameters.AddWithValue("@sa", _sa);
                    cmd.Parameters.AddWithValue("@sh", _sh);
                    cmd.Parameters.AddWithValue("@ip", ip);
                    cmd.Parameters.AddWithValue("@confirmationCode", genCode);

                    pResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                    pResult.Direction = ParameterDirection.ReturnValue;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    int intResult = new int();
                    intResult = Convert.ToInt32(pResult.Value.ToString());
                    if (intResult > 0)
                    {
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress("register@i-underground.com");
                        mail.To.Add(_em);
                        mail.Subject = "The Underground validation";

                        StringBuilder regBody = new StringBuilder();
                        regBody.Append(@"Welcome to The Underground Network.  The future of evening entertainment is upon us." + "\n\n");
                        regBody.Append("\n");
                        regBody.Append(@"Please click on the link below to validate your registration with i-Underground.com:" + "\n");
                        regBody.Append(@"http://www.i-underground.com/validation.aspx?dent=" + intResult + "&gc=" + genCode + "\n");
                        regBody.Append("\n");
                        regBody.Append(@"Regards," + "\n");
                        regBody.Append(@"Registration, The Underground-" + "\n\n");
                        regBody.Append(@"If ths email has been sent to your email address by mistake or this email address does not belong to you" + "\n" + "please disregard and accept our apologies for any inconvenience." + "\n");

                        mail.Body = regBody.ToString();

                        SmtpClient smtp = new SmtpClient("mail.i-underground.com");
                        NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                        smtp.Credentials = cred;
                        smtp.Send(mail);

                        sMsg.Visible = true;
                        sTR.Visible = true;
                        sMsg.Text = "Thank you for filling out this form.  Please check your email and follow the attached link to complete registration of your account.";
                    }
                    else
                    {
                        errMsg.Text = "Transmission error. Please try again.";
                        errMsg.Visible = true;
                    }
                }
            }
        }

        private int intUnCheck(string uname)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("checkUN_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@un", uname);
            SqlParameter cParam = new SqlParameter();
            cParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            cParam.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(cParam.Value.ToString());
        }

        private int intEmailCheck(string email)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("checkEmail_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", email);

            SqlParameter eParam = new SqlParameter();
            eParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            eParam.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(eParam.Value.ToString());
        }

        protected void checkuser_Change(object sender, EventArgs e)
        {
            if (intUnCheck(un.Text) <= 0)
            {
                unCheck.Text = "Username is available.";
                unCheck.Visible = true;
                un.BackColor = System.Drawing.Color.White;
            }
            else
            {
                unCheck.Text = "Username is not available. Please try another.";
                unCheck.Visible = true;
                un.Text = "";
                un.BackColor = System.Drawing.Color.Beige;
                un.Focus();
            }
        }

        protected void email_chk(object sender, EventArgs e)
        {
            if (intEmailCheck(email.Text) <= 0)
            {
                emaillbl.Visible = false;
                email.BackColor = System.Drawing.Color.White;
            }
            else
            {
                emaillbl.Text = "This email address is already in our records. Please sign in.";
                emaillbl.Visible = true;
                email.Text = "";
                email.BackColor = System.Drawing.Color.Beige;
                email.Focus();
            }
        }
    }
}