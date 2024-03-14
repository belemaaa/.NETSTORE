using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Interfaces;
using _netstore.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._context = context;
            this._userManager = userManager;
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductDTO>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productDto = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    ProductType = product.ProductType,
                    QuantityAvailable = product.QuantityAvailable,
                    Owner = _mapper.Map<OwnerDTO>(product.Owner)
                };
                productDTOs.Add(productDto);
            }
            return Ok(productDTOs);
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
            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                ProductType = product.ProductType,
                QuantityAvailable = product.QuantityAvailable,
                Owner = _mapper.Map<OwnerDTO>(product.Owner)
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(productDto);
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

