using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using X.PagedList;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly demoContext _context;

        public ProductsController(demoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Search(string name, decimal? to, decimal? from)
        {
            var products = from product in _context.Products.Include(p => p.Brand).Include(p => p.Category) select product;
            //var products = Utilities.SendDataRequest<Product>(ConstantValues.Product.DanhSachSanPham);
            //var pagedList =  products.ToPagedList((int)pageNo, 10);
            if (!string.IsNullOrEmpty(name))//neu ma khong trong
            {
                if (to != null && from != null)
                {
                    products = products.Where(x => x.ProductName.Contains(name) && x.ListPrice >= to && x.ListPrice <= from).OrderByDescending(l => l.ListPrice);
                }
                else
                {
                    products = products.Where(x => x.ProductName.Contains(name));
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    products = products.Where(x => x.ProductName.Contains(name) && x.ListPrice >= to && x.ListPrice <= from);
                }
            }

            return View(products);
        }
        
        // GET: Admin/Products
        public async Task<IActionResult> Index(int? categoryId, int? pageNo = 1)
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai);
                categories.Insert(0, new Category { CategoryId = 0, CategoryName = "----------Select Category----------" });
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", categoryId);
                if (categoryId == null)
                {
                    //var demoContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
                    //var pagedList = await demoContext.ToPagedListAsync((int)pageNo, 10);
                    var products = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);
                    var pagedList = await products.ToPagedListAsync((int)pageNo, 10);
                    return View(pagedList);
                }
                else
                {
                    //var demoContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
                    //var pagedList = await demoContext.Where(x => x.CategoryId == categoryId).ToPagedListAsync((int)pageNo, 10);
                    var products = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);
                    var pagedList = await products.ToPagedListAsync((int)pageNo, 10);
                    return View(pagedList);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Product.ChiTietSanPham, id);
                var product = Utilities.SendDataRequest<Product>(url);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["BrandId"] = new SelectList(Utilities.SendDataRequest<List<Brand>>(ConstantValues.Brand.DanhSachNhanHieu), "BrandId", "BrandName");
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai), "CategoryId", "CategoryName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,ProductName,BrandId,CategoryId,ModelYear,ListPrice,Discount")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Product>(ConstantValues.Product.ThemSanPham, product);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["BrandId"] = new SelectList(Utilities.SendDataRequest<List<Brand>>(ConstantValues.Brand.DanhSachNhanHieu), "BrandId", "BrandName", product.BrandId);
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai), product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: Admin/Products/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Product.Find, id);
                var product = Utilities.SendDataRequest<Product>(url);
                if (product == null)
                {
                    return NotFound();
                }

                ViewData["BrandId"] = new SelectList(Utilities.SendDataRequest<List<Brand>>(ConstantValues.Brand.DanhSachNhanHieu), "BrandId", "BrandName", product.BrandId);
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,BrandId,CategoryId,ModelYear,ListPrice,Discount")] Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Product.CapNhatSanPham, product);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["BrandId"] = new SelectList(Utilities.SendDataRequest<List<Brand>>(ConstantValues.Brand.DanhSachNhanHieu), "BrandId", "BrandName", product.BrandId);
                ViewData["CategoryId"] = new SelectList(Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Admin/Products/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Product.ChiTietSanPham, id);
                var product = Utilities.SendDataRequest<Product>(url);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Product.Find, id);
                var product = Utilities.SendDataRequest<Product>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.Product.XoaSanPham, product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool ProductExists(int id)
        {
            var url = string.Format(ConstantValues.Product.ProductExists, id);
            var product = Utilities.SendDataRequest<bool>(url);
            if (product != true) return false;
            else return true;
        }
    }
}
