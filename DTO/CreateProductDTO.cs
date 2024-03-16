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

        [Required]
        public IFormFile? Image { get; set; }

        [ValidProductType]
        public string? ProductType { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public int QuantityAvailable { get; set; }

        public string? OwnerId { get; set; }
    }
}
