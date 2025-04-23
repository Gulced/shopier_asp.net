using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Interfaces
{
	public class IProductService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
