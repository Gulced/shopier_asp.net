using Microsoft.AspNetCore.Mvc;

namespace WebApi.Data.Repositories
{
	public class ProductRepository : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
