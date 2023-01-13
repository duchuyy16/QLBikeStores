using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.Attributes
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class WebAuthorizeAttribute:Attribute,IAsyncAuthorizationFilter
    {
        private string[] roleArray;
        public WebAuthorizeAttribute(string role):base()
        {
            roleArray = role.Split(';');
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context) //phan xu ly
        {
            var username = context.HttpContext.Session.GetString("Username");
            
            if (username == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account",null);               
            }
            else
            {
                var democontext = new demoContext();
                var staff=await democontext.Staffs.SingleOrDefaultAsync(s => s.Username==username);
                if (!roleArray.Contains(staff.Role.RoleName))
                    context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }

    }
}
