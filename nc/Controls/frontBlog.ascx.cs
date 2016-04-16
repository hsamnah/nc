using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class frontBlog : System.Web.UI.UserControl
    {
        protected string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        private Random rand = new Random();
        private int intResult;

        private bool hasArt(string cat)
        {
            SqlParameter param;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_blog_catCount_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cat", cat);

            param = cmd.Parameters.Add(new SqlParameter("@cc", SqlDbType.Int));
            param.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            con.Close();

            intResult = Convert.ToInt32(param.Value.ToString());

            if (intResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsStartupScriptRegistered("fBlog"))
            {
                csm.RegisterClientScriptInclude("fBlog", "/scripts/jsBlog.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                primRep.DataSource = catDT();
                primRep.DataBind();
            }
        }

        private string iName;

        public string getRandomImg()
        {
            if ((int)rand.Next(0, 16) % 4 == 0)
            {
                iName = "a_" + rand.Next(1, 23) + ".gif";
            }
            else
            {
                iName = "s_" + rand.Next(1, 33) + ".jpg";
            }
            string imgName = iName;
            return imgName;
        }

        private DataTable _dt = new DataTable();

        private DataTable pdt(int cat)
        {
            _dt.Rows.Clear();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand pcmd = new SqlCommand("get_NL_Blogs_sp", con);
            pcmd.CommandType = CommandType.StoredProcedure;

            pcmd.Parameters.AddWithValue("@cat", cat);

            SqlDataAdapter pda = new SqlDataAdapter(pcmd);
            pda.Fill(_dt);
            con.Close();
            return _dt;
        }

        private DataTable _catDT = new DataTable();

        private DataTable catDT()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cCmd = new SqlCommand("get_blogCat_sp", con);
            cCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter cDA = new SqlDataAdapter(cCmd);
            cDA.Fill(_catDT);
            con.Close();
            return _catDT;
        }

        protected void primRep_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int cat = (Convert.ToInt32(primRep.DataKeys[e.Item.ItemIndex]));
            Repeater childRep = ((Repeater)e.Item.FindControl("catItems"));

            childRep.DataSource = pdt(cat);
            childRep.DataBind();
        }
    }
}