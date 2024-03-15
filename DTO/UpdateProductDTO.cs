using System;
using System.ComponentModel.DataAnnotations;

namespace _netstore.DTO
{
	public class UpdateProductDTO
	{
        public string? OwnerId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public IFormFile? Image { get; set; }

        [ValidProductType]
        public string? ProductType { get; set; }

        public int QuantityAvailable { get; set; }
    }
}

