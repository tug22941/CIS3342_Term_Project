﻿using Newtonsoft.Json;
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

/*  CIS3342-001
 *  Term Project:Video Game Store
 *  Haolin Song & Jonah Saywonson
 */

//This Page contains the codebhind for the Login aspx p age

namespace VideoGameStoreWeb
{
    public partial class Login : System.Web.UI.Page
    {
        Utilities utl = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Cookie object for exsisting username and password and place values within textbox
            if (!IsPostBack)
            {
                if (Request.Cookies["vgsUsername"] != null)
                {
                    txtLoginUsername.Text = Request.Cookies["vgsUsername"].Value;
                }                   

                if (Request.Cookies["vgsPassword"] != null)
                {
                    txtLoginPassword.Attributes.Add("value", Request.Cookies["vgsPassword"].Value);
                }
                                   
                if (Request.Cookies["vgsUsername"] != null && Request.Cookies["vgsPassword"] != null)
                {
                    chkRememberMe.Checked = true;
                }                    
            }
        }

        //Event handler for Login button - sends HTTP request to the login action method of the VGS Web API
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:44368/v1";
            User user = new User();
            user.Username = txtLoginUsername.Text;

            //password is sent encrypted
            user.Password = utl.Encrypt(txtLoginPassword.Text);
            var serializedParam = JsonConvert.SerializeObject(user);
            HttpClient client = new HttpClient();

            //Asynchronoous Postback
            HttpContent content = new StringContent(serializedParam, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl + "/Login", content).Result;            
            var data = response.Content.ReadAsStringAsync().Result;
            if(data != null && data != "")
            {
                //valid user has authenticated                
                User loggedOnUser = JsonConvert.DeserializeObject<User>(data);
                //allow verified users to login
                if (loggedOnUser.Verified)
                {
                    Application["LoggedOnUser"] = loggedOnUser;
                    Application["LoggedOnUserId"] = loggedOnUser.Id;
                    Application["LoggedOnUserType"] = loggedOnUser.UserType;
                    Application["Cart"] = new Cart();
                    SetRememberMeCookie();
                    Response.Redirect("Home.aspx");                    
                }
                else
                //if user has not yet been verified, prevent from logging in
                {
                    //instruct user to verify account
                    ShowMessage("Your account is pending verification. Please check your email for instructions.", "warning");
                }                
            }
            else
            {
                //invalid login
                ShowMessage("Invalid username or password", "error");
            }
        }

        //method that activates the Script Manager - Displays Required field message for empty inputs
        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }

      
        //Set value and expiration period for Username and Password
        protected void SetRememberMeCookie()
        {
            if (chkRememberMe.Checked == true)
            {
                Response.Cookies["vgsUsername"].Value = txtLoginUsername.Text;
                Response.Cookies["vgsPassword"].Value = txtLoginPassword.Text;
                Response.Cookies["vgsUsername"].Expires = DateTime.Now.AddDays(15);
                Response.Cookies["vgsPassword"].Expires = DateTime.Now.AddDays(15);
            }

            else

            {
                Response.Cookies["vgsUsername"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["vgsPassword"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}