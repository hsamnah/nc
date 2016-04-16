using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc
{
    public partial class reset : System.Web.UI.Page
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }
        private string recID
        {
            get { return Request.QueryString["interim"]; }
        }
        private string genCode
        {
            get { return Request.QueryString["BlueSky"]; }
        }
        private SqlDataReader getQAH(int rID)
        {
            
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("getUserSecQAH_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@recID", rID);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private int getGenCode(int rID)
        {
            int gc = 0;
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("getGC_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@recID", rID);
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int));
            param.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            gc = Convert.ToInt32(param.Value.ToString());
            return gc;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (getGenCode(Convert.ToInt32(recID))==Convert.ToInt32(genCode))
            {
                SqlDataReader QAH= getQAH(Convert.ToInt32(recID));
                while (QAH.Read())
                {
                    secQ.Text = QAH["secQuest"].ToString();
                    Session["answer"] = QAH["secAnswer"].ToString();
                    secH.Text = QAH["secHint"].ToString();
                }
                QAH.Close();
            }
            else
            {
                Server.Transfer("logrec.aspx", false);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Session["answer"].ToString() == secA.Text)
            {
                Session.Abandon();
                Server.Transfer("finalReset.aspx?a=" + recID, true);
            }
            else
            {
                resultmsg.Visible = true;
                resultmsg.Text = "Your security answer does not match what's on file.  Please try again.";
            }
        }
    }
}