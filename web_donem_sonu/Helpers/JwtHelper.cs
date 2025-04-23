using Microsoft.AspNetCore.Mvc;

namespace WebApi.Helpers
{
	public class JwtHelper : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
