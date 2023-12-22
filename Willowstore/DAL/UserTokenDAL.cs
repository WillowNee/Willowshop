
using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public class UserTokenDAL : IUserTokenDAL
    {
        public async Task<Guid> Create(int userId)
        {
            Guid tokenId = Guid.NewGuid();

            string sql = @"insert into UserToken (UserTokenId, UserId, Created)
                    values (@newId, @userId, NOW())";

            await DbHelper.ExecuteAsync(sql, new { userId = userId, newId = tokenId });

            return tokenId;
        }

        public async Task<int?> Get(Guid tokenId)
        {
            string sql = @"select UserId from UserToken 
                    where UserTokenId = @tokenId";

            return await DbHelper.QueryScalarAsync<int>(sql, new { tokenId = tokenId });
        }
    }
}
