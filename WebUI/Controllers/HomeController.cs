using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	[Authorize(Roles = "User")] //sadece giriþ yapan kullanýcý eriþebilir
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Profile()
		{
			return View();
		}
	}
}
