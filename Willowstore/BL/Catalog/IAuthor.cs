using Willowstore.BL.Models;

namespace Willowstore.BL.Catalog
{
    public interface IAuthor
    {
        Task<AuthorDataModel> GetAuthor(string uniqueid);
    }
}
