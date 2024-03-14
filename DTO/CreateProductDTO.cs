using System;
using System.ComponentModel.DataAnnotations;
using _netstore.Data;
using _netstore.Models;

namespace _netstore.DTO
{
    public class CreateProductDTO
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Price { get; set; }

      
        public IFormFile? Image { get; set; }

        [Required]
        [ValidProductType]
        public string? ProductType { get; set; }

        public int QuantityAvailable { get; set; }
    }
}
