using System;
using _netstore.Data;
using _netstore.Models;

namespace _netstore.DTO
{
	public class CreateProductDTO
	{
		public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? Image { get; set; }

        public ProductType ProductType { get; set; }

        public int QuantityAvailable { get; set; }
    }
}

