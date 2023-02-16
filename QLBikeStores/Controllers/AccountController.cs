using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
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
                    Street=model.Street,
                    City=model.City,
                    ZipCode=model.ZipCode,
                    State=model.State,
                    //Active =Convert.ToByte(model.IsActive)
                };
                //_context.Customers.Add(data);
                //_context.SaveChanges();

                Utilities.SendDataRequest<Customer>(ConstantValues.Customer.ThemKhachHang, data);

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
            //var data = _context.Customers.Where(e => e.Username == userName).SingleOrDefault();
            var url = string.Format(ConstantValues.Customer.KiemTraUsername, userName);
            var data = Utilities.SendDataRequest<Customer>(url);
            if (data!=null)
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
                //var data = _context.Customers.Where(s => s.Username == model.Username).FirstOrDefault();
                var data = Utilities.SendDataRequest<Customer>(ConstantValues.Customer.DangNhap, model);
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
                        HttpContext.Session.SetInt32("ClientCustomerId", data.CustomerId);
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
            HttpContext.Session.Clear();
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
        public IActionResult FacebookLogin(string hoten,string email)
        {

            return View();
        }

        
    }
}
