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
            }
            catch (Exception ex)
            {
                msg = "An error occurred, error: " + ex;
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

                if (product != null)
                {
                    msg = "Success";
                    response = true;
                    status = 200;
                }
                else
                {
                    response = false;
                    msg = "Product was not found";
                    status = 404;
                }
            }
            catch(Exception ex)
            {
                msg = "An error occurred";
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
                msg = "An error occured";
                status = 400;
            }
            return (products, status, response, msg);
        }

        public async Task<(ProductDTO product, int status, bool isSuccessful, string message)> UpdateProduct(int productId, UpdateProductDTO model)
        {
            var product = new Product();
            int status = 0;
            bool response = false;
            string? msg = null;

            try
            {
                var fetchedProduct = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
                if (fetchedProduct != null)
                {
                    var productOwner = fetchedProduct.Owner.Id;

                    if (productOwner == model.OwnerId)
                    {
                        string? uploadedImage = null;

                        if (model.Image.Length > 0)
                        {
                            await _imageService.DeletePhotoAsync(fetchedProduct.Image);
                            var result = await _imageService.AddPhotoAsync(model.Image);
                            uploadedImage = result.Url.ToString();
                        }
                        else
                        {
                            uploadedImage = fetchedProduct.Image;
                        }

                        product = new Product
                        {
                            Id = productId,
                            Name = model.Name,
                            Description = model.Description,
                            Price = model.Price,
                            Image = uploadedImage,
                            ProductType = model.ProductType,
                            QuantityAvailable = model.QuantityAvailable,
                            Owner = fetchedProduct.Owner
                        };
                        _context.Update(product);
                    }
                    else
                    {
                        response = false;
                        msg = "Unauthorized";
                        status = 401;
                    }

                    response = true;
                    msg = "Success";
                    status = 200;
                } 
            }
            catch(Exception ex)
            {
                msg = "An error occurred, error: " + ex;
                status = 400;
            }

        }

        public Task<(int status, bool isSuccessful, string message)> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}

