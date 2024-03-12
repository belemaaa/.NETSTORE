using System;
using System.Net;
using _netstore.Models;
using Microsoft.AspNetCore.Identity;

namespace _netstore.Data
{
    public class Seed
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> _userManager;

        public Seed(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.dbContext = context;
            this._userManager = userManager;
        }
        public void SeedDataContext()
        { 
            if (!dbContext.Products.Any())
            {
                var products = new List<Product>()
                {    
                    new Product
                    {
                        Name = "Nike shorts",
                        Description = "These are the best Nike shorts available",
                        Price = "15,000",
                        Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1707570362/mbxl3jx8js0dzz3h9r4t.jpg",
                        ProductType = ProductType.Shorts,
                        QuantityAvailable = 14,
                        CreatedAt = DateTime.Now,
                    },

                    new Product
                    {
                        Name = "Samsung phone",
                        Description = "Mobile samsung phone",
                        Price = "100,000",
                        Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1694168777/cld-sample-2.jpg",
                        ProductType = ProductType.Electronics,
                        QuantityAvailable = 30,
                        CreatedAt = DateTime.Now,
                    },

                    new Product
                    {
                        Name = "Bikins",
                        Description = "Bikinis for both men and women",
                        Price = "15,000",
                        Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1707570362/mbxl3jx8js0dzz3h9r4t.jpg",
                        ProductType = ProductType.Womens,
                        QuantityAvailable = 23,
                        CreatedAt = DateTime.Now,
                    },

                };
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }

        public static async Task SeedUsersAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "graceitamunoala@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "grace",
                        Email = adminUserEmail,
                    };
                    await userManager.CreateAsync(newAdminUser, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }

                string appUserEmail = "bob@gmail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                    };
                    await userManager.CreateAsync(newAppUser, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(newAppUser, "Member");
                }
            }
        }
    }
}

