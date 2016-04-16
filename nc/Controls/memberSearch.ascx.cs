using System;
using System.Web.UI;

namespace nc.Controls
{
    public partial class memberSearch : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered("jsMS"))
            {
                Page.ClientScript.RegisterClientScriptInclude("jsMS", "../scripts/jsMS.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void viewMember_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/userItems/userWelcome.aspx?uPage=" + uIdentifier.Value);
        }
    }
}