using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;

namespace nc
{
    public partial class vRegCon : System.Web.UI.Page
    {
        private string tempID
        {
            get { return Request.QueryString["dent"]; }
        }

        private int generatedCode
        {
            get { return Convert.ToInt32(Request.QueryString["gc"]); }
        }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int storedVID = new int();
        private string vName;

        private int getGenCode2Compare(int tempID)
        {
            int storedCode = new int();
            SqlParameter paramResult = new SqlParameter();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_venueTmp4Code_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tmpID", tempID);
            paramResult = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            paramResult.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            storedCode = Convert.ToInt32(paramResult.Value.ToString());
            if (storedCode == -1)
            {
                Response.RedirectPermanent("vRegistry.aspx");
                return storedCode;
            }
            else
            {
                return storedCode;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(tempID))
                {
                    Response.RedirectPermanent("vRegistry.aspx");
                }
                else
                {
                    if (generatedCode == getGenCode2Compare(Convert.ToInt32(tempID)))
                    {
                        SqlConnection con = new SqlConnection(conn);
                        con.Open();
                        SqlCommand tvcmd = new SqlCommand("transfer_tmp2venue", con);
                        tvcmd.CommandType = CommandType.StoredProcedure;
                        tvcmd.Parameters.AddWithValue("@tmpID", tempID);

                        SqlParameter vParam = new SqlParameter();
                        vParam = tvcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                        vParam.Direction = ParameterDirection.ReturnValue;

                        SqlParameter vNameParam = new SqlParameter();
                        vNameParam = tvcmd.Parameters.Add(new SqlParameter("@venueName", SqlDbType.VarChar, 255));
                        vNameParam.Direction = ParameterDirection.Output;

                        tvcmd.ExecuteNonQuery();
                        con.Close();

                        Session["vid"] = Convert.ToInt32(vParam.Value.ToString());
                        vName = vNameParam.Value.ToString();

                        string dpath = "Venues/venueUploads/" + vName + "/";
                        if (!Directory.Exists(Server.MapPath(dpath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(dpath));
                            con.Open();
                            SqlCommand dcmd = new SqlCommand("set_Venue_DIrectory_sp", con);
                            dcmd.CommandType = CommandType.StoredProcedure;

                            dcmd.Parameters.AddWithValue("@vid", (int)Session["vid"]);
                            dcmd.Parameters.AddWithValue("@vDir", dpath);

                            dcmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "var dname=prompt('You already have a directory assigned to your venue. Please provide a new directory name:", true);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Venue_register_Click(object sender, EventArgs e)
        {
            if ((int)Session["vid"] <= 0)
            {
                Err.Visible = true;
                Err.Text = Session["vid"].ToString();
            }
            else {
                string summary = sDesc.Text;
                string fullDesc = fDesc.Text;
                int vIdentifier = (int)Session["vid"];

                string ws = website.Text;
                string _genre = genre.Text;
                string _days = daysOpen.Text;
                string _time = Hours.SelectedValue + ":" + Minutes.SelectedValue + ":" + Seconds.SelectedValue + " " + DayPart.SelectedValue;
                int _cap = Convert.ToInt32(cap.Text);
                int _age = Convert.ToInt32(ageLimit.Text);
                string dcode = DressCode.Text;

                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand fsCmd = new SqlCommand("insert_VDescriptions_sp", con);
                fsCmd.CommandType = CommandType.StoredProcedure;

                fsCmd.Parameters.AddWithValue("@vid", vIdentifier);
                fsCmd.Parameters.AddWithValue("@sDesc", summary);
                fsCmd.Parameters.AddWithValue("@fDesc", fullDesc);

                fsCmd.Parameters.AddWithValue("@ws", ws);
                fsCmd.Parameters.AddWithValue("@genre", _genre);
                fsCmd.Parameters.AddWithValue("@days", _days);
                fsCmd.Parameters.AddWithValue("@time", _time);
                fsCmd.Parameters.AddWithValue("@cap", _cap);
                fsCmd.Parameters.AddWithValue("@age", _age);
                fsCmd.Parameters.AddWithValue("@dcode", dcode);

                fsCmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect("vregCon2.aspx");
            }
        }
    }
}