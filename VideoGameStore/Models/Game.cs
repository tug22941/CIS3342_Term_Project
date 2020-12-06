using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameStore.Models
{
	public class Game
	{
		public int ID { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public String ImageUrl { get; set; }
		public String Type { get; set; }
		public Decimal WholesalePrice { get; set; }
		public Decimal RetailPrice { get; set; }
		public Decimal CurrentDiscount { get; set; }
		public User Producer { get; set; }
		public DateTime ReleaseDate { get; set; }
		public bool StoreOwnsLicense { get; set; }
		public bool ListedForSale { get; set; }

		public Game(int id, String title, String description, String imageUrl, String type, Decimal wholesalePrice, Decimal retailPrice, Decimal currentDiscount, User producer, DateTime releaseDate, bool storeOwnsLicense, bool listedForSale)
		{
			ID = id;
			Title = title;
			Description = description;
			ImageUrl = imageUrl;
			Type = type;
			WholesalePrice = wholesalePrice;
			RetailPrice = retailPrice;
			CurrentDiscount = currentDiscount;
			Producer = producer;
			ReleaseDate = releaseDate;
			StoreOwnsLicense = storeOwnsLicense;
			ListedForSale = listedForSale;
		}

		public Game(int id)
		{
			ID = id;
			Title = "";
			Description = "";
			ImageUrl = "";
			Type = "";
			WholesalePrice = 0;
			RetailPrice = 0;
			CurrentDiscount = 0;
			Producer = null;
			ReleaseDate = new DateTime();
			StoreOwnsLicense = false;
			ListedForSale = false;
		}

	}
}
