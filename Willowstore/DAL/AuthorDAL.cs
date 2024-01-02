using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public class AuthorDAL : IAuthorDAL
    {
        public async Task<AuthorModel> GetAuthor(string uniqueid)
        {
            return await DbHelper.QueryScalarAsync<AuthorModel>(
                @"select AuthorId, FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId
                from Author
                where uniqueid = @id", new { id = uniqueid });
        }
    }
}
