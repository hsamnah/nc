using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Venues
{
    public partial class venuePhotos : System.Web.UI.Page
    {
        private string con
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private int venueIdentifier
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity VenueIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = VenueIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        public string venueName
        {
            get
            {
                SqlParameter retParam;
                SqlParameter banParam;
                string strResult;
                SqlConnection conn = new SqlConnection(con);
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_venueName_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vid", venueIdentifier);

                retParam = cmd.Parameters.Add(new SqlParameter("@vN", SqlDbType.VarChar, 255));
                retParam.Direction = ParameterDirection.Output;

                banParam = cmd.Parameters.Add(new SqlParameter("@vB", SqlDbType.VarChar, 255));
                banParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                conn.Close();
                strResult = retParam.Value.ToString();
                return (strResult == "na") ? string.Empty : strResult;
            }
        }

        private string getVenuePath
        {
            get
            {
                string vN = "/Venues/venueUploads/" + venueName;
                return vN;
            }
        }

        private bool isPublic(string Dir)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("isPublic_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vid", venueIdentifier);
            cmd.Parameters.AddWithValue("@path", Dir);

            SqlParameter retParam = new SqlParameter();
            retParam = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            retParam.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            int intResult = Convert.ToInt32(retParam.Value.ToString());
            if (intResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private char[] delimiter = { '/' };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _path = Server.MapPath(getVenuePath);
                DirectoryInfo dir = new DirectoryInfo(_path);

                FileInfo[] Images = dir.GetFiles("*.png").Concat(dir.GetFiles("*.jpg")).Concat(dir.GetFiles("*.gif")).ToArray();
                Files.DataSource = Images;
                Files.DataBind();

                TreeNode rootNode = new TreeNode();
                rootNode.Text = "...";
                rootNode.Value = _path;
                rootNode.ShowCheckBox = false;
                DirectoryListing.Nodes.AddAt(0, rootNode);
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeNode subTN = new TreeNode();
                    subTN.Text = subDir.Name.ToString();
                    subTN.Value = subDir.FullName.ToString();
                    subTN.Checked = (isPublic(subDir.FullName.ToString())) ? true : false;
                    DirectoryListing.Nodes.Add(subTN);
                    getChildNodes(subTN, subDir.FullName.ToString());
                }
            }
        }

        private void getChildNodes(TreeNode parentNode, string dPath)
        {
            DirectoryInfo cDi = new DirectoryInfo(dPath);
            foreach (DirectoryInfo childDir in cDi.GetDirectories())
            {
                TreeNode cTn = new TreeNode();
                cTn.Text = childDir.Name.ToString();
                cTn.Value = childDir.FullName.ToString();
                cTn.Checked = (isPublic(childDir.FullName.ToString())) ? true : false;
                parentNode.ChildNodes.Add(cTn);
                getChildNodes(cTn, childDir.FullName.ToString());
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

                if (makePublic.Checked)
                {
                    SqlConnection conn = new SqlConnection(con);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("add_vDirectory_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@vid", venueIdentifier);
                    cmd.Parameters.AddWithValue("@dir", newDir);
                    cmd.Parameters.AddWithValue("@cmd", "public");

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                DirectoryListing.Nodes.Clear();
                string _path = Server.MapPath(getVenuePath);
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
                    SqlParameter retValue;

                    SqlConnection conn = new SqlConnection(con);
                    conn.Open();
                    SqlCommand delF = new SqlCommand("delete_img_sp", conn);
                    delF.CommandType = CommandType.StoredProcedure;

                    delF.Parameters.AddWithValue("@fileN", file2Delete.FullName);

                    retValue = delF.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Bit));
                    retValue.Direction = ParameterDirection.ReturnValue;

                    delF.ExecuteNonQuery();
                    conn.Close();
                    if (Convert.ToInt32(retValue.Value.ToString()) == 1)
                    {
                        file2Delete.Delete();
                    }
                    else
                    {
                        dirErr.Visible = true;
                        dirErr.Text = "File not available to delete.";
                    }
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
            if ((!string.IsNullOrEmpty(DirectoryListing.SelectedValue)) && (DirectoryListing.SelectedValue != Server.MapPath(getVenuePath)))
            {
                if (Directory.GetFiles(DirectoryListing.SelectedValue).Length > 0)
                {
                    DeleteHelper(DirectoryListing.SelectedValue);
                    Directory.Delete(DirectoryListing.SelectedValue);

                    DirectoryListing.Nodes.Clear();
                    string _path = Server.MapPath(getVenuePath);
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
                    string _path = Server.MapPath(getVenuePath);
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
                    string _path = Server.MapPath(getVenuePath);
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

                    string _path = DirectoryListing.SelectedValue;
                    DirectoryInfo di = new DirectoryInfo(_path);
                    FileInfo[] testImgs = di.GetFiles();

                    if (isFileUnique(FileLoad.FileName, testImgs))
                    {
                        string newFilePath = DirectoryListing.SelectedValue + "/" + FileLoad.FileName;
                        string cDirectory = getDir(newFilePath);

                        SqlConnection conn = new SqlConnection(con);
                        conn.Open();

                        SqlCommand nImg = new SqlCommand("add_Venue_newIMG_sp", conn);
                        nImg.CommandType = CommandType.StoredProcedure;

                        nImg.Parameters.AddWithValue("@vid", venueIdentifier);
                        nImg.Parameters.AddWithValue("@iName", FileLoad.FileName);
                        nImg.Parameters.AddWithValue("@iType", "General Image");
                        nImg.Parameters.AddWithValue("@Directory", cDirectory);

                        nImg.ExecuteNonQuery();
                        conn.Close();

                        FileLoad.SaveAs(newFilePath);
                    }
                    else
                    {
                        Random rand = new Random();
                        int randomizer = rand.Next(1, 1000000);
                        string getExtension = Path.GetExtension(FileLoad.PostedFile.FileName);
                        string getName = Path.GetFileNameWithoutExtension(FileLoad.PostedFile.FileName) + randomizer.ToString() + getExtension;

                        string newFilePath = DirectoryListing.SelectedValue + "/" + getName;
                        string cDirectory = getDir(newFilePath);

                        SqlConnection conn = new SqlConnection(con);
                        conn.Open();

                        SqlCommand nImg = new SqlCommand("add_Venue_newIMG_sp", conn);
                        nImg.CommandType = CommandType.StoredProcedure;

                        nImg.Parameters.AddWithValue("@vid", venueIdentifier);
                        nImg.Parameters.AddWithValue("@iName", getName);
                        nImg.Parameters.AddWithValue("@iType", "General Image");
                        nImg.Parameters.AddWithValue("@Directory", cDirectory);

                        nImg.ExecuteNonQuery();
                        conn.Close();

                        FileLoad.SaveAs(newFilePath);
                    }
                    Files.SelectedIndex = -1;
                    FileInfo[] Images = di.GetFiles("*.png").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.gif")).ToArray();
                    Files.DataSource = Images;
                    Files.DataBind();
                }
                else
                {
                    dirErr.Visible = true;
                    dirErr.Text = "Nothing is available to upload. Please browse for your image and try again.";
                }
            }
            else
            {
                dirErr.Visible = true;
                dirErr.Text = "No directory selected.";
            }
        }

        private bool isFileUnique(string fName, FileInfo[] dirFileList)
        {
            int intIsSame = 0;
            for (int index = 0; index < dirFileList.Count(); index++)
            {
                if (fName == dirFileList[index].ToString())
                {
                    intIsSame++;
                }
            }
            if (intIsSame > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void deleteFile_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(DirectoryListing.SelectedValue))
            {
                dirErr.Visible = false;
                SqlParameter retValue;
                string f2D = Files.DataKeys[Files.SelectedIndex].ToString();

                SqlConnection conn = new SqlConnection(con);
                conn.Open();
                SqlCommand delF = new SqlCommand("delete_img_sp", conn);
                delF.CommandType = CommandType.StoredProcedure;

                delF.Parameters.AddWithValue("@fileN", f2D);

                retValue = delF.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Bit));
                retValue.Direction = ParameterDirection.ReturnValue;

                delF.ExecuteNonQuery();
                conn.Close();

                if (Convert.ToInt32(retValue.Value.ToString()) == 1)
                {
                    FileInfo file2Delete = new FileInfo(f2D);
                    file2Delete.Delete();
                }
                else
                {
                    dirErr.Visible = true;
                    dirErr.Text = "Transaction unsuccessful.  Please try again.";
                }

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
            string uriBase = Server.MapPath(getVenuePath);
            string url = (DataBinder.Eval(path, "FullName").ToString());

            var url2Ref = new Uri(url);
            var reference = new Uri(uriBase);

            string conversion = reference.MakeRelativeUri(url2Ref).ToString();
            string returnStr = "venueUploads/" + conversion;
            return returnStr;
        }

        private string getDir(String fi)
        {
            string containDirectory;
            int index;
            index = fi.LastIndexOf('/');
            containDirectory = fi.Remove(index);
            return containDirectory;
        }

        protected void setVisible_Change(object sender, TreeNodeEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("add_vDirectory_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (e.Node.Checked)
            {
                cmd.Parameters.AddWithValue("@vid", venueIdentifier);
                cmd.Parameters.AddWithValue("@dir", e.Node.Value.ToString());
                cmd.Parameters.AddWithValue("@cmd", "public");
            }
            else
            {
                cmd.Parameters.AddWithValue("@vid", venueIdentifier);
                cmd.Parameters.AddWithValue("@dir", e.Node.Value.ToString());
                cmd.Parameters.AddWithValue("@cmd", "private");
            }
            cmd.ExecuteNonQuery();
            conn.Close();

            DirectoryListing.Nodes.Clear();
            string _path = Server.MapPath(getVenuePath);
            DirectoryInfo dir = new DirectoryInfo(_path);

            FileInfo[] Images = dir.GetFiles("*.png").Concat(dir.GetFiles("*.jpg")).Concat(dir.GetFiles("*.gif")).ToArray();
            Files.DataSource = Images;
            Files.DataBind();

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "...";
            rootNode.Value = _path;
            rootNode.ShowCheckBox = false;
            DirectoryListing.Nodes.AddAt(0, rootNode);
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                TreeNode subTN = new TreeNode();
                subTN.Text = subDir.Name.ToString();
                subTN.Value = subDir.FullName.ToString();
                subTN.Checked = (isPublic(subDir.FullName.ToString())) ? true : false;
                DirectoryListing.Nodes.Add(subTN);
                getChildNodes(subTN, subDir.FullName.ToString());
            }
        }
    }
}