using Web_siteResume.DAL.Models;
namespace Web_siteResume.BL.Auth
{
    public interface IDbSession
    {
        Task <SessionModel> GetSession();
        Task<int> SetUserId (int userId);
        Task<int?> GetUserId();
        Task<bool> isLoggedIn();
    }
}
