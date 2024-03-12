using System;
using _netstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _netstore.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder.Entity<IdentityRole>()
				.HasData(
					new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
					new IdentityRole { Name = "Admin", NormalizedName = "ADMIN"}
				);
        }
    }
}

