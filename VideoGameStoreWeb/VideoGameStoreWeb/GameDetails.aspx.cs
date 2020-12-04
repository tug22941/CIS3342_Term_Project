using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

namespace VideoGameStoreWeb
{
    public partial class GameDetails : System.Web.UI.Page
    {
        Utilities utl = new Utilities();       
        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!IsPostBack)
            {
                try
                {
                    User user = (User)Application["LoggedOnUser"];
                     
                    switch (user.UserType)
                    {
                        case "Producer":

                            break;
                        case "Store Manager":
                            
                            break;
                        case "Customer":
                            divAddReview.Visible = true;                           
                            
                            break;
                        default:

                            break;
                    }

                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }

                BindGame();
                BindReviews();
            }

        }

        protected HttpResponseMessage GetGame()
        {
            HttpClient client = new HttpClient();
            string id = Request.QueryString["id"];
            string apiUrl = "https://localhost:44368/v1/GetGame?id=" + id;
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            return response;
        }

        protected Game GetGame(int gameId)
        {
            HttpClient client = new HttpClient();           
            string apiUrl = "https://localhost:44368/v1/GetGame?id=" + gameId;
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            Game game = null;
            if (data != null && data != "")
            {
                game = (Game)JsonConvert.DeserializeObject<List<Game>>(data).ElementAt(0);
            }
            return game;
        }
        protected void BindGame()
        {
            try
            {
                HttpResponseMessage response = GetGame();
                var data = response.Content.ReadAsStringAsync().Result;

                if (data != null && data != "")
                {
                    gvGameDetails.DataSource = JsonConvert.DeserializeObject<List<Game>>(data);
                    gvGameDetails.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                ShowMessage("Error getting game", "error");
            }
            
        }
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Application["LoggedOnUser"] = "";
            Application["LoggedOnUserId"] = "";
            Application["LoggedOnUserType"] = "";
            Response.Redirect("Login.aspx");
        }
        
        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }


        protected void gvGameDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGameDetails.EditIndex = e.NewEditIndex;
            BindGame();
        }

        protected void gvGameDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Details")
            {
                //getting key value, row id
                int RowIndex = int.Parse(e.CommandArgument.ToString());
                int GameId = Convert.ToInt32(gvGameDetails.DataKeys[RowIndex]["ID"]);

                Response.Redirect("GameDetails.aspx?GameId=" + GameId);
            }
            if (e.CommandName == "Approve")
            {
                try
                {
                    //getting key value, row id
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    int gameId = Convert.ToInt32(gvGameDetails.DataKeys[RowIndex]["ID"]);

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
                        gvGameDetails.EditIndex = -1;
                        BindGame();
                        ShowMessage("Game approved", "success");
                    }
                    else
                    {
                        ShowMessage("Error approving game", "error");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error approving game", "error");
                }
            }
            if (e.CommandName == "AddToCart")
            {
                //getting key value, row id
                int RowIndex = int.Parse(e.CommandArgument.ToString());
                int gameId = Convert.ToInt32(gvGameDetails.DataKeys[RowIndex]["ID"]);
                Cart cart = (Cart)Application["Cart"];
                Game addedGame = GetGame(gameId);                
                if (addedGame != null)
                {
                    int quantityInCart = cart.Add(addedGame);
                    if (quantityInCart > 1)
                    {
                        ShowMessage(quantityInCart + " of this game in shopping cart.", "success");                        
                    }
                    else
                    {
                        ShowMessage("Added to cart", "success");
                    }
                    ucSessionInfo.UpdateCart();
                }
                else
                {
                    ShowMessage("The added game is no longer available.", "error");
                }                
                
            }
        }

        protected void gvGameDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            User user = (User)Application["LoggedOnUser"];
            if (user.UserType == "Producer")
            {
                UpdateGameByProducer(e);
            }
            else if (user.UserType == "Store Manager")
            {
                UpdateGameByStoreManager(e);
            }
        }

        private void UpdateGameByProducer(GridViewUpdateEventArgs e)
        {
            try
            {
                //getting key value, row id
                int GameId = Convert.ToInt32(gvGameDetails.DataKeys[e.RowIndex].Value.ToString());
                //getting row field details  
                TextBox Title = (TextBox)gvGameDetails.Rows[e.RowIndex].FindControl("txtTitle");
                TextBox Description = (TextBox)gvGameDetails.Rows[e.RowIndex].FindControl("txtDescription");
                TextBox ImageUrl = (TextBox)gvGameDetails.Rows[e.RowIndex].FindControl("txtImageUrl");
                DropDownList GameType = (DropDownList)gvGameDetails.Rows[e.RowIndex].FindControl("ddlGameType");
                DropDownList Platform = (DropDownList)gvGameDetails.Rows[e.RowIndex].FindControl("ddlPlatform");
                TextBox ReleaseDate = (TextBox)gvGameDetails.Rows[e.RowIndex].FindControl("txtReleaseDate");

                Game editGame = new Game(GameId, Title.Text, Description.Text, ImageUrl.Text, GameType.SelectedValue, Platform.SelectedValue, 0, 0, 0, null, Convert.ToDateTime(ReleaseDate.Text), false);
                //do update here

                string apiUrl = "https://localhost:44368/v1";
                var serializedParam = JsonConvert.SerializeObject(editGame);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(apiUrl + "/UpdateGameByProducer", content).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (Convert.ToBoolean(data))
                {
                    //show success message
                    gvGameDetails.EditIndex = -1;
                    BindGame();
                    ShowMessage("Game updated", "success");
                }
                else
                {
                    ShowMessage("Error updating game", "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating game", "error");
            }
            
        }

        private void UpdateGameByStoreManager(GridViewUpdateEventArgs e)
        {
            try
            {
                //getting key value, row id
                int GameId = Convert.ToInt32(gvGameDetails.DataKeys[e.RowIndex].Value.ToString());
                //getting row field details  
                TextBox Discount = (TextBox)gvGameDetails.Rows[e.RowIndex].FindControl("txtDiscount");

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
                    gvGameDetails.EditIndex = -1;
                    BindGame();
                    ShowMessage("Discount applied", "success");
                }
                else
                {
                    ShowMessage("Error applying discount", "error");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error applying discount", "error");
            }
            
        }

        protected void gvGameDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGameDetails.EditIndex = -1;
            BindGame();
        }

        

        protected void gvGameDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //getting key value, row id
                int gameId = Convert.ToInt32(gvGameDetails.DataKeys[e.RowIndex].Value.ToString());

                //do delete here
                string apiUrl = "https://localhost:44368/v1/DeleteGame?id=" + gameId;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (Convert.ToBoolean(data))
                {
                    //show success message
                    gvGameDetails.EditIndex = -1;
                    BindGame();
                    ShowMessage("Game deleted", "success");
                }
                else
                {
                    ShowMessage("Error deleting game", "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting game", "error");
            }
            
        }


        //reviews

       
        protected void gvReviews_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReviews.EditIndex = e.NewEditIndex;
            BindReviews();
        }

        protected void gvReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string gameId = Request.QueryString["id"];
                //getting key value, row id
                int reviewID = Convert.ToInt32(gvReviews.DataKeys[e.RowIndex].Value.ToString());
                //getting row field details 

                RadioButtonList GamePlay = (RadioButtonList)gvReviews.Rows[e.RowIndex].FindControl("rblGamePlay");
                RadioButtonList Graphics = (RadioButtonList)gvReviews.Rows[e.RowIndex].FindControl("rblGraphics");
                RadioButtonList ReplayValue = (RadioButtonList)gvReviews.Rows[e.RowIndex].FindControl("rblReplayValue");
                TextBox Comments = (TextBox)gvReviews.Rows[e.RowIndex].FindControl("txtComments");

                Review editReview = new Review(reviewID,
                        new Game(Convert.ToInt32(gameId)),
                        Convert.ToInt32(GamePlay.SelectedValue),
                        Convert.ToInt32(Graphics.SelectedValue),
                        Convert.ToInt32(ReplayValue.SelectedValue),
                        Comments.Text,
                        null);

                //do update here

                string apiUrl = "https://localhost:44368/v1";
                var serializedParam = JsonConvert.SerializeObject(editReview);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(apiUrl + "/UpdateReview", content).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (Convert.ToBoolean(data))
                {
                    //show success message
                    gvReviews.EditIndex = -1;
                    BindReviews();
                    ShowMessage("Review updated", "success");
                }
                else
                {
                    ShowMessage("Error updating review", "error");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error updating review", "error");
            }
            
        }

        protected void gvReviews_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvReviews.EditIndex = -1;
            BindReviews();
        }
              

        protected void gvReviews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //getting key value, row id
                int reviewID = Convert.ToInt32(gvReviews.DataKeys[e.RowIndex].Value.ToString());

                //do delete here
                string apiUrl = "https://localhost:44368/v1/DeleteReview?id=" + reviewID;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.DeleteAsync(apiUrl).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (Convert.ToBoolean(data))
                {
                    //show success message
                    gvReviews.EditIndex = -1;
                    BindReviews();
                    ShowMessage("Review deleted", "success");
                }
                else
                {
                    ShowMessage("Error deleting review", "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting review", "error");
            }
            
        }

        protected void lbtnAddReview_Click(object sender, EventArgs e)
        {
            try
            {
                string gameId = Request.QueryString["id"];
                User user = (User)Application["LoggedOnUser"];

                string apiUrl = "https://localhost:44368/v1";

                Review newReview = new Review();
                newReview.Game = new Game(Convert.ToInt32(gameId));
                newReview.GamePlay = Convert.ToInt32(rblNewGamePlay.SelectedValue);
                newReview.Graphics = Convert.ToInt32(rblNewGraphics.SelectedValue);
                newReview.ReplayValue = Convert.ToInt32(rblNewReplayValue.SelectedValue);
                newReview.Comments = txtNewComments.Text;
                newReview.Author = user;

                var serializedParam = JsonConvert.SerializeObject(newReview);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiUrl + "/AddReview", content).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (Convert.ToBoolean(data))
                {
                    BindReviews();
                    //review added
                    ShowMessage("Review added", "success");
                }
                else
                {
                    ShowMessage("Error adding review", "error");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error adding review", "error");
            }               
        }

        protected void BindReviews()
        {
            try
            {
                HttpClient client = new HttpClient();
                string gameId = Request.QueryString["id"];
                string apiUrl = "https://localhost:44368/v1/GetReviews?gameId=" + gameId;
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (data != null && data != "")
                {
                    gvReviews.DataSource = JsonConvert.DeserializeObject<List<Review>>(data);
                    gvReviews.DataBind();
                }
                else
                {
                    gvReviews.DataSource = null;
                    gvReviews.DataBind();
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error fetching reviews", "error");
            }            
            
        }

    }

   
}