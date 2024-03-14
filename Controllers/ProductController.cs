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
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            if (!products.isSuccessful)
            {
                return StatusCode(products.status, products.message);
            }

            return Ok(new ApiResponse()
            {
                Data = products.Item1,
                Message = products.message,
                StatusCode = products.status,
            });
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var product = await _productRepository.GetProduct(productId);
            if (!product.isSuccessful)
            {
                return StatusCode(product.status, product.message);
            }
            return Ok(new ApiResponse()
            {
                Data = product.Item1,
                Message = product.message,
                StatusCode = 200,
            });
        }

        //[Authorize(Roles = "Admin")] this allows you restrict this endpoint for just admin users
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var newProduct = await _productRepository.AddProduct(product);
            if (!newProduct.isSuccessful)
            {
                return StatusCode(newProduct.status, newProduct.message);
            }

            return Ok(new ApiResponse()
            {
                Message = newProduct.message,
                StatusCode = newProduct.status
            });
        }

    }
}

