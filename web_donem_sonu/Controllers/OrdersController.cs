using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
