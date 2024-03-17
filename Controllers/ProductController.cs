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
            var result = await _productRepository.GetProducts();
            if (!result.isSuccessful)
            {
                return StatusCode(result.status, result.message);
            }

            return Ok(new ApiResponse()
            {
                Data = result.Item1,
                Message = result.message,
                StatusCode = result.status,
            });
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var result = await _productRepository.GetProduct(productId);
            if (!result.isSuccessful)
            {
                return StatusCode(result.status, result.message);
            }
            return Ok(new ApiResponse()
            {
                Data = result.Item1,
                Message = result.message,
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

            var result = await _productRepository.AddProduct(product);
            if (!result.isSuccessful)
            {
                return StatusCode(result.status, result.message);
            }

            return StatusCode(201, new ApiResponse()
            {
                Message = result.message,
                StatusCode = result.status
            });
        }

        [Authorize]
        [HttpPatch("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var result = await _productRepository.UpdateProduct(productId, product);
            if (!result.isSuccessful)
            {
                return StatusCode(result.status, result.message);
            }
            return StatusCode(200, new ApiResponse()
            {
                Message = result.message,
                StatusCode = result.status
            });
        }

        [Authorize]
        [HttpDelete("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteProduct(int productId, string OwnerId)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            var result = await _productRepository.DeleteProduct(productId, OwnerId);
            if (!result.isSuccessful)
            {
                return StatusCode(result.status, result.message);
            }
            return StatusCode(200, new ApiResponse()
            {
                Message = result.message,
                StatusCode = result.status
            });

        }
    }
}

