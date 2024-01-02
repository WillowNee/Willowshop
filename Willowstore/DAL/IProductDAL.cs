using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public interface IProductDAL
    {
        Task<ProductModel> GetProduct(string uniqueid);

        Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productid);

        Task<IEnumerable<AuthorModel>> GetAuthorByProduct(int productid);

        Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryid);

        Task<int?> GetCategoryId(IEnumerable<string> uniqueids);
    }
}
