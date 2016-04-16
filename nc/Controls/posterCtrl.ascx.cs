using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class posterCtrl : System.Web.UI.UserControl
    {
        public string con { get; set; }
        public string userName { get; set; }
        public string venueName { get; set; }
        public int userID { get; set; }
        public string siteSide { get; set; }

        private string userRootPath
        {
            get { return (siteSide == "User") ? "/UserItems/userUploads/" + userName + userID.ToString() + "/" : "/Venues/venueUploads/" + userName + "/"; }
        }

        private string path { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("PC"))
            {
                csm.RegisterClientScriptInclude("PC", "../scripts/jsPC.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            imgMenu1.uID = userID;
            imgMenu1.uName = userName;
            imgMenu1.vName = venueName;
            imgMenu1.utype = siteSide;
            mDirections.uid = userID;
            mDirections.destination = "PostBox";
        }

        private DataTable gpDT = new DataTable();

        public void sendPost_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            if (siteSide == "User")
            {
                SqlCommand postCmd = new SqlCommand("add_Post_sp", conn);
                postCmd.CommandType = CommandType.StoredProcedure;

                postCmd.Parameters.AddWithValue("@uid", userID);
                postCmd.Parameters.AddWithValue("@origin", siteSide);
                postCmd.Parameters.AddWithValue("@post", postValue.Value);

                postCmd.ExecuteNonQuery();
                ((nc.UserItems.userwelcome)this.Page).postReset();
                conn.Close();
            }
            else
            {
                SqlCommand postCmd = new SqlCommand("add_Post_sp", conn);
                postCmd.CommandType = CommandType.StoredProcedure;

                postCmd.Parameters.AddWithValue("@uid", userID);
                postCmd.Parameters.AddWithValue("@origin", siteSide);
                postCmd.Parameters.AddWithValue("@post", postValue.Value);

                postCmd.ExecuteNonQuery();
                ((nc.Venues.welcome)this.Page).postReset();
                conn.Close();
            }
        }
    }
}