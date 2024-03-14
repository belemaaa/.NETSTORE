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

    public class OwnerDTO
    {
        public string? Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

