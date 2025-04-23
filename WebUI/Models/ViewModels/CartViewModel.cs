using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models.ViewModels
{
	public class CartViewModel : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
