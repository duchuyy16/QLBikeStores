using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace QLBikeStores.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                var url = string.Format(ConstantValues.Product.TimKiem, name);
                var products = Utilities.SendDataRequest<List<Product>>(url);
                return View(products);
            }
            else
            {
                return NotFound();
            }
        }
        public ActionResult ListProduct(int? pageNo = 1)
        {
            try
            {
                var products = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);
                var pagedList = products.ToPagedList((int)pageNo, 9);
                return View(pagedList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public ActionResult ListCategory(int categoryId)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.DocDanhSachSanPhamTheoTheLoai, categoryId);
                var products = Utilities.SendDataRequest<List<Product>>(url);
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public ActionResult ListCategoryBrand(int brandId, int categoryId)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.DanhSachSanPhamTheoTheLoaiThuongHieu, categoryId, brandId);
                var products = Utilities.SendDataRequest<List<Product>>(url);

                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        public ActionResult ProductDetail(int id)
        {
            var url = string.Format(ConstantValues.Product.ChiTietSanPham, id);
            var item = Utilities.SendDataRequest<Product>(url);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("ListProduct");
        }
    }
}
