using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Implementations
{
	public class OrderService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
