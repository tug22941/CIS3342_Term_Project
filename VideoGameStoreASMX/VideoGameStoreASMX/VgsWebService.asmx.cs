using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using VideoGameLibrary;
using Newtonsoft.Json;

namespace VideoGameStoreASMX
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        Utilities utl = new Utilities();

        [WebMethod]
        public bool AddOrder(string order)
        {
            Order newOrder = JsonConvert.DeserializeObject<Order>(order);
            return utl.AddOrder(newOrder);
        }

        [WebMethod]
        public string GetOrders(int userId)
        {
            return utl.GetOrdersJsonStringByCustomerId(userId);
        }
    }
}
