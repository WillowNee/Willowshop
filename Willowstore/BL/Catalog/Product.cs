using Willowstore.BL.Models;
using Willowstore.DAL;
using Willowstore.DAL.Models;

namespace Willowstore.BL.Catalog
{
    public class Product : IProduct
    {
        private readonly IProductDAL productDAL;
        private readonly IProductSearchDAL productSearchDAL;

        public Product(IProductDAL productDAL, IProductSearchDAL productSearchDAL)
        {
            this.productDAL = productDAL;
            this.productSearchDAL = productSearchDAL;
        }

        public async Task<IEnumerable<ProductCardModel>> GetByCategory(int categoryId)
        {
            return await productSearchDAL.Search(new ProductSearchFilter()
            {
                Top = 24,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                CategoryId = categoryId
            });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueids)
        {
            return await productDAL.GetCategoryId(uniqueids);
        }

        public async Task<IEnumerable<ProductCardModel>> GetNew(int top)
        {
            return await productSearchDAL.Search(new ProductSearchFilter()
            {
                Top = 6,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc
            });
        }

        public async Task<CompleteProductDataModel> GetProduct(string uniqueid)
        {
            var product = await productDAL.GetProduct(uniqueid);
            var productDetails = await productDAL.GetProductDetails(product.ProductId);
            var author = await productDAL.GetAuthorByProduct(product.ProductId);
            var categories = await productDAL.GetCategoryTree(product.CategoryId);

            return new CompleteProductDataModel
            {
                Product = product,
                ProductDetails = productDetails.ToList(),
                Authors = author.ToList(),
                Categories = categories.ToList(),
            };
        }
    }
}
