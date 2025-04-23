using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public AccountController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public IActionResult Login() => View();

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var client = _httpClientFactory.CreateClient("WebApi");
			var response = await client.PostAsJsonAsync("api/auth/login", model);

			if (response.IsSuccessStatusCode)
			{
				var token = await response.Content.ReadAsStringAsync();

				// 🍪 Cookie'ye token yaz
				Response.Cookies.Append("access_token", token, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTimeOffset.UtcNow.AddHours(1)
				});

				// 🔍 Token'dan rol çözümle
				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken(token);
				var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

				if (role == "Admin")
					return RedirectToAction("ProductList", "Admin");

				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Giriş başarısız.");
			return View(model);
		}

		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var client = _httpClientFactory.CreateClient("WebApi");
			var response = await client.PostAsJsonAsync("api/auth/register", model);

			if (response.IsSuccessStatusCode)
				return RedirectToAction("Login");

			ModelState.AddModelError("", "Kayıt başarısız.");
			return View(model);
		}

		[HttpPost]
		public IActionResult Logout()
		{
			Response.Cookies.Delete("access_token");
			return RedirectToAction("Login");
		}
	}
}
