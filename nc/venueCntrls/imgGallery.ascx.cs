using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.venueCntrls
{
    public partial class imgGallery : System.Web.UI.UserControl
    {
        public string dirName { get; set; }
        public string side { get; set; }
        public int vID { get; set; }

        private string rootP
        {
            get { return (side == "Venue") ? "/Venues/venueUploads/" + dirName + "/" : "/UserItems/userUploads/" + dirName + "/"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                photoVCollection(Server.MapPath(rootP));
                ViewState["path"] = Server.MapPath(rootP);
            }
            else
            {
                photoVCollection((string)ViewState["path"]);
            }
        }

        private void photoVCollection(string Path)
        {
            GDC.Controls.Clear();
            GC.Controls.Clear();

            int dirIndex = 0;
            DirectoryInfo primaryInfo = new DirectoryInfo(Path);

            DirectoryInfo[] pI = primaryInfo.GetDirectories();

            FileInfo[] pF = primaryInfo.GetFiles("*.png").Concat(primaryInfo.GetFiles("*.jpg")).Concat(primaryInfo.GetFiles("*.gif")).ToArray();

            foreach (DirectoryInfo subDir in pI)
            {
                DirectoryInfo[] dirCount = subDir.GetDirectories();

                Panel pCont = new Panel();
                pCont.CssClass = "pnlCon_cls";
                pCont.ID = "dirBasePanel_" + dirIndex.ToString();
                pCont.ClientIDMode = ClientIDMode.Predictable;
                Panel DCont = new Panel();
                DCont.CssClass = "dntContainer_cls";
                DCont.ID = "dcontainer_" + dirIndex.ToString();
                DCont.ClientIDMode = ClientIDMode.Predictable;

                Literal lit = new Literal();
                lit.Text = "<br />";
                DCont.Controls.Add(lit);

                Literal lit1 = new Literal();
                lit1.Text = "<br />";
                DCont.Controls.Add(lit1);

                Literal lit2 = new Literal();
                lit2.Text = "<br />";
                DCont.Controls.Add(lit2);

                Literal lit3 = new Literal();
                lit3.Text = "<br />";
                DCont.Controls.Add(lit3);

                LinkButton dBtn = new LinkButton();
                dBtn.Text = dirCount.Count().ToString();
                dBtn.Command += new CommandEventHandler(DBtn_Command);
                dBtn.CommandName = "newPath";
                dBtn.CommandArgument = subDir.FullName.ToString();
                dBtn.CssClass = "dirCountbtn_cls";
                DCont.Controls.Add(dBtn);

                pCont.Controls.Add(DCont);
                System.Web.UI.WebControls.Image SubImg = new System.Web.UI.WebControls.Image();

                FileInfo[] subFiles = subDir.GetFiles("*.png").Concat(subDir.GetFiles("*.jpg")).Concat(subDir.GetFiles("*.gif")).ToArray();
                for (int i = 0; i < subFiles.Count(); i++)
                {
                    if (i < 3)
                    {
                        if (i % 3 == 0)
                        {
                            Panel imgCon = new Panel();
                            string direction = urlConversion2(subFiles[i].FullName);
                            imgCon.Style.Add("background-image", "url('" + "/" + direction + "')");
                            imgCon.Style.Add("border", "solid 1px white");
                            imgCon.Style.Add(HtmlTextWriterStyle.Width.ToString(), "100px");
                            imgCon.Style.Add(HtmlTextWriterStyle.Height.ToString(), "100px");
                            pCont.Controls.Add(imgCon);
                        }
                        else
                        {
                            Panel imgCon = new Panel();
                            string direction = urlConversion2(subFiles[i].FullName);
                            imgCon.Style.Add("background-image", "url('" + "/" + direction + "')");
                            imgCon.Style.Add("border", "solid 1px white");
                            imgCon.Style.Add(HtmlTextWriterStyle.Width.ToString(), "49px");
                            imgCon.Style.Add(HtmlTextWriterStyle.Height.ToString(), "25px");
                            imgCon.Style.Add("display", "inline-block");
                            pCont.Controls.Add(imgCon);
                        }
                    }
                }
                GDC.Controls.Add(pCont);
                dirIndex++;
            }
            int Index = 0;
            foreach (FileInfo Img in pF)
            {
                Panel iContainer = new Panel();
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                switch (DemensionLbl(Img))
                {
                    case "Horizontal Rectangle":
                        img.ImageUrl = "../" + urlConversion2(Img.FullName);
                        img.Width = Unit.Pixel(140);
                        img.ID = "img_" + Index.ToString();
                        img.CssClass = "imgbtn_cls";
                        iContainer.Controls.Add(img);
                        iContainer.CssClass = "vIColl_cls";
                        iContainer.ID = Img.Name + "_" + Index.ToString();
                        iContainer.ClientIDMode = ClientIDMode.Predictable;
                        GC.Controls.Add(iContainer);
                        break;

                    case "Vertical Rectangle":
                        img.ImageUrl = "../" + urlConversion2(Img.FullName);
                        img.Width = Unit.Pixel(140);
                        img.ID = "img_" + Index.ToString();
                        img.CssClass = "imgbtn_cls";
                        iContainer.Controls.Add(img);
                        iContainer.CssClass = "vIColl_cls";
                        iContainer.ID = Img.Name + "_" + Index.ToString();
                        iContainer.ClientIDMode = ClientIDMode.Predictable;
                        GC.Controls.Add(iContainer);
                        break;

                    case "Square":
                        img.ImageUrl = "../" + urlConversion2(Img.FullName);
                        img.Width = Unit.Pixel(140);
                        img.ID = "img_" + Index.ToString();
                        img.CssClass = "imgbtn_cls";
                        iContainer.Controls.Add(img);
                        iContainer.CssClass = "vIColl_cls";
                        iContainer.ID = Img.Name + "_" + Index.ToString();
                        iContainer.ClientIDMode = ClientIDMode.Predictable;
                        GC.Controls.Add(iContainer);
                        break;
                }
                Index++;
            }
        }

        private void DBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "newPath")
            {
                ViewState["path"] = null;
                ViewState["path"] = e.CommandArgument.ToString();
                photoVCollection(e.CommandArgument.ToString());
            }
        }

        private string demension;

        private string DemensionLbl(FileInfo fi)
        {
            int x; int y;
            System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(fi.FullName);
            x = Convert.ToInt32(bmap.PhysicalDimension.Width.ToString()); y = Convert.ToInt32(bmap.PhysicalDimension.Height.ToString());
            bmap.Dispose();

            if (x > y)
            {
                demension = "Horizontal Rectangle";
            }
            if (x < y)
            {
                demension = "Vertical Rectangle";
            }
            if (x == y)
            {
                demension = "Square";
            }
            return demension;
        }

        protected string urlConversion(object path)
        {
            string uriBase = Server.MapPath(rootP);
            string url = (DataBinder.Eval(path, "FullName").ToString());

            var url2Ref = new Uri(url);
            var reference = new Uri(uriBase);

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = ((side == "Venue") ? "venueUploads/" + dirName + "/" : "userUploads/" + dirName + "/") + conversion;
            return returnStr;
        }

        protected string urlConversion2(string path)
        {
            var url2Ref = new Uri(path);
            var reference = new Uri(Server.MapPath(rootP));

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = ((side == "Venue") ? "Venues/venueUploads/" + dirName + "/" : "UserItems/userUploads/" + dirName + "/") + conversion;
            return returnStr;
        }

        protected void rootDir_Click(object sender, EventArgs e)
        {
            photoVCollection(Server.MapPath(rootP));
            ViewState["path"] = Server.MapPath(rootP);
        }

        protected void uploadImgs_Click(object sender, ImageClickEventArgs e)
        {
            windowResponseHelper.Redirect(Response, ((side == "Venue") ? "/Venues/venuePhotos.aspx" : "/UserItems/userPhotos.aspx"), "_blank", "menubar = 0, scrollbars = 1, titlebar=0,fullscreen=0, width = 1000, height = 710, top = 10");
        }
    }
}