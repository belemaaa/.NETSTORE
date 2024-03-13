using System;
using Microsoft.AspNetCore.Identity;

namespace _netstore.Models
{
	public class User : IdentityUser
	{
		public ICollection<Product>? Products { get; set; }
	}
}

