using Web_siteResume.DAL.Models;
namespace Web_siteResume.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> GetSession(Guid sessionId);
        Task<int> UpdateSession(SessionModel model);
        Task<int> CreateSession(SessionModel model);
    }
}
