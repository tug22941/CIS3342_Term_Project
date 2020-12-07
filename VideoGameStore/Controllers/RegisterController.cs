using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using VideoGameLibrary; //Shared Library containig video-game store utilities
//using VideoGameStore.Models;

using System.Data.SqlClient;
using System.Data;

//Web API controller responsible for adding a new user to the database

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
       

        //Testing HTTP Get
        [HttpGet]
        public string GetTest()
        {
            return "Hello";
        }

        //POST action method that adds user account to database
        [HttpPost("AddAccount")]
        public string AddAccount([FromBody] User newUser)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_RegisterAccount";

            //Create and define SqlParameter object for user First Name
            SqlParameter userFN = new SqlParameter("@firstName", newUser.FirstName);
            userFN.Direction = ParameterDirection.Input;
            userFN.SqlDbType = SqlDbType.VarChar;
            userFN.Size = 50;
            objCmd.Parameters.Add(userFN);

            //Create and define SqlParameter object for user Last Name
            SqlParameter userLN = new SqlParameter("@lastName", newUser.LastName);
            userLN.Direction = ParameterDirection.Input;
            userLN.SqlDbType = SqlDbType.VarChar;
            userLN.Size = 50;
            objCmd.Parameters.Add(userLN);

            //Create and define SqlParameter object for user Phone Number
            SqlParameter userPhone = new SqlParameter("@phone", newUser.Phone);
            userPhone.Direction = ParameterDirection.Input;
            userPhone.SqlDbType = SqlDbType.VarChar;
            userPhone.Size = 50;
            objCmd.Parameters.Add(userPhone);

            //Create and define SqlParameter object for user Email
            SqlParameter email = new SqlParameter("@email", newUser.Email);
            email.Direction = ParameterDirection.Input;
            email.SqlDbType = SqlDbType.VarChar;
            email.Size = 100;
            objCmd.Parameters.Add(email);

            //Create and define SqlParameter object for userName
            SqlParameter UserName = new SqlParameter("@username", newUser.Username);
            UserName.Direction = ParameterDirection.Input;
            UserName.SqlDbType = SqlDbType.VarChar;
            UserName.Size = 50;
            objCmd.Parameters.Add(UserName);

            //Create and define SqlParameter object for user Password
            SqlParameter password = new SqlParameter("@password", newUser.Password);
            password.Direction = ParameterDirection.Input;
            password.SqlDbType = SqlDbType.VarChar;
            password.Size = 50;
            objCmd.Parameters.Add(password);

            //Create and define SqlParameter object for user Address
            SqlParameter address = new SqlParameter("@address", newUser.Address);
            address.Direction = ParameterDirection.Input;
            address.SqlDbType = SqlDbType.VarChar;
            address.Size = 50;
            objCmd.Parameters.Add(address);

            //Create and define SqlParameter object for user City
            SqlParameter city = new SqlParameter("@city", newUser.City);
            city.Direction = ParameterDirection.Input;
            city.SqlDbType = SqlDbType.VarChar;
            city.Size = 50;
            objCmd.Parameters.Add(city);

            //Create and define SqlParameter object for user City
            SqlParameter state = new SqlParameter("@state", newUser.State);
            state.Direction = ParameterDirection.Input;
            state.SqlDbType = SqlDbType.VarChar;
            state.Size = 50;
            objCmd.Parameters.Add(state);

            //Create and define SqlParameter object for user Zipcode
            SqlParameter zip = new SqlParameter("@zip", newUser.Zip);
            zip.Direction = ParameterDirection.Input;
            zip.SqlDbType = SqlDbType.VarChar;
            zip.Size = 50;
            objCmd.Parameters.Add(zip);

            //Create and define SqlParameter object for user Type
            SqlParameter userType = new SqlParameter("@userType", newUser.Zip);
            userType.Direction = ParameterDirection.Input;
            userType.SqlDbType = SqlDbType.VarChar;
            userType.Size = 50;
            objCmd.Parameters.Add(userType);

            //Create and define SqlParameter object for Security Question 1
            SqlParameter secQ1 = new SqlParameter("@secQ1", newUser.FavoriteGame);
            secQ1.Direction = ParameterDirection.Input;
            secQ1.SqlDbType = SqlDbType.VarChar;
            secQ1.Size = 50;
            objCmd.Parameters.Add(secQ1);

            //Create and define SqlParameter object for Security Question 2
            SqlParameter secQ2 = new SqlParameter("@secQ2", newUser.MotherMaidenName);
            secQ2.Direction = ParameterDirection.Input;
            secQ2.SqlDbType = SqlDbType.VarChar;
            secQ2.Size = 50;
            objCmd.Parameters.Add(secQ2);

            //Create and define SqlParameter object for Security Question 3
            SqlParameter secQ3 = new SqlParameter("@secQ3", newUser.FavoriteSport);
            secQ3.Direction = ParameterDirection.Input;
            secQ3.SqlDbType = SqlDbType.VarChar;
            secQ3.Size = 50;
            objCmd.Parameters.Add(secQ3);

            //Check the return value for transaction success value
            int returnValue = objDB.DoUpdateUsingCmdObj(objCmd);

            if(returnValue > 0)
            {
                return "Account Successfully Created!";
            }
            else
            {
                return "An Error Has Occured. Account NOT Created!";
            }
        }
    }
}