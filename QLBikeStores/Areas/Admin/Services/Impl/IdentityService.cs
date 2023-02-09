using Microsoft.AspNetCore.Http;
using QLBikeStores.Areas.Admin.ModelsAdmin;

namespace QLBikeStores.Areas.Admin.Services.Impl
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public CurrentUserModel GetCurrentUserLogin()
        {
            var context = _contextAccessor.HttpContext;
            var userName = context.Session.GetString("Username");

            if (string.IsNullOrEmpty(userName))
            {
                return new CurrentUserModel();
            }

            return new CurrentUserModel()
            {
                UserName = userName,
            };
        }

        public bool IsAuthorize()
        {
            return !string.IsNullOrEmpty(GetCurrentUserLogin().UserName);
        }
    }
}
