using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Interfaces;
using _netstore.Models;
using _netstore.Services;
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
        private readonly ImageService _imageService;

        public ProductController(IProductRepository productRepository, ApplicationDbContext context, UserManager<User> userManager, IMapper mapper, ImageService imageService)
        {
            this._productRepository = productRepository;
            this._context = context;
            this._userManager = userManager;
            this._mapper = mapper;
            this._imageService = imageService;
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
            return StatusCode(200, productDTOs);
        }


        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type=typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            bool productExists = _productRepository.ProductExists(productId);
            if (!productExists)
            {
                return StatusCode(404, new { message = "Product does not exist" });
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
                return StatusCode(400, ModelState);
            }
            return StatusCode(200, productDto);
        }

        //[Authorize(Roles = "Admin")] this allows you restrict this endpoint for just admin users
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }
            var owner = await _userManager.FindByNameAsync(User.Identity.Name);
            if (owner == null)
            {
                return StatusCode(401);
            }

            var imageResult = await _imageService.AddPhotoAsync(dto.Image);
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Image = imageResult.Url.ToString(),
                ProductType = dto.ProductType,
                QuantityAvailable = dto.QuantityAvailable,
                Owner = owner,
                CreatedAt = DateTime.Now
            };
            var result = _productRepository.AddProduct(product);
            if (!result)
            {
                return StatusCode(400);
            }
            return StatusCode(201, new { message = "New product has been created successfully" });
        }

    }
}

