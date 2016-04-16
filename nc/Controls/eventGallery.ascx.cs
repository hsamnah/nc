using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class eventGallery : System.Web.UI.UserControl
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        public int Identifier { get; set; }

        private string[] location
        {
            get
            {
                SqlParameter city;
                SqlParameter Region;

                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand localCmd = new SqlCommand("get_locality_sp", con);
                localCmd.CommandType = CommandType.StoredProcedure;

                localCmd.Parameters.AddWithValue("@uid", Identifier);

                city = localCmd.Parameters.Add(new SqlParameter("@localCity", SqlDbType.VarChar, 255));
                city.Direction = ParameterDirection.Output;

                Region = localCmd.Parameters.Add(new SqlParameter("@localRegion", SqlDbType.VarChar, 4));
                Region.Direction = ParameterDirection.Output;

                localCmd.ExecuteNonQuery();
                con.Close();

                string[] loco = new string[2];
                loco[0] = city.Value.ToString();
                loco[1] = Region.Value.ToString();

                return loco;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsStartupScriptRegistered("eGallery"))
            {
                csm.RegisterClientScriptInclude("eGallery", "../scripts/eventGalleryScript.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                venue2.DataSource = venueCollection();
                venue2.DataBind();
            }
        }

        private DataTable vc = new DataTable();

        public DataTable venueCollection()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand vCmd = new SqlCommand("get_local_venues_sp", con);
            vCmd.CommandType = CommandType.StoredProcedure;
            vCmd.Parameters.AddWithValue("@City", location[0].ToString());
            vCmd.Parameters.AddWithValue("@Region", location[1].ToString());
            SqlDataAdapter vDA = new SqlDataAdapter(vCmd);
            con.Close();
            vDA.Fill(vc);
            return vc;
        }

        public string eventImg(object directory, object Name)
        {
            string imgDir = (string)(DataBinder.Eval(directory, "Directory"));
            string imgN = (string)(DataBinder.Eval(Name, "Name"));

            int vlIndex = imgDir.LastIndexOf("eventImages");
            string relDir = imgDir.Remove(0, vlIndex);

            return "../Venues/" + relDir + "/" + imgN;
        }

        protected string urlConversion2(string path)
        {
            var url2Ref = new Uri(path);
            var reference = new Uri(Server.MapPath("../venueUploads/"));

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = "Venues/venueUploads/" + "/" + conversion;
            return returnStr;
        }

        private int eCountIndex = 0;
        private string eImg = null;

        protected void Venue2_ItemDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int vID = Convert.ToInt32(venue2.DataKeys[e.Row.DataItemIndex].Value.ToString());
                HtmlGenericControl eColl = ((HtmlGenericControl)e.Row.FindControl("primaryEventContainer"));
                StringBuilder eG = new StringBuilder();
                foreach (DataRow dr in eventCollection(vID).Rows)
                {
                    eImg = "/" + dr["Directory"].ToString() + "/" + dr["Name"].ToString();
                    eG.Append("<img class=\"eventImg_cls\" src=\"" + eImg + "\" id=\"e_" + eCountIndex + "\" />");
                    eG.Append("<input type=\"hidden\" class=\"primary_cls\" id=\"containerID_" + eCountIndex + "\" value=\"" + dr["Identifier"].ToString() + "\"/>");
                    eG.Append("<div class=\"event_Card_cls\" id=\"event_card_" + eCountIndex + "\">");
                    eG.Append("<Table class=\"event_Card_cls\">");
                    eG.Append("<tr>");
                    eG.Append("<td class=\"eventTitleRow_cls\" colspan=\"2\">" + dr["Title"].ToString() + "</td>");
                    eG.Append("</tr>");
                    eG.Append("<tr>");
                    eG.Append("<td class=\"eventRow_cls\" colspan=\"2\">" + dr["eventDesc"].ToString() + "</td>");
                    eG.Append("</tr>");
                    eG.Append("<tr>");
                    eG.Append("<td class=\"eventRowf_cls\">Time:</td>");
                    eG.Append("<td class=\"eventRow_cls\">" + ((DateTime)dr["eDate"]).ToShortDateString() + "<br />" + dr["Time"].ToString() + "</td>");
                    eG.Append("</tr>");
                    eG.Append("</Table>");
                    eG.Append("</div>");
                    eG.Append("<span class=\"gs_cls\" id=\"gs_" + eCountIndex + "\">Guest Services</span>");
                    eCountIndex++;
                }
                eColl.InnerHtml = eG.ToString();
            }
        }

        private DataTable ec = new DataTable();

        public DataTable eventCollection(int vIdentifier)
        {
            ec.Clear();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand eCmd = new SqlCommand("get_eventsFromVenue_sp", con);
            eCmd.CommandType = CommandType.StoredProcedure;

            eCmd.Parameters.AddWithValue("@vid", vIdentifier);

            SqlDataAdapter eDA = new SqlDataAdapter(eCmd);
            con.Close();
            eDA.Fill(ec);
            return ec;
        }

        protected void registerGuests_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Guests":
                    int EventIDentifier = Convert.ToInt32(e.CommandArgument.ToString());
                    windowResponseHelper.Redirect(Response, "/UserItems/guestservices.aspx?eID=" + EventIDentifier.ToString(), "_blank", "menubar = 0, scrollbars = 1, titlebar=0,fullscreen=0, width = 1005, height = 705, top = 10");
                    break;

                default:
                    break;
            }
        }
    }
}