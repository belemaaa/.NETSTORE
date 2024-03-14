using System;
namespace _netstore.DTO
{
	public class ApiResponse
	{
		
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
	}
}

