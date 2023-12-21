using Willownet.DAL.Models;

namespace Willownet.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task Update(Guid dbSessionId, string sessionData);

        Task Extend(Guid sessionId);

        Task Create(SessionModel model);

        public Task Lock(Guid sessionId);
    }
}
