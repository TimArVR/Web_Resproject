namespace Web_siteResume.BL.Auth
{
    public interface IEncrypt
    {
        string HashPassword(string password, string salt);
    }
}
