using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace nc
{
    public partial class vregCon3 : System.Web.UI.Page
    {
        private string _Dir;

        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("NV"))
            {
                csm.RegisterClientScriptInclude("NV", "/scripts/jsNV.js");
            }
        }

        private string getDir(int vID)
        {
            // this method returns the venue created directory.  It is currently incomplete.
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand dircmd = new SqlCommand("get_venueDirectory_sp", conn);
            dircmd.CommandType = CommandType.StoredProcedure;

            dircmd.Parameters.AddWithValue("@vID", vID);
            _Dir = (string)dircmd.ExecuteScalar();

            return _Dir;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UploadBkgrndLink_Click(object sender, EventArgs e)
        {
            if (bgUpload.HasFile)
            {
                clrSelector.Text = "Select Color";
                if (!string.IsNullOrEmpty(Session["vid"].ToString()))
                {
                    string fp = getDir((int)Session["vid"]) + bgUpload.FileName;
                    bgUpload.SaveAs(Server.MapPath(fp));
                    var bgPath = getDir((int)Session["vid"]) + imgName((int)Session["vid"], fp, bgUpload.FileName, "background");

                    UpbgStatus.Text = "Transfer status: Transfer successful to: " + fp;
                    bgContainer.Style.Add(HtmlTextWriterStyle.BackgroundImage, bgPath);
                }
                else
                {
                    UpbgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
            else
            {
                if (clrSelector.Text != "Select Color")
                {
                    if (!string.IsNullOrEmpty(Session["vid"].ToString()))
                    {
                        bgContainer.Style.Add(HtmlTextWriterStyle.BackgroundColor, clrSelector.Text);
                        // this information has to be saved somewhere.
                    }
                    else
                    {
                        UpbgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                    }
                }
                else
                {
                    UpbgStatus.Text = "Transfer status: Nothing selected. Please search for an image to upload or select a color.";
                }
            }
            clrSelector.Text = "Select Color";
        }

        protected void UploadLogoLink_Click(object sender, EventArgs e)
        {
            if (logoUpload.HasFile)
            {
                if (!string.IsNullOrEmpty(Session["vid"].ToString()))
                {
                    string fp = getDir((int)Session["vid"]) + logoUpload.FileName;
                    logoUpload.SaveAs(Server.MapPath(fp));
                    uplgStatus.Text = "Transfer status: Transfer successful to: " + fp;
                    var logoPath = getDir((int)Session["vid"]) + imgName((int)Session["vid"], fp, logoUpload.FileName, "logo");
                    logoContainer.InnerHtml = "<img id=\"lgimg\" width=\"645px\" src=\"" + logoPath + "\" \\>";
                }
                else
                {
                    uplgStatus.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
        }

        protected void UploadBannerLink_Click(object sender, EventArgs e)
        {
            if (bannerUpload.HasFile)
            {
                if (!string.IsNullOrEmpty(Session["vid"].ToString()))
                {
                    string fp = getDir((int)Session["vid"]) + bannerUpload.FileName;
                    bannerUpload.SaveAs(Server.MapPath(fp));
                    bnrUpload.Text = "Transfer status: Transfer successful to: " + fp;
                    var bannerPath = getDir((int)Session["vid"]) + imgName(Convert.ToInt32((int)Session["vid"]), fp, bannerUpload.FileName, "banner");
                    bnrContainer.InnerHtml = "<img id=\"lgimg\" width=\"645px\" src=\"" + bannerPath + "\" \\>";
                }
                else
                {
                    bnrUpload.Text = "Transfer status: There is no account attached to this upload.  Please register your venue before uploading any images.";
                }
            }
        }

        private SqlParameter iParam;

        private string imgName(int vID, string imgPath, string imgTitle, string imgType)
        {
            int dirIndex = imgPath.LastIndexOf("/");
            string imgDirectory = imgPath.Remove(dirIndex);

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand imgCmd = new SqlCommand("attach_vIMG_sp", conn);
            imgCmd.CommandType = CommandType.StoredProcedure;

            imgCmd.Parameters.AddWithValue("@vid", vID);
            imgCmd.Parameters.AddWithValue("@title", imgTitle);
            imgCmd.Parameters.AddWithValue("@imgDir", imgDirectory);
            imgCmd.Parameters.AddWithValue("@type", imgType);
            iParam = imgCmd.Parameters.Add(new SqlParameter("@imgName", SqlDbType.VarChar, 800));
            iParam.Direction = ParameterDirection.Output;

            imgCmd.ExecuteNonQuery();
            conn.Close();

            return iParam.Value.ToString();
        }

        protected void Venue_register_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("default.aspx", true);
        }
    }
}