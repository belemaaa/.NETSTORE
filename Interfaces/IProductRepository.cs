using System;
using _netstore.Models;

namespace _netstore.Interfaces
{
	public interface IProductRepository
	{
		ICollection<Product> GetProducts();

		Product GetProduct(int productId);

		bool AddProduct(int ownerId, Product product);

		bool UpdateProduct(int productId, Product product);

		bool DeleteProduct(int productId);

		bool ProductExists(int productId);

		bool Save();
	}
}

