using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using QLBikeStores.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace QLBikeStores.Controllers
{
    public class CartController : Controller
    {
        private readonly demoContext _context;
        public CartController(demoContext context)
        {
            _context = context;
        }
        public List<OrderItem> Carts
        {
            get 
            { 
                var data = HttpContext.Session.Get<List<OrderItem>>("GioHang");
                if(data == null)
                {
                    data = new List<OrderItem>();
                }
                return data;
            }
        }

        public IActionResult AddToCart(int id)
        {
            var myCart = Carts;
            var item=myCart.SingleOrDefault(p=>p.ProductId == id);
            if(item == null)
            {
                var products=_context.Products.SingleOrDefault(p=>p.ProductId == id);
                item = new OrderItem
                {
                    ProductId=id,
                    ListPrice=products.ListPrice                
                };
                myCart.Add(item);
            }    
            else
            {
                item.Quantity++;
            }
            HttpContext.Session.Set("GioHang",myCart);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
