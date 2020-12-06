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

namespace RestaurantReviewSystem
{
    public partial class Register : System.Web.UI.Page
    {
        Utilities utl = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:44368/v1";
            User user = new User();
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Phone = txtPhone.Text;
            user.Email = txtEmail.Text;
            user.Username = txtUsername.Text;
            //password is sent and stored encrypted
            user.Password = utl.Encrypt(txtPassword.Text);
            user.Address = txtAddress.Text;
            user.City = txtCity.Text;
            user.State = ddlState.SelectedValue;
            user.Zip = txtZip.Text;
            user.UserType = rblUserType.SelectedItem.Text;
            user.FavoriteGame = txtFavoriteGame.Text;
            user.MotherMaidenName = txtMotherMaidenName.Text;
            user.FavoriteSport = txtFavoriteSport.Text;
            var serializedParam = JsonConvert.SerializeObject(user);
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl + "/Register", content).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            if (Convert.ToBoolean(data))
            {
                //registered and pending verification
                ShowMessage("You have been registered. Before you can log in, you need to verify your account. Please check your email for instructions.", "success");               
            }
            else
            {
                //user may exist
                ShowMessage("The provided username or email is already taken. If you have an account, please log in instead.", "error");
            }
        }

        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }
    }
}