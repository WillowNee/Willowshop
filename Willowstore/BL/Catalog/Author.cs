using Willowstore.BL.Models;
using Willowstore.DAL;
using Willowstore.DAL.Models;

namespace Willowstore.BL.Catalog
{
    public class Author : IAuthor
    {
        private readonly IAuthorDAL authorDAL;
        private readonly IProductSearchDAL productSearchDAL;

        public Author(IAuthorDAL authorDAL, IProductSearchDAL productSearchDAL)
        {
            this.authorDAL = authorDAL;
            this.productSearchDAL = productSearchDAL;
        }

        public async Task<AuthorDataModel> GetAuthor(string uniqueid)
        {
            var author = await authorDAL.GetAuthor(uniqueid);

            var productCards = await productSearchDAL.Search(new ProductSearchFilter()
            {
                Top = 100,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                AuthorId = author.AuthorId
            });

            return new AuthorDataModel
            {
                Author = author,
                ProductCards = productCards.ToList()
            };
        }
    }
}
