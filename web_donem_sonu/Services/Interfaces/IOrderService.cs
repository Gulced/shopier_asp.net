using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Interfaces
{
	public class IOrderService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
