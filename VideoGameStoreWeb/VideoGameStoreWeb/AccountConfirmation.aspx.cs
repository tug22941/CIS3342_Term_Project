using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

namespace VideoGameStoreWeb
{
    public partial class AccountConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri verificationUrl = HttpContext.Current.Request.Url;
            string apiUrl = "https://localhost:44368/v1";
            User user = new User();
            user.VerificationUrl = verificationUrl.ToString();

            var serializedParam = JsonConvert.SerializeObject(user);
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(apiUrl + "/VerifyUser", content).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            if (Convert.ToBoolean(data))
            {
                //show verification message
                divUserVerified.Visible = true;
            }
            else
            {
                //redirect to login page if this page is access directly
                Session["LoggedOnUser"] = "";
                Response.Redirect("Login.aspx");
            }
        }
       
    }
}