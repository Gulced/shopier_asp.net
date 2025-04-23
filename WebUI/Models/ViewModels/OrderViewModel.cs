using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models.ViewModels
{
	public class OrderViewModel : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
