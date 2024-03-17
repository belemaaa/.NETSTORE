using System;
using System.ComponentModel.DataAnnotations;

namespace _netstore.DTO
{
	public class UpdateProductDTO
	{
        [Required]
        public string? OwnerId { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public IFormFile? Image { get; set; }

        public int QuantityAvailable { get; set; }
    }
}

