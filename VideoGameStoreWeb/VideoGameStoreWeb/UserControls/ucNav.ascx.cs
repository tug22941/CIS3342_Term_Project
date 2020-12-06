using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

namespace VideoGameStoreWeb.UserControls
{
    public partial class ucNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string thisPage = Request.Url.Segments.Last();
                User user = (User)Application["LoggedOnUser"];

                switch (thisPage)
                {
                    case "Home":
                        lnkHome.CssClass = "active";
                        break;
                    case "PendingApproval":
                        lnkPendingApproval.CssClass = "active";
                        break;
                    case "Orders":
                        lnkOrders.CssClass = "active";
                        break;
                    default:
                        break;
                }

                switch (user.UserType)
                {
                    case "Producer":
                        break;
                    case "Store Manager":
                        liPendingApproval.Visible = true;
                        break;
                    case "Customer":
                        liOrders.Visible = true;
                        break;
                    default:

                        break;
                }
            }
            
        }
    }
}