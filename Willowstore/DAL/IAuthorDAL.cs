using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public interface IAuthorDAL
    {
        Task<AuthorModel> GetAuthor(string uniqueid);
    }
}
