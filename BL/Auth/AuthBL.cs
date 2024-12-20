using Web_siteResume.DAL.Models;
using Web_siteResume.DAL;

namespace Web_siteResume.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDAL;
        public AuthBL(IAuthDAL authDAL)
        {
            this.authDAL = authDAL;
        }
        public async Task<int> CreateUser(UserModel user)
        {
            return await authDAL.CreateUser(user);
        }
    }
}
