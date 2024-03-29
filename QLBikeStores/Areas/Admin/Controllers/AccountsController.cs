﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
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

        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var data = _context.Staffs.Where(s => s.Username == model.Username).SingleOrDefault();
                var data = Utilities.SendDataRequest<Staff>(ConstantValues.Staff.DangNhap, model);
                if (data != null)
                {
                    bool isValid = (data.Username == model.Username && DecryptPassword(data.Password) == model.Password);
                    if (isValid == true)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.Username);
                        HttpContext.Session.SetString("UserRoles", data.Role.RoleName);
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
            return Redirect("/Admin/Accounts/Login");
        }
    }
}
