using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Implementations
{
	public class ProductService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
