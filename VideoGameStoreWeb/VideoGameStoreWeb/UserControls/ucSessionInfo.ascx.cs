using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

namespace VideoGameStoreWeb.UserControls
{
    public partial class ucSessionInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    User user = (User)Application["LoggedOnUser"];
                    lblUsername.Text = user.Username;

                    switch (user.UserType)
                    {
                        case "Producer":
                            
                            break;
                        case "Store Manager":

                            break;
                        case "Customer":
                            UpdateCart();
                            break;
                        default:

                            break;
                    }


                }
                catch (Exception ex)
                {
                    //Response.Redirect("Login.aspx");
                }
            }
        }

        public void UpdateCart()
        {
            spanCart.Visible = true;
            Cart cart = (Cart)Application["Cart"];
            lblCart.Text = cart.CartItems.Count.ToString();
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Application["LoggedOnUser"] = "";
            Application["LoggedOnUserId"] = "";
            Application["LoggedOnUserType"] = "";
            Response.Redirect("Login.aspx");
            //Application["Cart"] = null;
        }
    }
}