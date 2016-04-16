using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nc.dev
{
    [ToolboxData("<{0}:venueEntertainmentList runat=server></{0}:venueEntertainmentList>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class venueEntertainmentList : Control, INamingContainer
    {
        private ArrayList itemsArrayList;
        private Panel outerContainer = new Panel();
        private Panel selectedEvent = new Panel();
        private ImageButton buttonCmd = new ImageButton();
        private Panel innerContainer = new Panel();
        private Table listTbl = new Table();

        private ITemplate _itemTemplate;
        public ITemplate itemTemplate
        {
            get { return _itemTemplate; }
            set { _itemTemplate = value; }
        }
        protected override void CreateChildControls()
        {
            if (itemTemplate != null)
            {
                itemTemplate.InstantiateIn(this);
            }
        }
    }
}