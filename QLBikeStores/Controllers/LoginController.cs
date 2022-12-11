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

        public IActionResult GoogleLogin()
        {
            ViewData["ThongBao"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult GoogleLogin(string email, string fullname)
        {
            //xu ly dang nhap vao database
            string thongbao = "Bạn đã đăng nhập với thông tin: " + email + "(" + fullname + ")";
            ViewData["ThongBao"] = thongbao;
            return View();
        }
    }
}
