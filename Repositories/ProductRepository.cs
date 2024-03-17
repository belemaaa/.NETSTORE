using System;
using _netstore.Data;
using _netstore.DTO;
using _netstore.Interfaces;
using _netstore.Models;
using _netstore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _netstore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ProductRepository(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper, ImageService imageService)
        {
            this._context = context;
            this._userManager = userManager;
            this._mapper = mapper;
            this._imageService = imageService;
        }

        public async Task<(int status, bool isSuccessful, string message)> AddProduct(CreateProductDTO model)
        {
            string msg = string.Empty;
            var response = false;
            int status = 0;

            try
            {
                var owner = _context.Users.Where(u => u.Id == model.OwnerId).FirstOrDefault();
                if (owner != null)
                {
                    var imageResult = await _imageService.AddPhotoAsync(model.Image);
                    var product = new Product
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Image = imageResult.Url.ToString(),
                        ProductType = model.ProductType,
                        QuantityAvailable = model.QuantityAvailable,
                        Owner = owner,
                        CreatedAt = DateTime.Now
                    };
                    _context.Add(product);
                    _context.SaveChanges();

                    response = true;
                    msg = "Success";
                    status = 201;
                }
                else
                {
                    response = false;
                    status = 401;
                    msg = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                msg = "An error occurred, error: " + ex.Message;
                status = 400;
            }

            return (status, response, msg);
           
        }

        public async Task<(ProductDTO, int status, bool isSuccessful, string message)> GetProduct(int productId)
        {
            var product = new ProductDTO();
            string msg = string.Empty;
            var response = false;
            int status = 0;

            try
            {
                product = _context.Products.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    ProductType = product.ProductType,
                    QuantityAvailable = product.QuantityAvailable,
                    Owner = new OwnerDTO
                    {
                        Id = product.Owner.Id,
                        Username = product.Owner.UserName,
                        Email = product.Owner.Email,
                        PhoneNumber = product.Owner.PhoneNumber
                    }
                }).Where(p => p.Id == productId).FirstOrDefault();

                if (product == null)
                { 
                    response = false;
                    msg = "Product was not found";
                    status = 404;
                }
                else
                {
                    response = true;
                    msg = "Success";
                    status = 200;
                }
            }
            catch(Exception ex)
            {
                msg = "An error occurred, error: " + ex.Message;
                status = 400;
            }
            return (product, status, response, msg);
        }


        public async Task<(List<ProductDTO>, int status, bool isSuccessful, string message)> GetProducts()
        {
            var products = new List<ProductDTO>();
            string msg = string.Empty;
            var response = false;
            int status = 0;
           
            try
            {
                products = _context.Products.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    ProductType = product.ProductType,
                    QuantityAvailable = product.QuantityAvailable,
                    Owner = new OwnerDTO
                    {
                        Id = product.Owner.Id,
                        Username = product.Owner.UserName,
                        Email = product.Owner.Email,
                        PhoneNumber = product.Owner.PhoneNumber
                    }
                }).ToList();

                msg = "Success";
                response = true;
                status = 200;
            }
            catch (Exception ex)
            {
                msg = "An error occured, error: " + ex.Message;
                status = 400;
            }
            return (products, status, response, msg);
        }

        public async Task<(int status, bool isSuccessful, string message)> UpdateProduct(int productId, UpdateProductDTO model)
        {
            bool response = false;
            string? msg;
            int status;
            try
            {
                var product = await GetProduct(productId);
                var fetchedProduct = _mapper.Map<Product>(product.Item1);
                if (fetchedProduct != null && fetchedProduct.Owner.Id == model.OwnerId)
                {
                    string? uploadedImage = null;
                    if (model.Image != null && model.Image.Length > 0)
                    {
                        try
                        {
                            if (fetchedProduct.Image != null)
                            {
                                await _imageService.DeletePhotoAsync(fetchedProduct.Image);
                            }
                            var result = await _imageService.AddPhotoAsync(model.Image);
                            uploadedImage = result.Url.ToString(); 
                        }
                        catch (Exception ex)
                        {
                            msg = "Could not delete image, error: " + ex;
                            status = 400;
                        }
                    }
                    else
                    {
                        uploadedImage = fetchedProduct.Image;
                    }

                    _mapper.Map(model, fetchedProduct);
                    _context.Entry(fetchedProduct).State = EntityState.Modified;
                    try
                    {
                        // Attempt to save changes
                        await _context.SaveChangesAsync();
                        response = true;
                        msg = "Success";
                        status = 200;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        msg = "Concurrency conflict occurred. The data may have been modified or deleted by another user.";
                        status = 409;
                    }           
                }
                else
                {
                    response = false;
                    msg = "Product was not found or this action is unauthorized";
                    status = 404;
                }
            }
        

            catch (Exception ex)
            {
                msg = "An error occurred, error: " + ex;
                status = 400;
            }

            return (status, response, msg);
        }


        public async Task<(int status, bool isSuccessful, string message)> DeleteProduct(int productId, string OwnerId)
        {
            bool response = false;
            string? msg;
            int status;
            try
            {
                var product = await GetProduct(productId);
                var fetchedProduct = _mapper.Map<Product>(product.Item1);

                if (fetchedProduct != null)
                {
                    var productOwner = fetchedProduct.Owner.Id;
                    if (productOwner == OwnerId)
                    {
                        _context.Products.Remove(fetchedProduct);
                        response = true;
                        msg = "Success";
                        status = 200;
                    }
                    else
                    {
                        msg = "Forbidden";
                        status = 403;
                    }
                }
                else
                {
                    response = false;
                    msg = "Product was not found or this action is unauthorized";
                    status = 404;
                }
            }
            catch(Exception ex)
            {
                msg = "An error occurred, error: " + ex;
                status = 400;
            }
            return (status, response, msg);
        }
    }
}

