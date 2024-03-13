using System;
using System.ComponentModel.DataAnnotations;
using _netstore.Models;

namespace _netstore.DTO
{
	public class ProductDTO
	{
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? Image { get; set; }

        public string? ProductType { get; set; }

        public int QuantityAvailable { get; set; }

        public OwnerDTO? Owner { get; set; }
    }
}

