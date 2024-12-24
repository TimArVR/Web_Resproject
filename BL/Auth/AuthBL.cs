﻿using Web_siteResume.DAL.Models;
using Web_siteResume.DAL;

namespace Web_siteResume.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;//добавляем сессии

        public AuthBL(IAuthDAL authDAL, 
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor)//добавляем сессии
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authDAL = authDAL;
            this.encrypt = encrypt;
        }
        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDAL.CreateUser(user);
            Login(id);
            return id;
        }

        public void Login(int id)
        {
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);//добавляем сессии
        }

    }
}
