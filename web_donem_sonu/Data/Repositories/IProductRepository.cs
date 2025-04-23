using Microsoft.AspNetCore.Mvc;

namespace WebApi.Data.Repositories
{
	public class IProductRepository : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
