using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Interfaces;
using _netstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _netstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProductController(IProductRepository productRepository, ApplicationDbContext context, UserManager<User> userManager)
        {
            this._productRepository = productRepository;
            this._context = context;
            this._userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(ICollection<Product>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type=typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            bool productExists = _productRepository.ProductExists(productId);
            if (!productExists)
            {
                return NotFound("Product does not exist");
            }
            var product = await _productRepository.GetProduct(productId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var owner = await _userManager.FindByNameAsync(User.Identity.Name);
            if (owner == null)
            {
                return Unauthorized();
            }
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ProductType = dto.ProductType,
                QuantityAvailable = dto.QuantityAvailable,
                Owner = owner,
                CreatedAt = DateTime.Now
            };
            var result = _productRepository.AddProduct(product);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, new { message = "New product has been created successfully" });
        }

    }
}

