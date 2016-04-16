using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class datePicker : System.Web.UI.UserControl
    {
        public string DateSelection
        {
            get { return eventDate.Text; }
            set { eventDate.Text = value; }
        }

        public string containerID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            cIdentifier.Value = containerID;
        }

        protected void month_change(object sender, MonthChangedEventArgs e)
        {
            calContainer.Visible = true;
        }

        protected void calToggle_click(object sender, ImageClickEventArgs e)
        {
            if (calContainer.Visible == false)
            {
                calContainer.Visible = true;
            }
            else
            {
                calContainer.Visible = false;
            }
            if (Page.AppRelativeVirtualPath == "~/UserItems/UserWelcome.aspx")
            {
                HtmlInputHidden hihState = ((nc.UserItems.userwelcome)this.Page).stateBag;
                hihState.Value = "true";
            }
        }

        protected void dpCal_SelectionChanged(object sender, EventArgs e)
        {
            eventDate.Text = dpCal.SelectedDate.ToShortDateString();
            calContainer.Visible = false;

            if (Page.AppRelativeVirtualPath == "~/UserItems/UserWelcome.aspx")
            {
                HtmlInputHidden hihState = ((nc.UserItems.userwelcome)this.Page).stateBag;
                hihState.Value = "true";
            }
        }

        protected void dpCal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Cell.Text = "";
            }
        }
    }
}