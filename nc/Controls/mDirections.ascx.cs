using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class mDirections : System.Web.UI.UserControl
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }
        public int uid { get; set; }
        public string destination { get; set; }
        protected int uLocID(int id)
        {
            // get_userLocationID_sp
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_userLocationID_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", id);
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            param.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value.ToString());
        }
        // Venues_sp
        // It is possible to change the location id "locID" based on a drop down list.
        private DataTable venueList(int locID)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Venues_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cityID", locID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void getVenueList(DataTable vDT)
        {
            int vCount = 0;
            DataRowCollection dtc = vDT.Rows;
            StringBuilder vL_sb = new StringBuilder();
            vL_sb.Append("\"v\":[");
            foreach(DataRow dr in dtc)
            {
                string address = dr["vstr1"].ToString() + ", " + dr["city"].ToString() + ", " + dr["region"].ToString() + ", " + dr["postal"].ToString() + ", " + dr["Country"].ToString();
                if (vCount == 0)
                {
                    vL_sb.Append("{\"VI\":\"" + dr["VenueID"].ToString() + "\",\"VN\":\"" + dr["VenueName"].ToString() + "\",\"VA\":\"" + address + "\"}");
                }
                else
                {
                    vL_sb.Append(",{\"VI\":\"" + dr["VenueID"].ToString() + "\",\"VN\":\"" + dr["VenueName"].ToString() + "\",\"VA\":\"" + address + "\"}");
                }
                vCount++;
            }
            vL_sb.Append("]");
            jsVenueList.Value = vL_sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            destinationBox.Value = destination;
            if (!Page.IsPostBack)
            {
                getVenueList(venueList(uLocID(uid)));
            }
        }
    }
}