using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using QLBikeStores.Helpers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;

namespace QLBikeStores.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult PageBadRequest()
        {
            return View();
        }

        public IActionResult NoPermission()
        {
            return View();
        }
    }
}
