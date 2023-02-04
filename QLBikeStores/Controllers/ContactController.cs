using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Submit(string name, string email, string message)
        {
            // Xử lý dữ liệu form và lưu vào database hoặc gửi email
            return View("Thanks");
        }
    }
}
