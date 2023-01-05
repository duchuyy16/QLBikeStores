using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using System.Collections.Generic;
using System;
namespace QLBikeStores.Controllers
{
    public class PaymentController : Controller
    {
        private readonly demoContext _context;
        public PaymentController(demoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //if (Session["idUser"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            //    //lay thong tin gio hang
            //    var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            //    //gan du lieu cho order
            //    Order ord = new Order();
            //    ord.OrderId=;
            //    ord.CustomerId=
            //    ord.OrderStatus = 4;
            //    ord.OrderDate = DateTime.Now;
            //    ord.RequiredDate = DateTime.Today.AddDays(3);
            //    ord.ShippedDate = DateTime.Today.AddDays(2);
            //    ord.StoreId = 1;
            //    ord.StaffId = 1;
            //    _context.Orders.Add(ord);
            //    _context.SaveChanges();

            //    //lay orderid vua moi tao de luu vao bang orderitems
            //    int orderId = ord.OrderId;

            //    List<OrderItem> lstOrderItem = new();
            //    foreach (var item in lstCart)
            //    {
            //        OrderItem orderItem = new OrderItem();
            //        orderItem.OrderId = orderId;
            //        orderItem.ProductId = item.ProductId;
            //        orderItem.Quantity = item.Quantity;
            //        orderItem.ListPrice = item.ListPrice;
            //        lstOrderItem.Add(orderItem);
            //    }

            //    _context.OrderItems.AddRange(lstOrderItem);
            //    _context.SaveChanges();
            //}
            return View();
        }
    }
}
