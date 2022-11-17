using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Areas.Admin
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
