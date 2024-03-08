﻿using System;
using _netstore.Interfaces;
using _netstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace _netstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
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


    }
}

