using System;
namespace _netstore.DTO
{
	public class SignupDTO : LoginDTO
	{
		public string? Username { get; set; }

		public string? PhoneNumber { get; set; }
	}
}

