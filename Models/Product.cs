using System;
using _netstore.Data;

namespace _netstore.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? Price { get; set; }

		public string? Image { get; set; }

		public string? ProductType { get; set; }

		public int QuantityAvailable { get; set; }

		public User? Owner { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}

