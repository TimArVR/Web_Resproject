using Web_siteResume.DAL.Models;

namespace Web_siteResume.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel model);


    }
}
