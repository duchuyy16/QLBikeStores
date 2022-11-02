using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
