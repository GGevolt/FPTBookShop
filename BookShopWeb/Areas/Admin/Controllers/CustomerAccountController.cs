using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.Admin.Controllers
{
	public class CustomerAccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
