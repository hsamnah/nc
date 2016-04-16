using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.root
{
    public partial class validation : System.Web.UI.Page
    {
        protected string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }
        // this represents the temp id and must be replaced by the primary userid for the purpose of details_tbl
        protected string UID
        {
            get { return Request.QueryString["dent"]; }
        }

        protected bool isConfirmed
        {
            get { return (Request.QueryString["isConfirmed"] != "1") ? false : true; }
        }

        protected string gcode
        {
            get { return Request.QueryString["gc"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (isConfirmed)
                {
                    SqlConnection con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmdU = new SqlCommand("validated_user_sp", con);
                    cmdU.CommandType = CommandType.StoredProcedure;
                    cmdU.Parameters.AddWithValue("@uid", Convert.ToInt32(UID));
                    SqlDataReader drU = cmdU.ExecuteReader(CommandBehavior.CloseConnection);
                    while (drU.Read())
                    {
                        accountSetup(Convert.ToInt32(drU["ID"].ToString()), drU["uName"].ToString());
                    }
                    drU.Close();
                }
                else
                {
                    setConfirmation(Convert.ToInt32(UID), Convert.ToInt32(gcode));
                }
                get_CountryList();
            }
        }

        private void get_CountryList()
        {
            country.Items.Insert(0, new ListItem("---Country---"));
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand countryCmd = new SqlCommand("get_Country_sp", con);
            countryCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader countryDR = countryCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (countryDR.Read())
            {
                ListItem li = new ListItem(countryDR["CountryName"].ToString() + " " + countryDR["CountryCode"].ToString(), countryDR["CountryCode"].ToString());
                li.Attributes.Add("class", "subMenu_cls");
                country.Items.Add(li);
            }
            countryDR.Close();
        }

        protected void getSub_Click(object sender, EventArgs e)
        {
            int intResult = new int();
            subdivision.Items.Clear();
            string cc = country.SelectedValue;

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand subCmd = new SqlCommand("get_subdivision_sp", con);
            subCmd.CommandType = CommandType.StoredProcedure;
            subCmd.Parameters.AddWithValue("@cc", cc);
            SqlDataReader subDR = subCmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (subDR.Read())
            {
                if (!string.IsNullOrEmpty(subDR["Subdivision"].ToString()))
                {
                    ListItem li = new ListItem(subDR["Subdivision"].ToString(), subDR["Subdivision"].ToString());
                    li.Attributes.Add("class", "subMenu_cls");
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

        protected void getCity_Click(object sender, EventArgs e)
        {
            getCity(subdivision.SelectedValue, "subdivision");
        }

        protected void getCity(string Identifier, string Region)
        {
            City.Items.Clear();
            SqlConnection con = new SqlConnection(conn);
            if (con.State != ConnectionState.Open) { con.Open(); }
            SqlCommand cmd = new SqlCommand("get_city_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Item", Identifier);
            cmd.Parameters.AddWithValue("@From", Region);
            SqlDataReader cityDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (cityDR.Read())
            {
                ListItem li = new ListItem(cityDR["City_Town"].ToString(), cityDR["recordID"].ToString());
                li.Attributes.Add("class", "subMenu_cls");
                City.Items.Add(li);
            }
            cityDR.Close();
            City.Items.Insert(0, new ListItem("---City---"));
            City.Visible = true;
        }

        protected void setConfirmation(int tmpID, int gcode)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand confcmd = new SqlCommand("get_vCode_sp", con);
            confcmd.CommandType = CommandType.StoredProcedure;
            confcmd.Parameters.AddWithValue("@tID", tmpID);
            SqlParameter conParam = new SqlParameter();
            conParam = confcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            conParam.Direction = ParameterDirection.ReturnValue;
            confcmd.ExecuteNonQuery();
            con.Close();
            int conIntResult = new int();
            conIntResult = Convert.ToInt32(conParam.Value.ToString());
            if (conIntResult == gcode)
            {
                con.Open();
                SqlCommand t2pcmd = new SqlCommand("transfer_tmp2primary_sp", con);
                t2pcmd.CommandType = CommandType.StoredProcedure;

                t2pcmd.Parameters.AddWithValue("@tmpID", tmpID);

                // this is the new primary user Identifier for the details_tbl.
                SqlParameter pResult = new SqlParameter();
                pResult = t2pcmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
                pResult.Direction = ParameterDirection.ReturnValue;

                SqlParameter uResult = new SqlParameter();
                uResult = t2pcmd.Parameters.Add(new SqlParameter("@uName", SqlDbType.VarChar, 255));
                uResult.Direction = ParameterDirection.Output;

                t2pcmd.ExecuteNonQuery();
                con.Close();
                string strUN = uResult.Value.ToString();
                if (Convert.ToInt32(pResult.Value.ToString()) > 0)
                {
                    Session["userID"] = pResult.Value.ToString();
                    accountSetup(Convert.ToInt32(pResult.Value.ToString()), strUN);
                }
            }
            else
            {
                Response.RedirectPermanent("Register.aspx");
            }
        }

        private void accountSetup(int uid, string un)
        {
            string uPath = "UserItems/userUploads/" + un + uid.ToString();
            if (!Directory.Exists(Server.MapPath(uPath)))
            {
                Directory.CreateDirectory(Server.MapPath(uPath));
            }

            newUser nu = new newUser
            {
                uID = Convert.ToInt32(uid.ToString()),
                userName = un
            };

            var content = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("xmlData/UserList.json")))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(",{\"uID\":\"" + nu.uID + "\",\"UserName\":\"" + nu.userName.ToString() + "\"}" + "\n");
            sb.Append("]" + "\n");
            content = Regex.Replace(content, "\n]", sb.ToString());

            using (StreamWriter writer = new StreamWriter(Server.MapPath("xmlData/UserList.json")))
            {
                writer.Write(content);
                writer.Close();
            }
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            string strUIdentifier = Session["userID"].ToString();
            string strFN = FN.Text;
            string strLN = LN.Text;
            string strStr1 = strAdd.Text;
            string strStr2 = aptUnit.Text;
            string strCountry = country.SelectedValue;
            string strSub = subdivision.SelectedValue;
            string strCity = City.SelectedItem.Text;
            string strpz = pz.Text;
            string strhome = tel.Text;
            string strmob = mobile.Text;
            int locID = Convert.ToInt32(City.SelectedValue);

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("insertUserDetails_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@uid", strUIdentifier);
            cmd.Parameters.AddWithValue("@fn", strFN);
            cmd.Parameters.AddWithValue("@ln", strLN);
            cmd.Parameters.AddWithValue("@sa1", strStr1);
            cmd.Parameters.AddWithValue("@sa2", strStr2);
            cmd.Parameters.AddWithValue("@city", strCity);
            cmd.Parameters.AddWithValue("@subdiv", strSub);
            cmd.Parameters.AddWithValue("@country", strCountry);
            cmd.Parameters.AddWithValue("@pz", strpz);
            cmd.Parameters.AddWithValue("@mob", strmob);
            cmd.Parameters.AddWithValue("@home", strhome);
            cmd.Parameters.AddWithValue("@locID", locID);

            SqlParameter comParam = new SqlParameter();
            comParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            comParam.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            con.Close();

            if (Convert.ToInt32(comParam.Value.ToString()) > 0)
            {
                Session.Abandon();
                Response.Redirect("default.aspx?rt=true");
            }
        }
    }

    public class newUser
    {
        public int uID { get; set; }
        public string userName { get; set; }
    }
}