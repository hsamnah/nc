using System;
using System.Web.Security;
using System.Web.UI;

namespace nc.UserItems
{
    public partial class Test : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //  userPosts.uId = UI;
        }
    }
}