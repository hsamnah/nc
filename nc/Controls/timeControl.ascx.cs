using System;

namespace nc.Controls
{
    public partial class timeControl : System.Web.UI.UserControl
    {
        public string eh
        {
            get { return Hours.SelectedValue; }
        }

        public string em
        {
            get { return Minutes.SelectedValue; }
        }

        public string es
        {
            get { return Seconds.SelectedValue; }
        }

        public string dp
        {
            get { return DayPart.SelectedValue; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}