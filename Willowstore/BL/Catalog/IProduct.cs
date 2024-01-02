using Willowstore.BL.Models;
using Willowstore.DAL.Models;

namespace Willowstore.BL.Catalog
{
    public interface IProduct
    {
        Task<IEnumerable<ProductCardModel>> GetNew(int top);

        Task<CompleteProductDataModel> GetProduct(string uniqueid);

        Task<IEnumerable<ProductCardModel>> GetByCategory(int categoryId);

        Task<int?> GetCategoryId(IEnumerable<string> uniqueids);
    }
}
