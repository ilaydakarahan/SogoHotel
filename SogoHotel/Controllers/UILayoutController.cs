using Microsoft.AspNetCore.Mvc;

namespace SogoHotel.Controllers
{
	public class UILayoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
