using System;
using _netstore.DTO;
using _netstore.Models;

namespace _netstore.Interfaces
{
	public interface IProductRepository
	{
		Task<(List<ProductDTO>, int status,  bool isSuccessful, string message)> GetProducts();

		Task<(ProductDTO, int status, bool isSuccessful, string message)> GetProduct(int productId);

		Task<(int status, bool isSuccessful, string message)> AddProduct(CreateProductDTO model);

		Task<(Product product, int status, bool isSuccessful, string message)> UpdateProduct(int productId, UpdateProductDTO model);

        Task<(int status, bool isSuccessful, string message)> DeleteProduct(int productId);
    }
}

	