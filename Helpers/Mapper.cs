using System;
using _netstore.DTO;
using _netstore.Models;
using AutoMapper;

namespace _netstore.Helpers
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<UserDTO, User>();
			CreateMap<ProductDTO, Product>();
			CreateMap<OwnerDTO, User>();
			CreateMap<UpdateProductDTO, Product>();
		}
	}
}

