using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string hoten)
        {

            return View();
        }
    }
}
