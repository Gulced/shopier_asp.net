using Microsoft.AspNetCore.Mvc;

namespace WebApi.Data.Repositories
{
	public class Repository : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
