using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

namespace VideoGameStoreWeb
{
    public partial class Checkout : System.Web.UI.Page
    {
        Utilities utl = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Application["LoggedOnUser"];               
                Cart cart = (Cart)Application["Cart"];
                gvCart.DataSource = cart.CartItems;
                gvCart.DataBind();
                lblShipTo.Text = user.FirstName + " " + user.LastName + "<br>" + user.Address + "<br>" + user.City + ", " + user.State + " " + user.Zip;
                lblTotal.Text = cart.Total.ToString("C", CultureInfo.CurrentCulture);
            }            
        }

        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //getting row id
            
            try
            {
                int cartItemId = Convert.ToInt32(e.RowIndex);
                Cart cart = (Cart)Application["Cart"];
                cart.CartItems.RemoveAt(cartItemId);                
                gvCart.DataSource = cart.CartItems;
                gvCart.DataBind();
                ucSessionInfo.UpdateCart();                
                lblTotal.Text = cart.Total.ToString("C", CultureInfo.CurrentCulture);

                ShowMessage("Item removed from cart", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error removing cart item", "error");
            }
            
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            GridViewRow row = (GridViewRow)tb.NamingContainer;
            HiddenField hdnGameId = (HiddenField)row.FindControl("hdnGameId");            

            Cart cart = (Cart)Application["Cart"];
            foreach (CartItem c in cart.CartItems)
            {
                if (c.Game.ID.ToString() == hdnGameId.Value)
                {
                    c.Quantity = Convert.ToInt32(tb.Text);
                    gvCart.DataSource = cart.CartItems;
                    gvCart.DataBind();
                    lblTotal.Text = cart.Total.ToString("C", CultureInfo.CurrentCulture);
                }
            }
        }
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Application["LoggedOnUser"] = "";
            Application["LoggedOnUserId"] = "";
            Application["LoggedOnUserType"] = "";
            Response.Redirect("Login.aspx");
            //Application["Cart"] = null;
        }

        protected void lbtnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                User user = (User)Application["LoggedOnUser"];
                Cart cart = (Cart)Application["Cart"];
                foreach (CartItem order in cart.CartItems)
                {
                    AddOrder(new Order(0, order.Game, "", user, order.Game.DiscountedPrice, order.Quantity, new DateTime()));
                }
                cart.CartItems.Clear();
                ucSessionInfo.UpdateCart();
                ShowMessage("Order completed", "success");
                lbtnAddOrder.Visible = false;
                gvCart.Visible = false;
                divOrderCompleted.Visible = true;
            }
            catch (Exception ex)
            {
                ShowMessage("Error adding orders", "error");
            }
            
        }

        protected bool AddOrder(Order order)
        {
            try
            {
                User user = (User)Application["LoggedOnUser"];
                localhost.WebService1 vgsService = new localhost.WebService1();
                return vgsService.AddOrder(JsonConvert.SerializeObject(order));                 
            }
            catch (Exception ex)
            {
                ShowMessage("Error completing order", "error");                
            }
            return false;

        }
    }
}