using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VideoGameLibrary;

namespace VideoGameStore.Services
{
    interface IVideoGameStoreServices
    {
        User Login(string username, string password);
        bool Register(User user);
        bool VerifyUser(User user);        
        List<Game> GetGames(int userId);
        List<Game> GetGamesPendingApproval();
        List<Game> GetGame(int id);
        bool UpdateGameByProducer(Game game);
        bool UpdateGameByStoreManager(Game game);
        bool ApproveGame(Game game);
        bool AddGame(Game game);
        bool DeleteGame(int id);
        List<Review> GetReviews(int gameId);
        bool AddReview(Review review);
        bool DeleteReview(int id);
        bool UpdateReview(Review review);
        List<Order> GetOrders(int customerId);
        bool AddOrder(Order order);
    }
}
