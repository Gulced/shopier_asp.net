using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
