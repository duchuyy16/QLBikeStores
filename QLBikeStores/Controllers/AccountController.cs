using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace QLBikeStores.Controllers
{
    public class AccountController : Controller
    {
        private readonly demoContext _context;
        public AccountController(demoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
                var data = new Staff()
                {
                    FirstName=model.Firstname,
                    LastName=model.Lastname,
                    StoreId=model.StoreId,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    Active =Convert.ToByte(model.IsActive)
                };
                _context.Staffs.Add(data);
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Staffs.Where(s => s.Username == model.Username).SingleOrDefault();
                if (data != null)
                {
                    bool isValid = (data.Username == model.Username && data.Password == model.Password);
                    if (isValid)
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
            return RedirectToAction("Login", "Account");
        }
    }
}
