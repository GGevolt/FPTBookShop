using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.Admin.Controllers
{
	public class StoreOwnerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
