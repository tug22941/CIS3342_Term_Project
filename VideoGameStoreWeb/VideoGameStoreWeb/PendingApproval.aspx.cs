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
    public partial class PendingApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    User user = (User)Application["LoggedOnUser"];                                     
                    if(user.UserType == "Store Manager")
                    {
                        BindGames();                        
                    }
                    else
                    {
                        //only store managers can view this page, redirect other users to home page
                        Response.Redirect("Home.aspx");
                    }                    
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
            }

        }

        protected void BindGames()
        {
            HttpClient client = new HttpClient();            
            string apiUrl = "https://localhost:44368/v1/GetGamesPendingApproval";
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            if (data != null && data != "")
            {
                gvGames.DataSource = JsonConvert.DeserializeObject<List<Game>>(data);
                gvGames.DataBind();
            }
            else
            {
                gvGames.DataSource = null;
                gvGames.DataBind();
            }

        }

        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }

        protected void gvGames_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGames.EditIndex = e.NewEditIndex;
            BindGames();
        }

        protected void gvGames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                //getting key value, row id
                int RowIndex = int.Parse(e.CommandArgument.ToString());
                int GameId = Convert.ToInt32(gvGames.DataKeys[RowIndex]["ID"]);

                Response.Redirect("GameDetails.aspx?id=" + GameId);
            }
            if (e.CommandName == "Approve")
            {
                //getting key value, row id
                int RowIndex = int.Parse(e.CommandArgument.ToString());
                int gameId = Convert.ToInt32(gvGames.DataKeys[RowIndex]["ID"]);

                Game approvedGame = new Game(gameId);
                approvedGame.ListedForSale = true;
                //do update here

                string apiUrl = "https://localhost:44368/v1";
                var serializedParam = JsonConvert.SerializeObject(approvedGame);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(apiUrl + "/ApproveGame", content).Result;
                var data = response.Content.ReadAsStringAsync().Result;
              
                if (Convert.ToBoolean(data))
                {
                    //show success message
                    gvGames.EditIndex = -1;
                    BindGames();
                    ShowMessage("Game approved", "success");
                }
                else
                {
                    ShowMessage("Error approving game", "error");
                }

            }
        }

        protected void gvGames_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            UpdateGameByStoreManager(e);            
        }               

        private void UpdateGameByStoreManager(GridViewUpdateEventArgs e)
        {
            //getting key value, row id
            int GameId = Convert.ToInt32(gvGames.DataKeys[e.RowIndex].Value.ToString());
            //getting row field details  
            TextBox Discount = (TextBox)gvGames.Rows[e.RowIndex].FindControl("txtDiscount");

            Game editGame = new Game(GameId);
            editGame.CurrentDiscount = Convert.ToDecimal(Discount.Text);
            //do update here

            string apiUrl = "https://localhost:44368/v1";
            var serializedParam = JsonConvert.SerializeObject(editGame);
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(apiUrl + "/UpdateGameByStoreManager", content).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            if (Convert.ToBoolean(data))
            {
                //show success message
                gvGames.EditIndex = -1;
                BindGames();
                ShowMessage("Discount applied", "success");
            }
            else
            {
                ShowMessage("Error applying discount", "error");
            }
        }

        protected void gvGames_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGames.EditIndex = -1;
            BindGames();
        }       

        protected void gvGames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //getting key value, row id
            int gameId = Convert.ToInt32(gvGames.DataKeys[e.RowIndex].Value.ToString());

            //do delete here
            string apiUrl = "https://localhost:44368/v1/DeleteGame?id=" + gameId;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            if (Convert.ToBoolean(data))
            {
                //show success message
                gvGames.EditIndex = -1;
                BindGames();
                ShowMessage("Game deleted", "success");
            }
            else
            {
                ShowMessage("Error deleting game", "error");
            }

        }
    }
}