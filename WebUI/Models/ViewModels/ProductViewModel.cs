using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models.ViewModels
{
	public class ProductViewModel : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
