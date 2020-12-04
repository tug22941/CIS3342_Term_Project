using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameLibrary
{
	public class Cart
	{
		public List<CartItem> CartItems { get; set; }
		public Decimal Total { 
			get {
				Decimal total = 0;
				foreach (CartItem c in CartItems)
				{
					total += c.ItemTotalCost;
				}
				return total;
			} 
		}

		public Cart()
		{
			CartItems = new List<CartItem>();
		}

		public int Add(Game game)
        {
			foreach (CartItem c in CartItems)
			{
				if (c.Game.ID == game.ID)
				{
					//game already in cart, increase quantity
					c.Quantity ++;
					return c.Quantity;
				}                
			}
			//add new game to cart
			CartItems.Add(new CartItem(game, 1));
			return 1;
		}

		
		
	}
}
