using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
