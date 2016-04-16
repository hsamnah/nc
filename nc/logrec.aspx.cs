using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class logRec : System.Web.UI.Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }
        private void SetCaptchaText()
        {
            Random oRandom = new Random();
            int iNumber = oRandom.Next(100000, 999999);
            Session["Captcha"] = iNumber.ToString();
        }
        protected void Page_Init(object sender,EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetCaptchaText();
            }
        }

        protected void SendConfirm_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            int intRan = ran.Next(10000, 99999);
            int intReturn = 0;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand eCmd = new SqlCommand("Confirm_EmailExist_sp", con);
            eCmd.CommandType = CommandType.StoredProcedure;
            eCmd.Parameters.AddWithValue("@email", email.Text);
            eCmd.Parameters.AddWithValue("@genCode", intRan);

            SqlParameter recParam = new SqlParameter();
            recParam = eCmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            recParam.Direction = ParameterDirection.ReturnValue;
            eCmd.ExecuteNonQuery();
            con.Close();
            intReturn = Convert.ToInt32(recParam.Value.ToString());
            if (intReturn > 0)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email.Text);
                mail.From = new MailAddress("recover@i-underground.com");
                mail.Subject = "The Underground Netwrk: Account Recovery";

                StringBuilder body = new StringBuilder();
                body.Append("Hello," + "\n\n");
                body.Append("Below, please find a link to follow in order to reset your account password by either clicking on the link or cutting" + "\n");
                body.Append("and pasting the full address to your browser address bar." + "\n");
                body.Append("Please note that after 3 days this reset request will expire and will no longer be available." + "\n");
                body.Append("\n");
                body.Append("\n");
                // intRan is the generated random number that is compared to the stored random number in the database.
                // intReturn is the recovery identifier that is compared to recID in the accRec_tbl table for recovery.  The user id is pulled from the field uid in accRec_tbl.
                body.Append("http://www.i-underground.com/reset.aspx?BlueSky=" + intRan.ToString() + "&interim=" + intReturn.ToString() + "\n");
                body.Append("If ths email has reached you by mistake or this email address does not belong to you" + "\n" + "please disregard and accept our apologies for any inconvenience." + "\n");

                mail.Body = body.ToString();
                SmtpClient smtp=new SmtpClient("mail.i-underground.com");
                NetworkCredential cred = new NetworkCredential("register@i-underground.com", "reg377a9p8U@6");
                smtp.Credentials = cred;
                smtp.Send(mail);

                SendConfirm.Visible = false;
                resultmsg.Visible = true;
                resultmsg.Text = "We have sent an email to the address you provided.  Please follow the link inorder to reset you account password.  Thank you.";
            }
            else
            {
                resultmsg.Visible = true;
                resultmsg.Text = "According to our records the email you provided does not exist.  Please try again or contact support.";
            }
        }

        protected void Verify_Click(object sender, EventArgs e)
        {
            string cTxt = Session["Captcha"].ToString();
            if (cTxt == capCompare.Text.Trim())
            {
                SendConfirm.Visible = true;
                resultmsg.Visible = false;
                capCompare.Visible = false;
                imgCaptcha.Visible = false;
                Verify.Visible = false;
            }
            else
            {
                resultmsg.Visible = true;
                resultmsg.Text = "Entry text and captcha text do not match. Please try again.";
                capCompare.Text = "";
                SetCaptchaText();
            }
        }
    }
}