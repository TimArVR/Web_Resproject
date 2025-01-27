using System.ComponentModel.DataAnnotations;
using Web_siteResume.DAL.Models;

namespace Web_siteResume.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(Web_siteResume.DAL.Models.UserModel user);
        Task<int> Authenticate(string email, string password, bool rememberMe);

        Task ValidateEmail(string email);

        Task Register(UserModel user);

    }
}
