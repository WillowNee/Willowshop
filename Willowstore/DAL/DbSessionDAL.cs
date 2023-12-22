using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task Create(SessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionId, @SessionData, @Created, @LastAccessed, @UserId)";

            await DbHelper.ExecuteAsync(sql, model);
        }

        public async Task Extend(Guid dbSessionId)
        {
            string sql = @"update DbSession
                      set LastAccessed = @lastAccessed
                      where DbSessionID = @dbSessionID";

            await DbHelper.ExecuteAsync(sql, new { dbSessionId = dbSessionId, lastAccessed = DateTime.Now});
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession 
                    where DbSessionID = @sessionId";
            var sessions = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });

            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from DbSession 
                    where DbSessionID = @sessionId for update";

            await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
        }

        public async Task Update(Guid dbSessionId, string sessionData)
        {
            string sql = @"update DbSession
                      set SessionData = @sessionData
                      where DbSessionID = @dbSessionID";

            await DbHelper.ExecuteAsync(sql, new { dbSessionId = dbSessionId, sessionData = sessionData});
        }
    }
}
