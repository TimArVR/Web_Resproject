namespace Web_siteResume.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;//добавили кастомные сессии
        public CurrentUser(
            IHttpContextAccessor httpContextAccessor, 
            IDbSession dbSession)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
        }
        public async Task <bool> IsLoggedIn()
        {
            //int? id = httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME);
            return /*id != null*/await dbSession.isLoggedIn();

        }
    }
}
