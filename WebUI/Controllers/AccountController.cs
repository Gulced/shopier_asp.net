using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var client = _httpClientFactory.CreateClient("WebApi");
			var dto = new
			{
				Email = model.Email,
				Password = model.Password
			};

			var response = await client.PostAsJsonAsync("api/auth/login", dto);

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

				// 🔐 Token'dan Claims çıkar
				var handler = new JwtSecurityTokenHandler();
				var jwt = handler.ReadJwtToken(token);

				var claims = jwt.Claims.ToList();
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// 🟢 ASP.NET Core'a login işlemi bildir
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				// 🎯 Role göre yönlendir
				var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

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
			var dto = new
			{
				FullName = model.FullName,
				Email = model.Email,
				Password = model.Password
			};
			var response = await client.PostAsJsonAsync("api/auth/register", dto);

			if (response.IsSuccessStatusCode)
				return RedirectToAction("Login");

			ModelState.AddModelError("", "Kayıt başarısız.");
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			// 🔐 ASP.NET Core logout işlemi
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			Response.Cookies.Delete("access_token");

			return RedirectToAction("Login");
		}
	}
}
