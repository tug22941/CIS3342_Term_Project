using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameStore.Services;
using VideoGameLibrary;
using System.Data;

/*  CIS3342-001
 *  Term Project:Video Game Store
 *  Haolin Song & Jonah Saywonson
 */

//This Restful Web API includes actions method for The Video Game Store Application

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoGameStore.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class VGSController : ControllerBase
    {
        private VideoGameStoreServices services = new VideoGameStoreServices();
                
        [HttpPost]
        [Route("Login")]
        public User Login([FromBody] User user)
        {
            return services.Login(user.Username, user.Password);
        }

        [HttpPost]
        [Route("Register")]
        public bool Register([FromBody] User user)
        {
            return services.Register(user);
        }
               
        [HttpPut]
        [Route("VerifyUser")]
        public bool VerifyUser([FromBody] User user)
        {
            return services.VerifyUser(user);
        }                

        [Route("GetGames")]
        [HttpGet("userId")]
        public List<Game> GetGames(int userId)
        {
            return services.GetGames(userId);
        }

        [Route("GetGame")]
        [HttpGet("id")]
        public List<Game> GetGame(int id)
        {
            return services.GetGame(id);
        }

        [Route("GetGamesPendingApproval")]
        [HttpGet]
        public List<Game> GetGamesPendingApproval()
        {
            return services.GetGamesPendingApproval();
        }

        [Route("DeleteGame")]
        [HttpDelete("id")]
        public bool DeleteGame(int id)
        {
            return services.DeleteGame(id);
        }

        [HttpPut]
        [Route("UpdateGameByProducer")]
        public bool UpdateGameByProducer([FromBody] Game game)
        {
            return services.UpdateGameByProducer(game);
        }

        [HttpPut]
        [Route("UpdateGameByStoreManager")]
        public bool UpdateGameByStoreManager([FromBody] Game game)
        {
            return services.UpdateGameByStoreManager(game);
        }

        [HttpPut]
        [Route("ApproveGame")]
        public bool ApproveGame([FromBody] Game game)
        {
            return services.ApproveGame(game);
        }

        [HttpPost]
        [Route("AddGame")]
        public bool AddGame([FromBody] Game game)
        {
            return services.AddGame(game);
        }

        [Route("GetReviews")]
        [HttpGet("gameId")]
        public List<Review> GetReviews(int gameId)
        {
            return services.GetReviews(gameId);
        }

        [HttpPost]
        [Route("AddReview")]
        public bool AddReview([FromBody] Review review)
        {
            return services.AddReview(review);
        }

        [Route("DeleteReview")]
        [HttpDelete("id")]
        public bool DeleteReview(int id)
        {
            return services.DeleteReview(id);
        }

        [HttpPut]
        [Route("UpdateReview")]
        public bool UpdateReview([FromBody] Review review)
        {
            return services.UpdateReview(review);
        }

        [Route("GetOrders")]
        [HttpGet("customerId")]
        public List<Order> GetOrders(int customerId)
        {
            return services.GetOrders(customerId);
        }

        [HttpPost]
        [Route("AddOrder")]
        public bool AddOrder([FromBody] Order order)
        {
            return services.AddOrder(order);
        }
    }
}
