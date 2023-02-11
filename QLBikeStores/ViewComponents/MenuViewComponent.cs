using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.ViewComponents
{
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menu = Utilities.SendDataRequest<List<MenuViewModel>>("/api/Menu/GetMenuList");
            List<CartItem> lstCart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            if(lstCart!=null)
            {
                ViewData["Count"] = lstCart.Count;
            }    
            return View(menu);
        }
    }
}
