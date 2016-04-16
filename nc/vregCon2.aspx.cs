using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class vRegCon2 : System.Web.UI.Page
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvLineList.DataSource = getConList(Session["vid"].ToString());
            gvLineList.DataMember = "comList";
            gvLineList.DataBind();
            get_CountryList();
        }

        protected void lineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand conCmd = new SqlCommand("insert_VenueContact_sp", conn);
            conCmd.CommandType = CommandType.StoredProcedure;
            conCmd.Parameters.AddWithValue("@vID", Session["vid"].ToString());
            conCmd.Parameters.AddWithValue("@numType", lineType.SelectedValue);
            conCmd.Parameters.AddWithValue("@contactNum", number.Text);
            conCmd.ExecuteNonQuery();
            conn.Close();

            gvLineList.DataSource = getConList(Session["vid"].ToString());
            gvLineList.DataMember = "comList";
            gvLineList.DataBind();
            number.Text = "";
            lineType.SelectedIndex = 0;
        }

        private DataTable getConList(string identifier)
        {
            DataTable conLista = new DataTable();
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand lcmd = new SqlCommand("get_venue_contact_sp", conn);
            lcmd.CommandType = CommandType.StoredProcedure;

            lcmd.Parameters.AddWithValue("@vID", identifier);

            SqlDataAdapter da = new SqlDataAdapter(lcmd);
            conn.Close();
            da.Fill(conLista);
            return conLista;
        }

        protected void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intResult = new int();
            subdivision.Items.Clear();
            string cc = country.SelectedValue;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand subCmd = new SqlCommand("get_subdivision_sp", conn);
            subCmd.CommandType = CommandType.StoredProcedure;
            subCmd.Parameters.AddWithValue("@cc", cc);
            SqlDataReader subDR = subCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (subDR.Read())
            {
                if (!string.IsNullOrEmpty(subDR["Subdivision"].ToString()))
                {
                    ListItem li = new ListItem(subDR["Subdivision"].ToString(), subDR["Subdivision"].ToString());
                    subdivision.Items.Add(li);
                }
                else
                {
                    // get all the cities associated with the country.
                    getCity(cc, "Country");
                }
                intResult += 1;
            }
            // This produces cities where the country is not subdivided into provinces or states.
            if (intResult > 0)
            {
                subdivision.Visible = true;
                subdivision.Items.Insert(0, new ListItem("---Province/State---"));
            }
            else
            {
                subdivision.Visible = false;
                getCity(cc, "Country");
            }
            subDR.Close();
        }

        protected void getCity(string Identifier, string Region)
        {
            city.Items.Clear();
            SqlConnection conn = new SqlConnection(con);
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            SqlCommand cmd = new SqlCommand("get_city_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item", Identifier);
            cmd.Parameters.AddWithValue("@From", Region);
            SqlDataReader cityDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (cityDR.Read())
            {
                ListItem li = new ListItem(cityDR["City_Town"].ToString(), cityDR["recordID"].ToString());
                city.Items.Add(li);
            }
            cityDR.Close();
            city.Items.Insert(0, new ListItem("---City---"));
            city.Visible = true;
        }

        protected void subdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCity(subdivision.SelectedValue, "subdivision");
        }

        private void get_CountryList()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand countryCmd = new SqlCommand("get_Country_sp", conn);
            countryCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader countryDR = countryCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (countryDR.Read())
            {
                ListItem li = new ListItem(countryDR["CountryName"].ToString() + " " + countryDR["CountryCode"].ToString(), countryDR["CountryCode"].ToString());
                country.Items.Add(li);
            }
            countryDR.Close();
            country.Items.Insert(0, new ListItem("---Country---"));
        }

        protected void Venue_register_Click(object sender, EventArgs e)
        {
            string str1 = Addr1.Text;
            string str2 = (string.IsNullOrEmpty(Addr2.Text)) ? string.Empty : Addr2.Text;
            string Country = country.SelectedValue;
            string subDivision = subdivision.SelectedValue;
            string City = city.SelectedItem.Text;
            string locationID = city.SelectedValue;
            string postal = pz.Text;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("add_venue_address_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vid", (int)Session["vid"]);
            cmd.Parameters.AddWithValue("@str1", str1);
            cmd.Parameters.AddWithValue("@str2", str2);
            cmd.Parameters.AddWithValue("@country", Country);
            cmd.Parameters.AddWithValue("@subdiv", subDivision);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@postal", postal);
            cmd.Parameters.AddWithValue("@loID", locationID);

            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("vregCon3.aspx", true);
        }
    }
}