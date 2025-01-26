namespace Web_siteResume.BL.Auth
{
    public interface ICurrentUser
    {
       Task <bool> IsLoggedIn();
    }
}
