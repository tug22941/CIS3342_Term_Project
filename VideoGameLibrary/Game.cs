using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameLibrary
{
	public class Game
	{
		public int ID { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public String ImageUrl { get; set; }
		public String Type { get; set; }
		public String Platform { get; set; }
		public Decimal RetailPrice { get; set; }
		public Decimal DiscountedPrice { get; set; }
		public Decimal CurrentDiscount { get; set; }
		public User Producer { get; set; }
		public DateTime ReleaseDate { get; set; }		
		public bool ListedForSale { get; set; }

		public Game(int id, String title, String description, String imageUrl, String type, String platform, Decimal retailPrice, Decimal discountedPrice, Decimal currentDiscount, User producer, DateTime releaseDate, bool listedForSale)
		{
			ID = id;
			Title = title;
			Description = description;
			ImageUrl = imageUrl;
			Type = type;
			Platform = platform;
			RetailPrice = retailPrice;
			DiscountedPrice = discountedPrice;
			CurrentDiscount = currentDiscount;
			Producer = producer;
			ReleaseDate = releaseDate;			
			ListedForSale = listedForSale;
		}

		public Game(int id)
		{
			ID = id;
			Title = "";
			Description = "";
			ImageUrl = "";
			Type = "";
			Platform = "";
			RetailPrice = 0;
			DiscountedPrice = 0;
			CurrentDiscount = 0;
			Producer = null;
			ReleaseDate = new DateTime();			
			ListedForSale = false;
		}
		public Game(int id, String gameTitle)
		{
			ID = id;
			Title = gameTitle;
			Description = "";
			ImageUrl = "";
			Type = "";
			Platform = "";
			RetailPrice = 0;
			DiscountedPrice = 0;
			CurrentDiscount = 0;
			Producer = null;
			ReleaseDate = new DateTime();
			ListedForSale = false;
		}

		public Game()
		{
			ID = 0;
			Title = "";
			Description = "";
			ImageUrl = "";
			Type = "";
			Platform = "";
			RetailPrice = 0;
			DiscountedPrice = 0;
			CurrentDiscount = 0;
			Producer = null;
			ReleaseDate = new DateTime();			
			ListedForSale = false;
		}

	}
}
