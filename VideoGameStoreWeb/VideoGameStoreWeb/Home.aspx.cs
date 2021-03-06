﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoGameLibrary;

/*  CIS3342-001
 *  Term Project:Video Game Store
 *  Haolin Song & Jonah Saywonson
 */

//This page contains the code behind for the Home Page

namespace VideoGameStoreWeb
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve current user from application object
            if (!IsPostBack)
            {
                try
                {
                    User user = (User)Application["LoggedOnUser"];                    

                    BindGames();    //Call to BindGame method
                    switch (user.UserType)
                    {
                        case "Producer":
                            divAddGame.Visible = true;
                            break;
                        case "Store Manager":                            
                            break;
                        case "Customer":                            
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
            }  
        }

        //Method that sends HTTP request to the GetGames action method of the VGS Restful Web API
        protected void BindGames()
        {
            try
            {
                HttpClient client = new HttpClient();
                User user = (User)Application["LoggedOnUser"];
                string apiUrl = "https://localhost:44368/v1/GetGames?userId=" + user.Id;

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                //if the data, convert and databind to games gridview
                if (data != null && data != "")
                {
                    List<Game> games = JsonConvert.DeserializeObject<List<Game>>(data);
                    gvGames.DataSource = games;
                    gvGames.DataBind();
                    BindGameFilters(games);//Bind dropdown filter value
                }
                else
                {
                    gvGames.DataSource = null;
                    gvGames.DataBind();
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error fetching games", "error");
            }            
            
        }

        //Method that binds the value of the game filter dropdown to value from database
        private void BindGameFilters(List<Game> games)
        {
            foreach (Game game in games)
            {
                ListItem platFormItem = new ListItem(game.Platform, game.Platform);
                if (!ddlPlatformFilter.Items.Contains(platFormItem))
                {
                    ddlPlatformFilter.Items.Add(platFormItem);
                }

                ListItem genreItem = new ListItem(game.Type, game.Type);
                if (!ddlGenreFilter.Items.Contains(genreItem))
                {
                    ddlGenreFilter.Items.Add(genreItem);
                }
            }
        }


        //Method that displays required field message of all applicable inputs
        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }
                
        //Event handler for moving swapping out game title at the current index
        protected void gvGames_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGames.EditIndex = e.NewEditIndex;
            BindGames();    //Call to BindGame method
        }

        //Event handler that diplays game detals/approve game as an object
        protected void gvGames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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

                    //Issue Restful Web API call to the ApproveGame acion method of the VGSController
                    string apiUrl = "https://localhost:44368/v1";
                    var serializedParam = JsonConvert.SerializeObject(approvedGame);
                    HttpClient client = new HttpClient();
                    HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(apiUrl + "/ApproveGame", content).Result;
                    var data = response.Content.ReadAsStringAsync().Result;

                    //Display success/failure message depending on response data
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
            catch(Exception ex)
            {
                ShowMessage("Error approving game", "error");
            }
        }

        protected void gvGames_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            User user = (User)Application["LoggedOnUser"];
            if (user.UserType == "Producer")
            {
                UpdateGameByProducer(e);
            }
            else if(user.UserType == "Store Manager")
            {
                UpdateGameByStoreManager(e);
            }
        }

        //Method that allows Game Producers to create new game titles
        private void UpdateGameByProducer(GridViewUpdateEventArgs e)
        {
            try
            {
                //getting key value, row id
                int GameId = Convert.ToInt32(gvGames.DataKeys[e.RowIndex].Value.ToString());
                //getting row field details  
                TextBox Title = (TextBox)gvGames.Rows[e.RowIndex].FindControl("txtTitle");
                TextBox Description = (TextBox)gvGames.Rows[e.RowIndex].FindControl("txtDescription");
                TextBox ImageUrl = (TextBox)gvGames.Rows[e.RowIndex].FindControl("txtImageUrl");
                DropDownList GameType = (DropDownList)gvGames.Rows[e.RowIndex].FindControl("ddlGameType");
                DropDownList Platform = (DropDownList)gvGames.Rows[e.RowIndex].FindControl("ddlPlatform");
                TextBox ReleaseDate = (TextBox)gvGames.Rows[e.RowIndex].FindControl("txtReleaseDate");

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
                    gvGames.EditIndex = -1;
                    BindGames();
                    ShowMessage("Game updated", "success");
                }
                else
                {
                    ShowMessage("Error updating game", "error");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error updating game", "error");
            }
            
        }

        //Event handler that allows Store manager to Edit the discount rate of a store title
        private void UpdateGameByStoreManager(GridViewUpdateEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                ShowMessage("Error applying discount", "error");
            }
            
        }

        //Event handler to exit the edit mode of the Games Gridview
        protected void gvGames_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGames.EditIndex = -1;
            BindGames();
        }

        //Event handler that allows producers to create new game titles
        protected void btnAddGame_Click(object sender, EventArgs e)
        {
            try
            {
                User producer = (User)Application["LoggedOnUser"];
                string apiUrl = "https://localhost:44368/v1";
                Game game = new Game();
                game.Title = txtNewTitle.Text;
                game.Description = txtNewDescription.Text;
                game.ImageUrl = txtNewImageUrl.Text;
                game.Type = ddlNewType.SelectedValue;
                game.Platform = ddlNewPlatform.SelectedValue;
                game.RetailPrice = Convert.ToDecimal(txtNewRetailPrice.Text);
                game.Producer = producer;
                game.ReleaseDate = Convert.ToDateTime(txtNewReleaseDate.Text);
                var serializedParam = JsonConvert.SerializeObject(game);
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiUrl + "/AddGame", content).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (Convert.ToBoolean(data))
                {
                    BindGames();
                    //game added
                    ShowMessage("Game added", "success");
                    //clear inputs after successfull add
                    txtNewTitle.Text = "";
                    txtNewImageUrl.Text = "";
                    txtNewRetailPrice.Text = "";
                    txtNewReleaseDate.Text = "";
                    txtNewDescription.Text = "";
                    ddlNewPlatform.SelectedIndex = 0;
                    ddlNewType.SelectedIndex = 0;

                }
                else
                {                    
                    ShowMessage("Error adding game", "error");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Error adding game", "error");
            }
            
        }

        //Event handler to remove a game title from the user homepage
        protected void gvGames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                ShowMessage("Error deleting game", "error");
            }          

        }
    }
}
