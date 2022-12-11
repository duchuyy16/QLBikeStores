﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QLBikeStores.Helpers;
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
        public IActionResult DanhSachSanPham()
        {
            //var result = Utilities.SendDataRequest("api/Product/DocDanhSachSanPham");
            //var dsTheLoai = JsonConvert.DeserializeObject<List<ProductModel>>(result.ToString());
            var dsTheLoai = Utilities.SendDataRequest<List<ProductModel>>("api/Product/DocDanhSachSanPham");
            return View(dsTheLoai); 
        }
        public IActionResult DanhSachSanPhamAjax()
        {
            return View();
        }
        public List<ProductModel> DocDanhSachSanPhamAjax()
        {
            var result = Utilities.SendDataRequest("api/Product/DocDanhSachSanPham");
            var dsTheLoai = JsonConvert.DeserializeObject<List<ProductModel>>(result.ToString());
            return dsTheLoai;
        }
        public IActionResult DSSanPhamBanChay()
        {
            return View();
        }
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
    }
}
