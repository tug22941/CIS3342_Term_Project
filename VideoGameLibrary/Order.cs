using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameLibrary
{
	public class Order
	{
		public int ID { get; set; }
		public Game Game { get; set; }
		public String GameTitle { get; set; }
		public User Customer { get; set; }
		public Decimal PurchasePrice { get; set; }
		public int Quantity { get; set; }
		public DateTime PurchaseDate { get; set; }

		public Order(int id, Game game, string gameTitle, User customer, Decimal purchasePrice, int quantity, DateTime purchaseDate)
		{
			ID = id;
			Game = game;
			GameTitle = gameTitle;
			Customer = customer;
			PurchasePrice = purchasePrice;
			Quantity = quantity;
			PurchaseDate = purchaseDate;
		}
		public Order()
		{
			ID = 0;
			Game = null;
			GameTitle = "";
			Customer = null;
			PurchasePrice = 0;
			Quantity = 0;
			PurchaseDate = new DateTime();
		}
		public Order(int id)
		{
			ID = id;
			Game = null;
			GameTitle = "";
			Customer = null;
			PurchasePrice = 0;
			Quantity = 0;
			PurchaseDate = new DateTime();
		}
	}
}
