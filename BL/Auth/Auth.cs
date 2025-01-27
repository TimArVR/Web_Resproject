using Web_siteResume.DAL.Models;
using Web_siteResume.DAL;
using System.ComponentModel.DataAnnotations;
using Web_siteResume.BL;
using Web_siteResume.BL.Exeption;
using Web_siteResume.BL.General;

namespace Web_siteResume.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;//добавляем сессии
        private readonly IDbSession dbSession;//добавили кастомные сессии

        public Auth(IAuthDAL authDAL,
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor, //добавляем сессии
            IDbSession dbSession) //добавили кастомные сессии
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
        }


        public async Task Login(int id)
        {
            //httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);//добавляем сессии
            await dbSession.SetUserId(id);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDAL.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt)) //если введенный пароль и сохраненная соль (!) совпадает, то авторизуем
            {
                await Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDAL.CreateUser(user);
            await Login(id);
            return id;
        }

        //Здесь добавим валидацию Бизнес уровня, а не на уровне контроллера
        //Чтобы можно было потом и из мобильного приложения и из API вызвать ее и не переписывать
        //Проверим на дубликаты в Базе данных
        public async Task ValidateEmail(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserId != null)
            {
                throw new DuplicateEmailException();
            }
        }

        //
        public async Task Register(UserModel user)
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();//коммитим транзакцию
            }

        }
    }
}
