using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using System.Threading.Tasks;

namespace QLBikeStores.Controllers
{
    public class LoginController : Controller
    {
        public readonly demoContext _context;
        public LoginController(demoContext context)
        {
            _context = context;
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LogOff()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation(4, "User logged out.");
        //    return RedirectToAction(nameof(HomeController.Index), "Home");
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new User { UserName = model.Email, Email = model.Email, Name = model.FullName };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            _logger.LogInformation(3, "User created a new account with password.");
        //            return RedirectToAction(nameof(HomeController.Index), "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    return View(model);
        //}
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Login([FromRoute] string returnUrl = "")
        //{
        //    var model = new LoginViewModel { ReturnUrl = returnUrl };
        //    return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model, [FromRoute] string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            //update LatestLogin
        //            var user = await _userManager.FindByEmailAsync(model.Email);
        //            user.LatestLogin = DateTime.Now;
        //            await _userManager.UpdateAsync(user);

        //            _logger.LogInformation(1, "User logged in.");
        //            return RedirectToLocal(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning(2, "User account locked out.");
        //            return View("Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return View(model);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
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
