using System;
using _netstore.Data;
using _netstore.Interfaces;
using _netstore.Models;

namespace _netstore.Repositories
{
	public class ProductRepository : IProductRepository
	{
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
		{
			this._context = context;
		}

        public bool AddProduct(int ownerId, Product product)
        {
            var owner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                Type = product.Type,
                QuantityAvailable = product.QuantityAvailable,
                CreatedAt = DateTime.UtcNow,
                Owner = owner
            };
            _context.Add(product);
            return true;
        }

        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}

