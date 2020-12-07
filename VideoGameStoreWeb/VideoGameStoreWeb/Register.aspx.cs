  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using VideoGameLibrary; //Shared Library containig video-game store utilities

using System.Web.Script.Serialization;  // needed for JSON serializers
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request

using System.Data.SqlClient;
using System.Data;

namespace RestaurantReviewSystem
{
    public partial class Register : System.Web.UI.Page
    {
        Utilities utl = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Method that creates a webRequest to Restful WebAPI and adds a User to the Database
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //Display success/failure message of username within database
            bool found = UserFound(txtUsername.Text);
            if (found == true)
            {
                lblMessage.Text = "Username Unavailable!";
                txtUsername.Text = "";
                txtUsername.Focus();
            }
            else
            {
                User user = new User();
                //Collect input from form controls
                //Create user object and assign user input as value

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

                user.UserType = rblUserType.SelectedValue;

                user.FavoriteGame = txtFavoriteGame.Text;
                user.MotherMaidenName = txtMotherMaidenName.Text;
                user.FavoriteSport = txtFavoriteSport.Text;

                user.Id = 0;
                user.Verified = true;
                user.VerificationUrl = "";
                //Display success/failure message from Web API within label
                lblMessage.Text = AddAccount(user);

                //enable activate tick event event for Timer
                Timer1.Enabled = true;
                Timer1.Interval = 1000;
                Timer1.Tick += new EventHandler<EventArgs>(Timer1_Tick);
            }
        }

        //Method that creates a webRequest to Restful WebAPI and returns that status of userName within Database
        public bool UserFound(string userName)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_FindUsername";

            //Create and define SqlParameter object for username
            SqlParameter uName = new SqlParameter("@userName", userName);
            uName.Direction = ParameterDirection.Input;
            uName.SqlDbType = SqlDbType.VarChar;
            uName.Size = 50;
            objCmd.Parameters.Add(uName);

            //Execute stored procedure using DBConnect object and the SQLCommand objec and store DataSet in variable
            DataSet myDS = objDB.GetDataSetUsingCmdObj(objCmd);

            if (myDS == null)
            {
                return true; //database error no dataset returne
            }
            else
            {
                try
                {
                    //Check to see if Cell value is empty
                    string UN = (string)objDB.GetField("Username", 0);
                    return true; //user is found
                }
                catch
                {
                    //null value within cell, no user
                    return false;
                }
            }
        }

        //Method that creates a webRequest to Restful WebAPI and adds a User to the Database
        public string AddAccount(User user)
        {

            //Serialize User object inot a JSON string
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(user);

            string url = "https://localhost:44368/api/Register/AddAccount";


            // Create an HTTP Web Request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = jsonUser.Length;
            request.ContentType = "application/json";

            //Write the JSON data to the Web Request
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonUser);
            writer.Flush();
            writer.Close();

            //Get the HTTP Web Response from the server
            WebResponse response = request.GetResponse();

            // Read the data from the Web Response
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize a JSON string into a string
            string result = Convert.ToString(data);

            return result;
        }

        protected void ShowMessage(string Message, String messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + messageType + "');", true);
        }

        //Tick event for timer. Page Transfered occurs at count 0
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int timeNow = (Convert.ToInt32(Label1.Text));
                timeNow = timeNow - 1;
                Label1.Text = timeNow.ToString();
                if (timeNow < 0)
                {
                    Timer1.Enabled = false;
                    Server.Transfer("Login.aspx");
                }
            }
            catch
            {
                Label1.Text = "3";
                int timeNow = (Convert.ToInt32(Label1.Text));
                timeNow = timeNow - 1;
                Label1.Text = timeNow.ToString();
            }

        }
    }
}