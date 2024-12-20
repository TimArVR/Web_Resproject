using Web_siteResume.DAL.Models;

namespace Web_siteResume.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(Web_siteResume.DAL.Models.UserModel user);
    }
}
