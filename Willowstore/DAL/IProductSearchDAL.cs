using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public interface IProductSearchDAL
    {
        Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter);
    }
}
