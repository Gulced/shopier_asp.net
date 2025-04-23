using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.DTOs
{
	public class RegisterDTO
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string? RoleName { get; set; } // Optional rol bilgisi

	}

}
