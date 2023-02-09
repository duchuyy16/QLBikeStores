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
    public class CartController : Controller
    {
        //private readonly demoContext _context;
        //public CartController(demoContext context)
        //{
        //    _context = context;
        //}
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        public IActionResult AddToCart(int id, int quantity, string type = "Normal")
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var url = string.Format(ConstantValues.Product.ChiTietSanPham, id);
                var products = Utilities.SendDataRequest<Product>(url); /*_context.Products.SingleOrDefault(p => p.ProductId == id);*/
                item = new CartItem
                {
                    ProductId = id,
                    ProductName = products.ProductName,
                    Image = products.ImageBike,
                    ListPrice = (products.ListPrice - (products.ListPrice * products.Discount / 100)),
                    Quantity = quantity,

                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            HttpContext.Session.Set("GioHang", myCart);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            ViewData["Cart"] = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(Carts);
        }

        //public IActionResult RemoveCart(int id)
        //{
        //    var myCart = Carts;
        //    foreach (var item in myCart)
        //    {
        //        if (item.ProductId == id)
        //        {
        //            myCart.Remove(item);
        //            break;
        //        }
        //    }
        //    HttpContext.Session.Set("GioHang", myCart);
        //    return RedirectToAction("Index");
        //    Carts.Where(p => p.ProductId != id).ToList();

        //}

        public IActionResult RemoveCart(int id)
        {
            HttpContext.Session.Set("GioHang", Carts.Where(p => p.ProductId != id).ToList());
            return RedirectToAction("Index");
        }

        public IActionResult CheckOut()
        {
            return View(Carts);
        }

        public IActionResult OrderSuccess()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<CartItem> lstCart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                //gan du lieu cho order

                Order ord = new Order();
                ord.CustomerId = HttpContext.Session.GetInt32("ClientCustomerId");
                ord.OrderStatus = 3;
                ord.OrderDate = DateTime.Now;
                ord.RequiredDate = DateTime.Today.AddDays(3);
                ord.ShippedDate = DateTime.Today.AddDays(2);
                ord.StoreId = 1;
                ord.StaffId = 1;
                Utilities.SendDataRequest<Order>(ConstantValues.Order.ThemDonHang, ord);
                //_context.Orders.Add(ord);
                //_context.SaveChanges();

                foreach (CartItem cart in lstCart)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = ord.OrderId,
                        ItemId = cart.ProductId,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,
                        ListPrice = cart.ListPrice,
                        //Discount=product.Discount,
                    };
                    Utilities.SendDataRequest<OrderItem>(ConstantValues.OrderItem.ThemChiTietDonDatHang, orderItem);
                    //_context.OrderItems.Add(orderItem);
                    //_context.SaveChanges();
                }
                HttpContext.Session.Remove("GioHang");
                return View();
            }
            
        }

    }
}
