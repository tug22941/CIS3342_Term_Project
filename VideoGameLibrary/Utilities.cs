using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace VideoGameLibrary
{
    public class Utilities
    {
        const string ENC_KEY = "qbl3JUVEGuL5kUHM57QHWGm1";
        DBConnect objDB = new DBConnect();
                       
        public User Login(String username, String password)
        {            
            SqlCommand cmd = new SqlCommand("TP_AuthenticateUser");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            if(ds.Tables[0].Rows.Count > 0)
            {
                User user = new User(Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString()),
                    ds.Tables[0].Rows[0]["FirstName"].ToString(), 
                    ds.Tables[0].Rows[0]["LastName"].ToString(),
                    ds.Tables[0].Rows[0]["Username"].ToString(),
                    null,
                    ds.Tables[0].Rows[0]["Email"].ToString(),
                    ds.Tables[0].Rows[0]["Phone"].ToString(),
                    ds.Tables[0].Rows[0]["Address"].ToString(),
                    ds.Tables[0].Rows[0]["City"].ToString(),
                    ds.Tables[0].Rows[0]["State"].ToString(),
                    ds.Tables[0].Rows[0]["Zip"].ToString(),
                    ds.Tables[0].Rows[0]["UserType"].ToString(),
                    "",
                    "",
                    "",
                    Convert.ToBoolean(ds.Tables[0].Rows[0]["Verified"]),
                    ""
                    );
                return user;
            }
            return null;
        }

        public bool Register(User user)
        {
            SqlCommand cmd = new SqlCommand("TP_AddUser");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@city", user.City);
            cmd.Parameters.AddWithValue("@state", user.State);
            cmd.Parameters.AddWithValue("@zip", user.Zip);
            cmd.Parameters.AddWithValue("@userType", user.UserType);
            cmd.Parameters.AddWithValue("@favoriteGame", user.FavoriteGame);
            cmd.Parameters.AddWithValue("@motherMaidenName", user.MotherMaidenName);
            cmd.Parameters.AddWithValue("@favoriteSport", user.FavoriteSport);           
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

        public bool VerifyUser(User user)
        {
            SqlCommand cmd = new SqlCommand("TP_VerifyUser");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@url", user.VerificationUrl);            
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

       

        public bool UpdateGameByProducer(Game game)
        {
            SqlCommand cmd = new SqlCommand("TP_UpdateGameByProducer");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", game.ID);
            cmd.Parameters.AddWithValue("@title", game.Title);
            cmd.Parameters.AddWithValue("@description", game.Description);
            cmd.Parameters.AddWithValue("@imageUrl", game.ImageUrl);
            cmd.Parameters.AddWithValue("@type", game.Type);
            cmd.Parameters.AddWithValue("@platform", game.Platform);
            cmd.Parameters.AddWithValue("@releaseDate", game.ReleaseDate);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }
        public bool UpdateGameByStoreManager(Game game)
        {
            SqlCommand cmd = new SqlCommand("TP_UpdateGameByStoreManager");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", game.ID);            
            cmd.Parameters.AddWithValue("@currentDiscount", game.CurrentDiscount);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }
        public bool ApproveGame(Game game)
        {
            SqlCommand cmd = new SqlCommand("TP_ApproveGame");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", game.ID);            
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

        public bool AddGame(Game game)
        {
            SqlCommand cmd = new SqlCommand("TP_AddGame");
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@title", game.Title);
            cmd.Parameters.AddWithValue("@description", game.Description);
            cmd.Parameters.AddWithValue("@imageUrl", game.ImageUrl);
            cmd.Parameters.AddWithValue("@type", game.Type);
            cmd.Parameters.AddWithValue("@platform", game.Platform);
            cmd.Parameters.AddWithValue("@retailPrice", game.RetailPrice);
            cmd.Parameters.AddWithValue("@producerId", game.Producer.Id);
            cmd.Parameters.AddWithValue("@releaseDate", game.ReleaseDate);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

        public List<Game> GetGames(int userId)
        {
            SqlCommand cmd = new SqlCommand("TP_GetGamesByUserId");
            cmd.Parameters.AddWithValue("@id", userId);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            List<Game> games = new List<Game>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for(int i=0; i< ds.Tables[0].Rows.Count; i++)
                {
                    Game game = new Game(Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                    ds.Tables[0].Rows[i]["Title"].ToString(),
                    ds.Tables[0].Rows[i]["Description"].ToString(),
                    ds.Tables[0].Rows[i]["ImageUrl"].ToString(),
                    ds.Tables[0].Rows[i]["Type"].ToString(),
                    ds.Tables[0].Rows[i]["Platform"].ToString(),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["RetailPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscountedPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentDiscount"]),
                    null,
                    Convert.ToDateTime(ds.Tables[0].Rows[i]["ReleaseDate"]),                    
                    Convert.ToBoolean(ds.Tables[0].Rows[i]["ListedForSale"])
                    );
                    games.Add(game);
                }                
                return games;
            }
            return null;
        }

        public List<Game> GetGamesPendingApproval()
        {
            SqlCommand cmd = new SqlCommand("TP_GetGamesPendingApproval");           
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            List<Game> games = new List<Game>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Game game = new Game(Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                    ds.Tables[0].Rows[i]["Title"].ToString(),
                    ds.Tables[0].Rows[i]["Description"].ToString(),
                    ds.Tables[0].Rows[i]["ImageUrl"].ToString(),
                    ds.Tables[0].Rows[i]["Type"].ToString(),
                    ds.Tables[0].Rows[i]["Platform"].ToString(),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["RetailPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscountedPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentDiscount"]),
                    null,
                    Convert.ToDateTime(ds.Tables[0].Rows[i]["ReleaseDate"]),
                    Convert.ToBoolean(ds.Tables[0].Rows[i]["ListedForSale"])
                    );
                    games.Add(game);
                }
                return games;
            }
            return null;
        }
        public List<Game> GetGame(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_GetGameById");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {                
                    Game game = new Game(Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString()),
                    ds.Tables[0].Rows[0]["Title"].ToString(),
                    ds.Tables[0].Rows[0]["Description"].ToString(),
                    ds.Tables[0].Rows[0]["ImageUrl"].ToString(),
                    ds.Tables[0].Rows[0]["Type"].ToString(),
                    ds.Tables[0].Rows[0]["Platform"].ToString(),
                    Convert.ToDecimal(ds.Tables[0].Rows[0]["RetailPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[0]["DiscountedPrice"]),
                    Convert.ToDecimal(ds.Tables[0].Rows[0]["CurrentDiscount"]),
                    null,
                    Convert.ToDateTime(ds.Tables[0].Rows[0]["ReleaseDate"]),
                    Convert.ToBoolean(ds.Tables[0].Rows[0]["ListedForSale"])
                    );
                List<Game> games = new List<Game>();
                games.Add(game);
                return games;
            }
            return null;
        }

        public List<Review> GetReviewsByGameId(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_GetReviewsByGameId");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            List<Review> reviews = new List<Review>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Review review = new Review(Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                    null,
                    Convert.ToInt32(ds.Tables[0].Rows[i]["GamePlay"]),
                    Convert.ToInt32(ds.Tables[0].Rows[i]["Graphics"]),
                    Convert.ToInt32(ds.Tables[0].Rows[i]["ReplayValue"]),
                    ds.Tables[0].Rows[i]["Comment"].ToString(),                    
                    new User(Convert.ToInt32(ds.Tables[0].Rows[i]["AuthorID"]))
                    );
                    reviews.Add(review);
                }
                return reviews;
            }
            return null;
        }

        public bool AddReview(Review review)
        {
            SqlCommand cmd = new SqlCommand("TP_AddReview");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@gameId", review.Game.ID);
            cmd.Parameters.AddWithValue("@gamePlay", review.GamePlay);
            cmd.Parameters.AddWithValue("@graphics", review.Graphics);
            cmd.Parameters.AddWithValue("@replayValue", review.ReplayValue);           
            cmd.Parameters.AddWithValue("@comments", review.Comments);
            cmd.Parameters.AddWithValue("@authorID", review.Author.Id);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

        public bool UpdateReview(Review review)
        {
            SqlCommand cmd = new SqlCommand("TP_UpdateReview");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", review.Id);
            cmd.Parameters.AddWithValue("@gamePlay", review.GamePlay);
            cmd.Parameters.AddWithValue("@graphics", review.Graphics);
            cmd.Parameters.AddWithValue("@replayValue", review.ReplayValue);
            cmd.Parameters.AddWithValue("@comments", review.Comments);            
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }
        public bool DeleteReview(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_DeleteReview");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }
        public bool DeleteGame(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_DeleteGame");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }

        public List<Order> GetOrdersByCustomerId(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_GetOrdersByCustomerId");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            List<Order> orders = new List<Order>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Order order = new Order(Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                    null,
                    ds.Tables[0].Rows[i]["GameTitle"].ToString(),
                    new User(Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"])),
                    Convert.ToDecimal(ds.Tables[0].Rows[i]["PurchasePrice"]),
                    Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]),                    
                    Convert.ToDateTime(ds.Tables[0].Rows[i]["PurchaseDate"])
                    );
                    orders.Add(order);
                }
                return orders;
            }
            return null;
        }

        public string GetOrdersJsonStringByCustomerId(int id)
        {
            SqlCommand cmd = new SqlCommand("TP_GetOrdersByCustomerId");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = objDB.GetDataSetUsingCmdObj(cmd);
            string orders = "";
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                orders = JsonConvert.SerializeObject(ds.Tables[0], Newtonsoft.Json.Formatting.Indented);
            }
            return orders;
        }

        public bool AddOrder(Order order)
        {
            SqlCommand cmd = new SqlCommand("TP_AddOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@gameId", order.Game.ID);
            cmd.Parameters.AddWithValue("@userId", order.Customer.Id);
            cmd.Parameters.AddWithValue("@purchasePrice", order.PurchasePrice);
            cmd.Parameters.AddWithValue("@quantity", order.Quantity);                        
            int result = objDB.DoUpdateUsingCmdObj(cmd);
            return (result > 0);
        }


        public string Decrypt(string cipherString)
        {
            byte[] keyArray;            
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            System.Configuration.AppSettingsReader settingsReader =  new AppSettingsReader();           
            keyArray = UTF8Encoding.UTF8.GetBytes(ENC_KEY);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();            
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;           
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);            
            tdes.Clear();           
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();           
            keyArray = UTF8Encoding.UTF8.GetBytes(ENC_KEY);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();            
            tdes.Key = keyArray;          
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();           
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);            
            tdes.Clear();           
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


    }
}
