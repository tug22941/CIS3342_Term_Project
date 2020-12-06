using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VideoGameLibrary;

namespace VideoGameStore.Services
{
    public class VideoGameStoreServices:IVideoGameStoreServices
    {
        Utilities utl = new Utilities();
        public User Login(string username, string password)
        {
            return utl.Login(username, password);            
        }
        public bool Register(User user)
        {
            return utl.Register(user);
        }
        public bool VerifyUser(User user)
        {
            return utl.VerifyUser(user);
        }        
        public List<Game> GetGames(int userId)
        {
            return utl.GetGames(userId);
        }
        public List<Game> GetGamesPendingApproval()
        {
            return utl.GetGamesPendingApproval();
        }
        public List<Game> GetGame(int id)
        {
            return utl.GetGame(id);
        }
        public bool UpdateGameByProducer(Game game)
        {
            return utl.UpdateGameByProducer(game);
        }
        public bool UpdateGameByStoreManager(Game game)
        {
            return utl.UpdateGameByStoreManager(game);
        }
        public bool ApproveGame(Game game)
        {
            return utl.ApproveGame(game);
        }
        public bool AddGame(Game game)
        {
            return utl.AddGame(game);
        }
        public bool DeleteGame(int id)
        {
            return utl.DeleteGame(id);
        }
        public List<Review> GetReviews(int gameId)
        {
            return utl.GetReviewsByGameId(gameId);
        }
        public bool AddReview(Review review)
        {
            return utl.AddReview(review);
        }
        public bool DeleteReview(int id)
        {
            return utl.DeleteReview(id);
        }
        public bool UpdateReview(Review review)
        {
            return utl.UpdateReview(review);
        }
        public List<Order> GetOrders(int customerId)
        {
            return utl.GetOrdersByCustomerId(customerId);
        }
        public bool AddOrder(Order order)
        {
            return utl.AddOrder(order);
        }
    }
}
