using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class avatarUpload : System.Web.UI.UserControl
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        public int userIdentifier { get; set; }
        public string avString { get; set; }
        public Image avImg { get; set; }

        private string strFile { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("av"))
            {
                csm.RegisterClientScriptInclude("av", "/scripts/jsAV.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                uID.Value = userIdentifier.ToString();
                if (avString == "/avCollection/avUnavailable.jpg")
                {
                    upAvatar.InnerHtml = "Add Avatar";
                }
                else
                {
                    upAvatar.InnerHtml = "Change Avatar"; ;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            AvatarLoad.Dispose();
        }

        protected void upload_Click(object sender, EventArgs e)
        {
            if (AvatarLoad.HasFile)
            {
                strFile = "avCollection/" + AvatarLoad.FileName;
                if (avString == "/avCollection/avUnavailable.jpg")
                {
                    // leave file on server and add new image on the server and database.
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("userAvatar_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(uID.Value));
                    cmd.Parameters.AddWithValue("@avatar", strFile);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string imgPath = Server.MapPath("/avCollection/" + AvatarLoad.FileName);
                    AvatarLoad.SaveAs(imgPath);
                }
                else
                {
                    // removefile from server and database and replace it with the new image.

                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand deleteCmd = new SqlCommand("delete_avatar_sp", con);
                    deleteCmd.CommandType = CommandType.StoredProcedure;

                    deleteCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(uID.Value));

                    deleteCmd.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("userAvatar_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(uID.Value));
                    cmd.Parameters.AddWithValue("@avatar", strFile);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (!string.IsNullOrEmpty(avString))
                    {
                        File.Delete(Server.MapPath(avString));
                    }
                    string imgPath = Server.MapPath("/avCollection/" + AvatarLoad.FileName);
                    AvatarLoad.SaveAs(imgPath);

                    con.Open();
                    SqlCommand avcmd = new SqlCommand("get_avatar_sp", con);
                    avcmd.CommandType = CommandType.StoredProcedure;

                    avcmd.Parameters.AddWithValue("@uid", Convert.ToInt32(uID.Value));

                    SqlDataReader dr = avcmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        avImg.ImageUrl = "/" + dr["avatar"].ToString();
                    }
                    dr.Close();
                }
            }
        }
    }
}