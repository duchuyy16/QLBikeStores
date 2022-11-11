using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.Controllers
{
    public class HomeController : Controller
    {

        private readonly demoContext _context;

        public HomeController(demoContext context)
        {

            this._context = context;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{


        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult listProduct()
        {
            try
            {
                var products = _context.Products.ToList();
                return PartialView(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //x => x.CategoryId == categoryId &&
        public ActionResult List(int brandId)
        {
            try
            {
                var products = _context.Products.Where(x => x.BrandId == brandId).ToList();
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
