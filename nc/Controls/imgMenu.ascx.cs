using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace nc.Controls
{
    public partial class imgMenu : System.Web.UI.UserControl
    {
        public int uID { get; set; }
        public string uName { get; set; }
        public string vName { get; set; }
        public string utype { get; set; }
        private string getPIndexer
        {
            get
            {
                string p = (utype == "User") ? "/UserItems/userUploads/" : "/Venues/venueUploads/";
                return p;
            }
        }
        private string rootPath
        {
            get {
                string virtPath = (utype == "User") ? "/UserItems/userUploads/" + uName + uID + "/" : "/Venues/venueUploads/" + vName + "/";
                return Server.MapPath(virtPath);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptIncludeRegistered("IM"))
            {
                csm.RegisterClientScriptInclude("IM", "../scripts/jsIM.js");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo pDirectory = new DirectoryInfo(rootPath);
            FileInfo[] pFiles = pDirectory.GetFiles("*.png").Concat(pDirectory.GetFiles("*.jpg")).Concat(pDirectory.GetFiles("*.gif")).ToArray();
            StringBuilder dirFil_sb = new StringBuilder();
            int dirCount = 0, imgCount = 0;

            string correctRoot = rootPath.Replace("\\", "/");
            int rPathIndex = correctRoot.LastIndexOf(getPIndexer);
            string rPath = correctRoot.Remove(0, rPathIndex) + "/";
            dirFil_sb.Append("\"root\":\"" + rPath + "\",");
            dirFil_sb.Append("\"directories\":[");
            foreach(DirectoryInfo dir in pDirectory.GetDirectories())
            {
                string correctedPath = dir.FullName.Replace("\\", "/");
                int pathIndex = correctedPath.LastIndexOf(getPIndexer);
                string vPath = correctedPath.Remove(0, pathIndex) + "/";
                if (dirCount == 0)
                {
                    dirFil_sb.Append("{\"DirectoryName\":\"" + dir.Name + "\",\"DirectoryPath\":\"" + vPath + "\"" + id(dir.FullName) + "}");
                }
                else
                {
                    dirFil_sb.Append(",{\"DirectoryName\":\"" + dir.Name + "\",\"DirectoryPath\":\"" + vPath + "\"" + id(dir.FullName) + "}");
                }
                dirCount++;
            }
            dirFil_sb.Append("]");
            if (pFiles.Count() > 0)
            {
                dirFil_sb.Append(",\"Files\":[");
                foreach (FileInfo file in pFiles)
                {
                    string correctedPath = file.FullName.Replace("\\", "/");
                    int pathIndex = correctedPath.LastIndexOf(getPIndexer);
                    string vPath = correctedPath.Remove(0, pathIndex);
                    if (imgCount == 0)
                    {
                        dirFil_sb.Append("{\"FileName\":\"" + file.Name + "\",\"FilePath\":\"" + vPath + "\"}");
                    }
                    else
                    {
                        dirFil_sb.Append(",{\"FileName\":\"" + file.Name + "\",\"FilePath\":\"" + vPath + "\"}");
                    }
                    imgCount++;
                }
                dirFil_sb.Append("]");
            }
            imgCollection.Value = dirFil_sb.ToString();
        }
        private string id(string path)
        {
            StringBuilder jPaths = new StringBuilder();
            int dc = 0, ic = 0;

            DirectoryInfo dirs = new DirectoryInfo(path);
            if (dirs.GetDirectories().Count() > 0)
            {
                jPaths.Append(",\"directories\":[");
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    string correctedPath = dir.FullName.Replace("\\", "/");
                    int pathIndex = correctedPath.LastIndexOf(getPIndexer);
                    string vPath = correctedPath.Remove(0, pathIndex) + "/";
                    if (dc == 0)
                    {
                        jPaths.Append("{\"DirectoryName\":\"" + dir.Name + "\",\"DirectoryPath\":\"" + vPath + "\"" + id(dir.FullName) + "}");
                    }
                    else
                    {
                        jPaths.Append(",{\"DirectoryName\":\"" + dir.Name + "\",\"DirectoryPath\":\"" + vPath + "\"" + id(dir.FullName) + "}");
                    }
                    dc++;
                }
                jPaths.Append("]");
            }
            FileInfo[] fils=dirs.GetFiles("*.png").Concat(dirs.GetFiles("*.jpg")).Concat(dirs.GetFiles("*.gif")).ToArray();
            if (fils.Count() > 0)
            {
                jPaths.Append(",\"Files\":[");
                foreach (FileInfo file in fils)
                {
                    string correctedPath = file.FullName.Replace("\\", "/");
                    int pathIndex = correctedPath.LastIndexOf(getPIndexer);
                    string vPath = correctedPath.Remove(0, pathIndex);
                    if (ic == 0)
                    {
                        jPaths.Append("{\"FileName\":\"" + file.Name + "\",\"FilePath\":\"" + vPath + "\"}");
                    }
                    else
                    {
                        jPaths.Append(",{\"FileName\":\"" + file.Name + "\",\"FilePath\":\"" + vPath + "\"}");
                    }
                    ic++;
                }
                jPaths.Append("]");
            }
           
            return jPaths.ToString();
        }
    }
}