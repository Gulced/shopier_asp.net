using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services.Interfaces
{
	public class IAuthService : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
