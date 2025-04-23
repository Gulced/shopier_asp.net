using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi.Models.DTOs;

namespace WebUI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public AdminController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Products()
		{
			var client = _httpClientFactory.CreateClient("WebApi");
			var products = await client.GetFromJsonAsync<List<ProductDTO>>("api/admin/products");

			return View(products); // Views/Admin/Products.cshtml
		}

		public async Task<IActionResult> Orders()
		{
			var client = _httpClientFactory.CreateClient("WebApi");
			var orders = await client.GetFromJsonAsync<List<OrderDTO>>("api/admin/orders");

			return View(orders); // Views/Admin/Orders.cshtml
		}

		public async Task<IActionResult> Users()
		{
			var client = _httpClientFactory.CreateClient("WebApi");
			var users = await client.GetFromJsonAsync<List<RegisterDTO>>("api/admin/users");

			return View(users); // Views/Admin/Users.cshtml
		}
	}
}
