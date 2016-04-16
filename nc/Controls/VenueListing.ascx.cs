using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class VenueListing : System.Web.UI.UserControl
    {
        public string location { get; set; }
        public int locationIdentifier { get; set; }
        public int userIdentity { get; set; }

        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private DataTable _v = new DataTable();

        private DataTable sVenue(int locationID)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();

            SqlCommand shortcmd = new SqlCommand("venue_basic_info_sp", con);
            shortcmd.CommandType = CommandType.StoredProcedure;

            shortcmd.Parameters.AddWithValue("@locID", locationID);

            SqlDataAdapter shortDA = new SqlDataAdapter(shortcmd);
            shortDA.Fill(_v);
            return _v;
        }

        private DataTable _vD = new DataTable();

        private DataTable vData(int venueID)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand vDataCmd = new SqlCommand("get_venue_details_sp", con);
            vDataCmd.CommandType = CommandType.StoredProcedure;
            vDataCmd.Parameters.AddWithValue("@vid", venueID);
            SqlDataAdapter vda = new SqlDataAdapter(vDataCmd);
            con.Close();
            vda.Fill(_vD);
            return _vD;
        }

        private DataTable _vDir = new DataTable();

        private DataTable vDirectories(int vid)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_VenueImgDir_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vid", vid);
            SqlDataAdapter dirDA = new SqlDataAdapter(cmd);
            dirDA.Fill(_vDir);
            return _vDir;
        }

        // for dir status and public venue directory use table v_DirStatus_tbl and use add_vDirectory_sp stored procedure to add a directory to a venue.
        private int columnCount = 0;

        private int venueIdentifier = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("jsVL"))
            {
                csm.RegisterClientScriptInclude("jsVL", "../scripts/jsVL.js");
            }
            if (!csm.IsClientScriptIncludeRegistered("jsMU"))
            {
                csm.RegisterClientScriptInclude("jsMU", "../scripts/jsMashUp.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CurrentLocation.Text = currLocal();
                country.DataSource = getCountry();
                country.DataBind();

                country.Items.Insert(0, new ListItem("--Country--"));
                region.Items.Insert(0, new ListItem("--Region--"));
                city.Items.Insert(0, new ListItem("--City--"));

                StringBuilder sb = new StringBuilder();
                StringBuilder sbData = new StringBuilder();
                foreach (DataRow dr in sVenue(locationIdentifier).Rows)
                {
                    venueIdentifier = Convert.ToInt32(dr["Identifier"].ToString());
                    foreach (DataRow vDr in vData(Convert.ToInt32(dr["Identifier"].ToString())).Rows)
                    {
                        if (venueIdentifier == Convert.ToInt32(vDr["VenueIdentity"].ToString()))
                        {
                            sbData.Append("VI<>" + vDr["VenueIdentity"].ToString() + "~");
                            // Address information.
                            sbData.Append("streetAdd1<>" + vDr["vAdd1"].ToString() + "~");
                            sbData.Append("streetAdd2<>" + (String.IsNullOrEmpty(vDr["vAdd2"].ToString()) ? string.Empty : vDr["vAdd2"].ToString()) + "~");
                            sbData.Append("City<>" + vDr["city"].ToString() + "~");
                            sbData.Append("SubRegion<>" + vDr["subReg"].ToString() + "~");
                            sbData.Append("Country<>" + vDr["country"].ToString() + "~");
                            sbData.Append("Postal<>" + vDr["postal"].ToString() + "~");
                            //
                            sbData.Append("Venue_Name<>" + vDr["VenueName"].ToString() + "~");
                            sbData.Append("Venue_Banner<>" + "/" + vDr["imgDirectory"].ToString() + "/" + vDr["imgName"].ToString() + "~");
                            sbData.Append("Full_Description<>" + vDr["FullDescription"].ToString() + "~");
                            sbData.Append("Web_Site<>" + vDr["WebSite"].ToString() + "~");
                            sbData.Append("Contact_Number<>" + vDr["pNumber"].ToString() + "~");
                            sbData.Append("Genre<>" + vDr["Genre"].ToString() + "~");
                            sbData.Append("Hours_of_Operation<>" + vDr["timeOpen"].ToString() + "pm" + "~");
                            sbData.Append("Days_Open<>" + vDr["OpenDays"].ToString() + "~");
                            sbData.Append("Dress_Code<>" + vDr["dCode"].ToString() + "~");
                            sbData.Append("Capacity<>" + vDr["Capacity"].ToString() + "~");
                            sbData.Append("Age_Limit<>" + vDr["ageLimit"].ToString() + "~");
                            sbData.Append("BackgroundImage<>" + "/" + vDr["bImgDirectory"].ToString() + "/" + vDr["bImgName"].ToString() + "~");
                            sbData.Append("BackgroundColor<>" + vDr["backColor"].ToString());
                        }
                    }
                    sb.Append("<input type=\"hidden\" class=\"vdata_cls\" id=\"vData_" + dr["Identifier"].ToString() + "\" value=\"" + sbData.ToString() + "\"/>");
                    sbData.Clear();
                    StringBuilder pg = new StringBuilder();
                    int intCount = 0;
                    pg.Append("\"Directories\":[");

                    foreach(DataRow drow in vDirectories(Convert.ToInt32(dr["Identifier"].ToString())).Rows)
                    {
                        string dPath = drow["Directory"].ToString();
                        DirectoryInfo di = new DirectoryInfo(dPath);
                        if (intCount != 0)
                        {
                            pg.Append(",{\"DirectoryName\":\"" + di.Name.ToString() + "\",\"Imgs\":[");
                        }
                        else
                        {
                            pg.Append("{\"DirectoryName\":\"" + di.Name.ToString() + "\",\"Imgs\":[");
                        }

                        FileInfo[] files = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                        int imgCount = 0;

                        foreach (FileInfo fi in files)
                        {
                            string fileP = fi.FullName.ToString();
                            int fIndex = fileP.LastIndexOf("\\Venues\\venueUploads\\");
                            string fvPath = fileP.Remove(0, fIndex);
                            string final = fvPath.Replace("\\", "/");
                            if (imgCount != 0)
                            {
                                pg.Append(",{\"fileName\":\"" + fi.Name.ToString() + "\",\"filePath\":\"" + final + "\"}");
                            }
                            else
                            {
                                pg.Append("{\"fileName\":\"" + fi.Name.ToString() + "\",\"filePath\":\"" + final + "\"}");
                            }
                            imgCount++;
                        }
                        pg.Append("]}");
                        intCount++;
                    }
                    pg.Append("]");
                    sb.Append("<input type=\"hidden\" class=\"vImgs_cls\" id=\"vImgs_" + dr["Identifier"].ToString() + "\" value=\"" + WebUtility.HtmlEncode(pg.ToString()) + "\"/>");
                    pg.Clear();
                    vDirectories(Convert.ToInt32(dr["Identifier"])).Rows.Clear();

                    sb.Append("<div id=\"primeVContain_" + columnCount + "\" class=\"vc_cls\">");
                    sb.Append("<div id=\"vBannerContain_" + columnCount + "\" class=\"vBannerContainer_cls\">");
                    if (!string.IsNullOrEmpty(dr["imgName"].ToString()))
                    {
                        sb.Append("<image id=\"vBanner_" + columnCount + "\" src=\"/" + dr["imgDir"].ToString() + "/" + dr["imgName"].ToString() + "\" class=\"img_cls\">");
                    }
                    else
                    {
                        sb.Append("<img id=\"vBanner_" + columnCount + "\" src=\"../imgs/vbna.jpg\" class=\"img_cls\">");
                    }
                    sb.Append("</div>");

                    sb.Append("<input class=\"v_cls\" type=\"hidden\" id=\"venue_" + columnCount + "\" value=\"" + dr["Identifier"].ToString() + "\" />");
                    sb.Append("<div id=\"vDetailsContain_" + columnCount + "\" class=\"vDetails_cls\">");
                    sb.Append("<span id=\"vName_" + columnCount + "\" class=\"venueName_cls\">" + dr["Name"].ToString() + "</span><br />");
                    sb.Append("<span id=\"vsd_" + columnCount + "\" class=\"venueShort_cls\">" + dr["short"].ToString() + "</span><br />");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    sb.Append("<div id=\"functions_" + columnCount + "\" class=\"functionContainer_cls\">");
                    sb.Append("<span id=\"details_" + columnCount + "\" class=\"details_cls\">Details</span>&emsp;&nbsp;");
                    sb.Append("<span id=\"follow_" + columnCount + "\" class=\"follow_cls\">Follow</span>&emsp;&emsp;&emsp;&emsp;");
                    sb.Append("<img id=\"thumb_" + columnCount + "\" class=\"thumb_cls\" src=\"../imgs/thumbsup.jpg\" />");
                    sb.Append("</div>");
                    columnCount++;
                }
                venueContainer.InnerHtml = sb.ToString();
                sb.Clear();
            }
        }

        protected void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            region.DataSource = getRegion(country.SelectedValue);
            region.DataBind();
            city.DataSource = getCity(country.SelectedValue, "Country");
            city.DataBind();

            region.Items.Insert(0, new ListItem("--Region--"));
            city.Items.Insert(0, new ListItem("--City--"));
        }

        protected void region_SelectedIndexChanged(object sender, EventArgs e)
        {
            city.DataSource = getCity(region.SelectedValue, "subdivision");
            city.DataBind();

            city.Items.Insert(0, new ListItem("--City--"));
        }

        protected void city_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRegion = region.SelectedValue;
            string strCity = city.SelectedItem.Text;
            string strCountry = country.SelectedItem.Text;
            int intLocalID = Convert.ToInt32(city.SelectedValue);

            CurrentLocation.Text = strCountry + ", " + strRegion + ", " + strCity;

            StringBuilder sb = new StringBuilder();
            StringBuilder sbData = new StringBuilder();
            foreach (DataRow dr in sVenue(locationIdentifier).Rows)
            {
                venueIdentifier = Convert.ToInt32(dr["Identifier"].ToString());
                foreach (DataRow vDr in vData(Convert.ToInt32(dr["Identifier"].ToString())).Rows)
                {
                    if (venueIdentifier == Convert.ToInt32(vDr["VenueIdentity"].ToString()))
                    {
                        sbData.Append("VI<>" + vDr["VenueIdentity"].ToString() + "~");
                        sbData.Append("Venue_Name<>" + vDr["VenueName"].ToString() + "~");
                        sbData.Append("Venue_Banner<>" + "/" + vDr["imgDirectory"].ToString() + "/" + vDr["imgName"].ToString() + "~");
                        sbData.Append("Full_Description<>" + vDr["FullDescription"].ToString() + "~");
                        sbData.Append("Web_Site<>" + vDr["WebSite"].ToString() + "~");
                        sbData.Append("Contact Number<>" + vDr["pNumber"].ToString() + "~");
                        sbData.Append("Genre<>" + vDr["Genre"].ToString() + "~");
                        sbData.Append("Hours_of_Operation<>" + vDr["timeOpen"].ToString() + "-" + vDr["timeClose"].ToString() + "~");
                        sbData.Append("Days_Open<>" + vDr["OpenDays"].ToString() + "~");
                        sbData.Append("Dress_Code<>" + vDr["dCode"].ToString() + "~");
                        sbData.Append("Capacity<>" + vDr["Capacity"].ToString() + "~");
                        sbData.Append("Age_Limit<>" + vDr["ageLimit"].ToString() + "~");
                        sbData.Append("BackgroundImage<>" + "/" + vDr["bImgDirectory"].ToString() + "/" + vDr["bImgName"].ToString() + "~");
                        sbData.Append("BackgroundColor<>" + vDr["backColor"].ToString());
                    }
                }
                sb.Append("<input type=\"hidden\" class=\"vdata_cls\" id=\"vData_" + columnCount + "\" value=\"" + sbData.ToString() + "\"/>");
                sbData.Clear();

                sb.Append("<div id=\"primeVContain_" + columnCount + "\" class=\"vc_cls\">");
                sb.Append("<div id=\"vBannerContain_" + columnCount + "\" class=\"vBannerContainer_cls\">");
                if (!string.IsNullOrEmpty(dr["imgName"].ToString()))
                {
                    sb.Append("<image id=\"vBanner_" + columnCount + "\" src=\"/" + dr["imgDir"].ToString() + "/" + dr["imgName"].ToString() + "\" class=\"img_cls\">");
                }
                else
                {
                    sb.Append("<img id=\"vBanner_" + columnCount + "\" src=\"../imgs/vbna.jpg\" class=\"img_cls\">");
                }
                sb.Append("</div>");
                sb.Append("<input class=\"v_cls\" type=\"hidden\" id=\"venue_" + columnCount + "\" value=\"" + dr["Identifier"].ToString() + "\" />");
                sb.Append("<div id=\"vDetailsContain_" + columnCount + "\" class=\"vDetails_cls\">");
                sb.Append("<span id=\"vName_" + columnCount + "\" class=\"venueName_cls\">" + dr["Name"].ToString() + "</span><br />");
                sb.Append("<span id=\"vsd_" + columnCount + "\" class=\"venueShort_cls\">" + dr["short"].ToString() + "</span><br />");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div id=\"functions_" + columnCount + "\" class=\"functionContainer_cls\">");
                sb.Append("<span id=\"details_" + columnCount + "\" class=\"details_cls\">Details</span>&emsp;&nbsp;");
                sb.Append("<span id=\"follow_" + columnCount + "\" class=\"follow_cls\">Follow</span>&emsp;&emsp;&emsp;&emsp;");
                sb.Append("<img id=\"thumb_" + columnCount + "\" class=\"thumb_cls\" src=\"../imgs/thumbsup.jpg\" />");
                sb.Append("</div>");
                columnCount++;
            }
            venueContainer.InnerHtml = sb.ToString();
            sb.Clear();
        }

        private string _cl;

        private string currLocal()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_User_Location_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", userIdentity);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                _cl = dr["city"].ToString() + ", " + dr["Sub"].ToString();
            }
            dr.Close();
            return _cl;
        }

        private DataTable _gC = new DataTable();

        private DataTable getCountry()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand country = new SqlCommand("get_country_sp", con);
            country.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(country);
            da.Fill(_gC);
            con.Close();
            return _gC;
        }

        private DataTable _gR = new DataTable();

        private DataTable getRegion(string code)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand sub = new SqlCommand("get_subdivision_sp", con);
            sub.CommandType = CommandType.StoredProcedure;
            sub.Parameters.AddWithValue("@cc", code);
            SqlDataAdapter subda = new SqlDataAdapter(sub);
            subda.Fill(_gR);
            con.Close();
            return _gR;
        }

        private DataTable _gCity = new DataTable();

        private DataTable getCity(string code, string range)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand city = new SqlCommand("get_city_sp", con);
            city.CommandType = CommandType.StoredProcedure;

            city.Parameters.AddWithValue("@Item", code);
            city.Parameters.AddWithValue("@From", range);

            SqlDataAdapter cityda = new SqlDataAdapter(city);
            cityda.Fill(_gCity);
            con.Close();
            return _gCity;
        }
    }
}