using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Interfaces;
using QLBikeStores.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QLBikeStores.Controllers
{
    public class AccountController : Controller
    {
        private readonly demoContext _context;
        public AccountController(demoContext context)
        {
            _context = context;
        }

        //private readonly demoContext _context;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<AppUser> _signInManager;
        //private readonly ISendGridEmail _sendGridEmail;

        //public AccountController(demoContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        //   ISendGridEmail sendGridEmail, RoleManager<IdentityRole> roleManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _sendGridEmail = sendGridEmail;
        //    _roleManager = roleManager;
        //}

        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user == null)
        //        {
        //            return RedirectToAction("ForgotPasswordConfirmation");
        //        }
        //        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

        //        await _sendGridEmail.SendEmailAsync(model.Email, "Reset Email Confirmation", "Please reset email by going to this " +
        //            "<a href=\"" + callbackurl + "\">link</a>");
        //        return RedirectToAction("ForgotPasswordConfirmation");
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public IActionResult ResetPassword(string code = null)
        //{
        //    return code == null ? View("Error") : View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("Email", "User not found");
        //            return View();
        //        }
        //        var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("ResetPasswordConfirmation");
        //        }
        //    }
        //    return View(resetPasswordViewModel);
        //}

        //[HttpGet]
        //public IActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }

        public static string EncryptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }    
            else
            {
                byte[] storePassword=ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword=Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }    
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var data = new Customer()
                {                 
                    FirstName=model.Firstname,
                    LastName=model.Lastname,
                    Username = model.Username,
                    Email = model.Email,
                    Password =EncryptPassword(model.Password),
                    Phone = model.Phone,
                    //Active =Convert.ToByte(model.IsActive)
                };
                _context.Customers.Add(data);
                _context.SaveChanges();
                TempData["successMessage"] = "You are eligible to login, please fill own credential's then login!";
                return RedirectToAction("Login");

            }
            else
            {
                TempData["errorMessage"] = "Empty form can't be submitted";
                return View(model);
            }
        }

        [AcceptVerbs("Post","Get")] 
        public IActionResult UserNameIsExist(string userName)
        {
            var data = _context.Customers.Where(e => e.Username == userName).SingleOrDefault();
            if(data!=null)
            {
                return Json($"Username {userName} already in use!");
            }   
            else
            {
                return Json(true);
            }    
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Customers.Where(s => s.Username == model.Username).FirstOrDefault();
                if (data != null)
                {
                    bool isValid = (data.Username == model.Username && DecryptPassword(data.Password) == model.Password);
                    if (isValid==true)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid password!";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Username not found!";
                    return View(model);
                }
            }
            else
            {
                TempData["errorMessage"] = "Invalid!";
                return View(model);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Index", "Home");
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

        public IActionResult FacebookLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FacebookLogin(string hoten)
        {

            return View();
        }
    }
}
