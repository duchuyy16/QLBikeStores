using QLBikeStores.Areas.Admin.ModelsAdmin;

namespace QLBikeStores.Areas.Admin.Services
{
    public interface IIdentityService
    {
        CurrentUserModel GetCurrentUserLogin();
        bool IsAuthorize();
    }
}
