using Microsoft.AspNetCore.Mvc;

namespace WebApi.Data.Repositories
{
	public class IRepository : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
