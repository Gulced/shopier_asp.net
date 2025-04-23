using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Models.DTOs;
using WebApi.Models.Entities;
using BCrypt.Net;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _config;

		public AuthController(ApplicationDbContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		[HttpPost("register")]
		public IActionResult Register(RegisterDTO dto)
		{
			if(_context.Users.Any(x => x.Email == dto.Email))
			{
				return BadRequest("Bu email zaten kayıtlı.");
			}

			var roleName = string.IsNullOrEmpty(dto.RoleName) ? "User" : dto.RoleName;

			var role = _context.Roles.FirstOrDefault(r => r.RoleName == roleName);
			if (role == null)
				return StatusCode(500, "Seçilen rol veritabanında bulunamadı.");

			var user = new User
			{
				FullName = dto.FullName,
				Email = dto.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				RoleId = role.RoleId
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			return Ok("Kayıt başarılı.");
		}

		[HttpPost("login")]
		public IActionResult Login(LoginDTO dto)
		{
			var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
				return Unauthorized("Hatalı giriş.");

			var token = GenerateJwtToken(user);
			return Ok(token);
		}

		private string GenerateJwtToken(User user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
			new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim("FullName", user.FullName),
			new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
		};

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
