using System;
using _netstore.Models;

namespace _netstore.Interfaces
{
	public interface IProductRepository
	{
		Task<ICollection<Product>> GetProducts();

		Task<Product> GetProduct(int productId);

		bool AddProduct(Product product);

		bool UpdateProduct(int productId, Product product);

		bool DeleteProduct(int productId);

		bool ProductExists(int productId);

		bool Save();
	}
}

