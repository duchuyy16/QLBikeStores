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
        //là một mảng chứa các vai trò của người dùng cần được xác thực để truy cập vào các trang yêu cầu quyền truy cập.
        private string[] roleArray;
        public WebAuthorizeAttribute(string roles):base()
        {
            //chuỗi "roles" gán giá trị của nó cho biến "roleArray" bằng cách sử dụng phương thức "Split" để chuyển chuỗi "roles" thành một mảng.
            roleArray = roles.Split(';');
        }

        public void OnAuthorization(AuthorizationFilterContext context) 
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
                    if (!roleArray.Contains(userRole))//Nếu vai trò của người dùng không nằm trong mảng "roleArray"
                    {
                        context.Result = new RedirectResult("/Common/NoPermission");
                    }
                }
            }
        }

    }
}
