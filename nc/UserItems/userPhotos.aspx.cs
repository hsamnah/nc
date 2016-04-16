using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.UserItems
{
    public partial class userPhotos : System.Web.UI.Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int UI
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        private string userName
        {
            get
            {
                string un;
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand uCmd = new SqlCommand("get_userName_sp", con);
                uCmd.CommandType = CommandType.StoredProcedure;

                uCmd.Parameters.AddWithValue("@uid", UI);

                un = uCmd.ExecuteScalar().ToString();
                con.Close();
                return un;
            }
        }

        private char[] delimiter = { '/' };

        public string getUserPath
        {
            get
            { return "userUploads/" + userName + UI + "/"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _path = Server.MapPath(getUserPath);
                DirectoryInfo di = new DirectoryInfo(_path);

                FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                Files.DataSource = Images;
                Files.DataBind();
                TreeNode rootNode = new TreeNode();
                rootNode.Text = "...";
                rootNode.Value = _path;
                DirectoryListing.Nodes.AddAt(0, rootNode);
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = dir.Name.ToString();
                    tn.Value = dir.FullName.ToString();
                    DirectoryListing.Nodes.Add(tn);
                    getChildNodes(tn, dir.FullName.ToString());
                }
            }
        }

        private void getChildNodes(TreeNode parentNode, string dPath)
        {
            DirectoryInfo cDi = new DirectoryInfo(dPath);
            foreach (DirectoryInfo dir in cDi.GetDirectories())
            {
                TreeNode cTn = new TreeNode();
                cTn.Text = dir.Name.ToString();
                cTn.Value = dir.FullName.ToString();
                parentNode.ChildNodes.Add(cTn);
                getChildNodes(cTn, dir.FullName.ToString());
            }
        }

        protected void DirecorySelect_Change(object sender, EventArgs e)
        {
            string selectedPath = DirectoryListing.SelectedValue.ToString();
            DirectoryInfo dirListing = new DirectoryInfo(selectedPath);
            FileInfo[] Images = dirListing.GetFiles("*.png").Concat(dirListing.GetFiles("*.jpg")).Concat(dirListing.GetFiles("*.gif")).ToArray();
            Files.DataSource = Images;
            Files.DataBind();
            dirErr.Visible = false;
            dirErr.Text = "";
        }

        protected void AddDirectory_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(DirectoryListing.SelectedValue))
            {
                dirErr.Visible = false;
                string newDir = DirectoryListing.SelectedValue + "\\" + newFolderName.Text;
                Directory.CreateDirectory(newDir);

                DirectoryListing.Nodes.Clear();
                string _path = Server.MapPath(getUserPath);
                DirectoryInfo di = new DirectoryInfo(_path);

                FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                Files.DataSource = Images;
                Files.DataBind();
                TreeNode rootNode = new TreeNode();
                rootNode.Text = "...";
                rootNode.Value = _path;
                DirectoryListing.Nodes.AddAt(0, rootNode);
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = dir.Name.ToString();
                    tn.Value = dir.FullName.ToString();
                    DirectoryListing.Nodes.Add(tn);
                    getChildNodes(tn, dir.FullName.ToString());
                }
                newFolderName.Text = "";
            }
            else
            {
                dirErr.Visible = true;
                dirErr.Text = "Directory not selected.";
                newFolderName.Text = "";
            }
        }

        private void DeleteHelper(string filePath)
        {
            if (Directory.GetFiles(filePath).Length > 0)
            {
                foreach (string str in Directory.GetFiles(filePath))
                {
                    FileInfo file2Delete = new FileInfo(str);
                    file2Delete.Delete();
                }
            }
            else if (Directory.GetDirectories(filePath).Length > 0)
            {
                foreach (string dirString in Directory.GetDirectories(filePath))
                {
                    DeleteHelper(dirString);
                    Directory.Delete(dirString);
                }
            }
        }

        protected void DeleteDirectory_Click(object sender, ImageClickEventArgs e)
        {
            if ((!string.IsNullOrEmpty(DirectoryListing.SelectedValue)) && (DirectoryListing.SelectedValue != Server.MapPath(getUserPath)))
            {
                if (Directory.GetFiles(DirectoryListing.SelectedValue).Length > 0)
                {
                    DeleteHelper(DirectoryListing.SelectedValue);
                    Directory.Delete(DirectoryListing.SelectedValue);

                    DirectoryListing.Nodes.Clear();
                    string _path = Server.MapPath(getUserPath);
                    DirectoryInfo di = new DirectoryInfo(_path);

                    FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                    Files.DataSource = Images;
                    Files.DataBind();
                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = "...";
                    rootNode.Value = _path;
                    DirectoryListing.Nodes.AddAt(0, rootNode);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = dir.Name.ToString();
                        tn.Value = dir.FullName.ToString();
                        DirectoryListing.Nodes.Add(tn);
                        getChildNodes(tn, dir.FullName.ToString());
                    }
                }
                else if (Directory.GetDirectories(DirectoryListing.SelectedValue).Length > 0)
                {
                    DeleteHelper(DirectoryListing.SelectedValue);
                    Directory.Delete(DirectoryListing.SelectedValue);

                    DirectoryListing.Nodes.Clear();
                    string _path = Server.MapPath(getUserPath);
                    DirectoryInfo di = new DirectoryInfo(_path);

                    FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                    Files.DataSource = Images;
                    Files.DataBind();
                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = "...";
                    rootNode.Value = _path;
                    DirectoryListing.Nodes.AddAt(0, rootNode);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = dir.Name.ToString();
                        tn.Value = dir.FullName.ToString();
                        DirectoryListing.Nodes.Add(tn);
                        getChildNodes(tn, dir.FullName.ToString());
                    }
                }
                else
                {
                    Directory.Delete(DirectoryListing.SelectedValue);

                    DirectoryListing.Nodes.Clear();
                    string _path = Server.MapPath(getUserPath);
                    DirectoryInfo di = new DirectoryInfo(_path);

                    FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                    Files.DataSource = Images;
                    Files.DataBind();
                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = "...";
                    rootNode.Value = _path;
                    DirectoryListing.Nodes.AddAt(0, rootNode);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = dir.Name.ToString();
                        tn.Value = dir.FullName.ToString();
                        DirectoryListing.Nodes.Add(tn);
                        getChildNodes(tn, dir.FullName.ToString());
                    }
                }
            }
            else
            {
                dirErr.Visible = true;
                dirErr.Text = "Directory not selected or you're at the root directory.";
            }
        }

        protected void addImg_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(DirectoryListing.SelectedValue))
            {
                if (FileLoad.HasFile)
                {
                    dirErr.Visible = false;
                    string newFilePath = DirectoryListing.SelectedValue + "/" + FileLoad.FileName;
                    FileLoad.SaveAs(newFilePath);

                    Files.SelectedIndex = -1;
                    string _path = DirectoryListing.SelectedValue;
                    DirectoryInfo di = new DirectoryInfo(_path);

                    FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                    Files.DataSource = Images;
                    Files.DataBind();
                }
                else
                {
                    dirErr.Visible = true;
                    dirErr.Text = "Nothing is available to upload.  Please browse for your image and try again.";
                }
            }
            else
            {
                dirErr.Visible = true;
                dirErr.Text = "No directory selected.";
            }
        }

        protected void deleteFile_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(DirectoryListing.SelectedValue))
            {
                dirErr.Visible = false;
                string f2D = Files.DataKeys[Files.SelectedIndex].ToString();
                FileInfo file2Delete = new FileInfo(f2D);
                file2Delete.Delete();

                Files.SelectedIndex = -1;
                string _path = DirectoryListing.SelectedValue;
                DirectoryInfo di = new DirectoryInfo(_path);

                FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                Files.DataSource = Images;
                Files.DataBind();
            }
            else
            {
                dirErr.Visible = true;
                dirErr.Text = "Directory not selected.";
            }
        }

        protected void ImgsThumb_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ib = (sender as ImageButton);
            DataListItem item = ib.NamingContainer as DataListItem;
            int index = item.ItemIndex;

            item.BackColor = System.Drawing.Color.FromArgb(36, 50, 62);
            Files.SelectedIndex = index;
        }

        protected string urlConversion(object path)
        {
            string uriBase = Server.MapPath(getUserPath);
            string url = (DataBinder.Eval(path, "FullName").ToString());

            var url2Ref = new Uri(url);
            var reference = new Uri(uriBase);

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = "userUploads/" + userName + UI + "/" + conversion;
            return returnStr;
        }
    }
}