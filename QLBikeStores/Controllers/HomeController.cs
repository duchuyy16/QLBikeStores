﻿using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using X.PagedList;

namespace QLBikeStores.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult DanhSachSanPhamAjax()
        //{
        //    return View();
        //}
        //public List<Product> DocDanhSachSanPhamAjax()
        //{
        //    var result = Utilities.SendDataRequest("api/Product/DocDanhSachSanPham");
        //    var dsTheLoai = JsonConvert.DeserializeObject<List<Product>>(result.ToString());
        //    return dsTheLoai;
        //}

        //public IActionResult DanhSachSanPham()
        //{
        //    //var result = Utilities.SendDataRequest("api/Product/DocDanhSachSanPham");
        //    //var dsTheLoai = JsonConvert.DeserializeObject<List<ProductModel>>(result.ToString());
        //    var dsSanPham = Utilities.SendDataRequest<List<Product>>("api/Product/DocDanhSachSanPham");
        //    return View(dsSanPham);
        //}


        public IActionResult IndexTest()
        {
            return BadRequest();
        }

        public IActionResult Index(int? pageNo = 1)
        {
            try
            {
                var products = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);
                var pagedList = products.ToPagedList((int)pageNo, 10);
                return View(pagedList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
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
