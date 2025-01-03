﻿using Web_siteResume.DAL.Models;
using Web_siteResume.DAL;
using System.ComponentModel.DataAnnotations;
using Web_siteResume.BL;

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

        public async Task<int> Authenticate(string email, string password, bool rememberMe) 
        {
            var user = await authDAL.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt)) //если введенный пароль и сохраненная соль (!) совпадает, то авторизуем
            {
                Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        //Здесь добавим валидацию Бизнес уровня, а не на уровне контроллера
        //Чтобы можно было потом и из мобильного приложения и из API вызвать ее и не переписывать
        //Проверим на дубликаты в Базе данных

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserId != null) 
            {
                return new ValidationResult("Email уже существует");
            }
            return null;
        
        }
    }
}
