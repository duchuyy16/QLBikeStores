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
    public class WebAuthorizeAttribute:Attribute, IAuthorizationFilter
    {
        private string[] roleArray;
        public WebAuthorizeAttribute(string roles):base()
        {
            roleArray = roles.Split(';');
        }

        public void OnAuthorization(AuthorizationFilterContext context) //phan xu ly
        {
            var username = context.HttpContext.Session.GetString("Username");
            
            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectResult("/Admin/Accounts/Login");               
            }
            else
            {
                var userRole = context.HttpContext.Session.GetString("UserRoles");

                if (string.IsNullOrEmpty(userRole))
                {
                    context.Result = new RedirectResult("/Admin/Accounts/Login");
                }
                else
                {
                    if (!roleArray.Contains(userRole))
                    {
                        context.Result = new RedirectResult("/Common/NoPermission");
                    }
                }
            }
        }

    }
}
