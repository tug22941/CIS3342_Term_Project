using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;


namespace VideoGameStoreWeb
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            BindOrders();
            
        }

        protected void BindOrders()
        {
            try
            {
                User user = (User)Application["LoggedOnUser"];
                localhost.WebService1 vgsService = new localhost.WebService1();
                string strOrders = vgsService.GetOrders(user.Id);
                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(strOrders);
                rptrOrders.DataSource = orders;
                rptrOrders.DataBind();     
            }
            catch (Exception ex)
            {
                ShowMessage("Error fetching orders", "error");
            }

        }
        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }
    }
}