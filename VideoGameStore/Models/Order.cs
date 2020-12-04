using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameStore.Models
{
	public class Order
	{
		public int ID { get; set; }
		public Game Game { get; set; }
		public User Customer { get; set; }
		public Decimal PurchasePrice { get; set; }
		public int Quantity { get; set; }

		public Order(int id, Game game, User customer, Decimal purchasePrice, int quantity)
		{
			ID = id;
			Game = game;
			Customer = customer;
			PurchasePrice = purchasePrice;
			Quantity = quantity;
		}
	}
}
