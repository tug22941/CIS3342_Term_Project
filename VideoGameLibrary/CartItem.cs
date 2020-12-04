using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibrary
{
    public class CartItem
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
        public Decimal ItemTotalCost { get { return Game.DiscountedPrice * Quantity; } }

        public CartItem(Game game, int quantity)
        {
            Game = game;
            Quantity = quantity;            
        }
        public CartItem()
        {
            Game = null;
            Quantity = 0;
        }
    }
}
