﻿using System;
using _netstore.Data;
using _netstore.Interfaces;
using _netstore.Models;
using Microsoft.EntityFrameworkCore;

namespace _netstore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool AddProduct(Product product)
        {
            this._context.Add(product);
            return true;
        }

        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}

