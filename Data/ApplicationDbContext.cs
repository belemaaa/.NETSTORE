using System;
using _netstore.Models;
using Microsoft.EntityFrameworkCore;

namespace _netstore.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }

		public DbSet<Owner> Owners { get; set; }
	}
}

