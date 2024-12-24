using Web_siteResume.DAL.Models;
using Web_siteResume.DAL;

namespace Web_siteResume.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;

        public AuthBL(IAuthDAL authDAL, IEncrypt encrypt)
        {
            this.authDAL = authDAL;
            this.encrypt = encrypt;
        }
        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            return await authDAL.CreateUser(user);
        }
    }
}
