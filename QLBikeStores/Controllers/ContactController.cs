using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;

namespace QLBikeStores.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Thongbao"] = false;
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Contact>(ConstantValues.Contact.ThemLienLac, model);
                    ViewData["Thongbao"] = true;
                    return View("Index");
                }
                ViewData["Thongbao"] = false;
                return View("Index");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
