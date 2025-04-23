using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Implementations
{
	public class AuthService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
