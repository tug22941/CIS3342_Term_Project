using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameLibrary
{
    public class Review
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public int GamePlay { get; set; }
        public int Graphics { get; set; }
        public int ReplayValue { get; set; }       
        public String Comments { get; set; }
        public User Author { get; set; }

        public Review(int id, Game game, int gameplay, int graphics, int replayValue, String comments, User author)
        {
            Id = id;
            Game = game;
            GamePlay = gameplay;
            Graphics = graphics;
            ReplayValue = replayValue;
            Comments = comments;           
            Author = author;
        }
        public Review()
        {
            Id = 0;
            Game = new Game();
            GamePlay = 0;
            Graphics = 0;
            ReplayValue = 0;
            Comments = "";
            Author = new User();
        }
    }
}
