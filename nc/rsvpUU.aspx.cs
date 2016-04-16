using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace nc
{
    public partial class rsvpuu : System.Web.UI.Page
    {
        private int gc
        {
            get { return Convert.ToInt32(Request.QueryString["gc"]); }
        }

        private int tuid
        {
            get { return Convert.ToInt32(Request.QueryString["tuid"]); }
        }

        private int eid
        {
            get { return Convert.ToInt32(Request.QueryString["eventID"]); }
        }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int storedGC
        {
            get
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_ConfirmationCode_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tmpUI", tuid);
                SqlParameter tParam = new SqlParameter();
                tParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                tParam.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                con.Close();
                return Convert.ToInt32(tParam.Value.ToString());
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

        private int issuer
        {
            get { return Convert.ToInt32(Request.QueryString["issue"]); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (tuid == -1)
            {
                Response.Redirect("/default.aspx?Event=" + eid + "&issue=" + issuer);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void User_register_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (storedGC == gc)
                {
                    string bdate = Year.Items[Year.SelectedIndex].Value + "-" + Month.Items[Month.SelectedIndex].Value + "-" + Day.Items[Day.SelectedIndex].Value;
                    string _un = un.Text;
                    string _pwd = pwd1.Text;
                    string _sq = sq.Text;
                    string _sa = sa.Text;
                    string _sh = sh.Text;
                    string _ip = ip;

                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("createUser_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tmpUID", tuid);
                    cmd.Parameters.AddWithValue("@bday", bdate);
                    cmd.Parameters.AddWithValue("@un", _un);
                    cmd.Parameters.AddWithValue("@pwd", _pwd);
                    cmd.Parameters.AddWithValue("@sq", _sq);
                    cmd.Parameters.AddWithValue("@sa", _sa);
                    cmd.Parameters.AddWithValue("@sh", _sh);
                    cmd.Parameters.AddWithValue("@ip", _ip);

                    SqlParameter uParam = new SqlParameter();
                    uParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                    uParam.Direction = ParameterDirection.ReturnValue;

                    cmd.ExecuteNonQuery();
                    con.Close();
                    int intResult = Convert.ToInt32(uParam.Value.ToString());
                    if (intResult > 0)
                    {
                        con.Open();
                        SqlCommand cmdInvite = new SqlCommand("send_EventInvites_sp", con);
                        cmdInvite.CommandType = CommandType.StoredProcedure;
                        cmdInvite.Parameters.AddWithValue("@uid", issuer);
                        cmdInvite.Parameters.AddWithValue("@fid", intResult);
                        cmdInvite.Parameters.AddWithValue("@eid", eid);
                        cmdInvite.ExecuteNonQuery();
                        // Remove temporary invite table.
                        SqlCommand tcmd = new SqlCommand("clean_tmpUInvites_sp", con);
                        tcmd.CommandType = CommandType.StoredProcedure;
                        tcmd.Parameters.AddWithValue("@tuid", tuid);
                        tcmd.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("validation.aspx?dent=" + intResult.ToString() + "&isConfirmed=1");
                    }
                    else
                    {
                        errMsg.Text = "Registration incomplete. Please make sure all required fields have been filled in and try again.";
                    }
                }
                else
                {
                    errMsg.Text = "Your confirmation code does not match the confirmation code on file.  A notice has been sent to the issuer of this invitation.  If an invitation is not sent to you within a few days please contact the issuer of this invitation.";
                }
            }
            else
            {
                errMsg.Text = "Marked fields are required.";
            }
        }
    }
}