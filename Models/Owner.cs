using System;
namespace _netstore.Models
{
	public class Owner
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }

		public DateTime CreatedAt { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}

