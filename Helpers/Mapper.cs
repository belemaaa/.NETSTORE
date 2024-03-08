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
			CreateMap<User, UserDto>();
		}
	}
}

