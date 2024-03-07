using System;
using _netstore.Models;

namespace _netstore.Data
{
    public class Seed
    {
        private readonly ApplicationDbContext dbContext;
        public Seed(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        public void SeedDataContext()
        {
            if (!dbContext.Owners.Any())
            {
                var owners = new List<Owner>()
                {
                    new Owner
                    {
                        Name = "Grace",
                        Email = "grace@gmail.com",
                        PhoneNumber = "09099989766",
                        CreatedAt = DateTime.Now,
                        Products = new List<Product>()
                        {
                            new Product
                            {
                                Name = "Nike shorts",
                                Description = "These are the best Nike shorts available",
                                Price = "15,000",
                                Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1707570362/mbxl3jx8js0dzz3h9r4t.jpg",
                                Type = "Clothing",
                                QuantityAvailable = 14,
                                CreatedAt = DateTime.Now,
                            },

                            new Product
                            {
                                Name = "Samsung phone",
                                Description = "Mobile samsung phone",
                                Price = "100,000",
                                Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1694168777/cld-sample-2.jpg",
                                Type = "Electronics",
                                QuantityAvailable = 30,
                                CreatedAt = DateTime.Now,
                            }
                        }
                        
                    },

                    new Owner
                    {
                        Name = "James",
                        Email = "james@gmail.com",
                        PhoneNumber = "09876667887",
                        CreatedAt = DateTime.Now,
                        Products = new List<Product>()
                        {
                            new Product
                            {
                                Name = "Bikins",
                                Description = "Bikinis for both men and women",
                                Price = "15,000",
                                Image = "https://res.cloudinary.com/drrnvvy3v/image/upload/v1707570362/mbxl3jx8js0dzz3h9r4t.jpg",
                                Type = "Clothing",
                                QuantityAvailable = 23,
                                CreatedAt = DateTime.Now,
                            },
                        }
                    }
                };
                dbContext.Owners.AddRange(owners);
                dbContext.SaveChanges();
            }
        }
    }
}

