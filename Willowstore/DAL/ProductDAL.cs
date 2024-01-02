using System.Text;
using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public class ProductDAL : IProductDAL
    {
        public async Task<IEnumerable<AuthorModel>> GetAuthorByProduct(int productid)
        {
            return await DbHelper.QueryAsync<AuthorModel>(
                @"select a.AuthorId, a.FirstName, a.MiddleName, a.Description, a.LastName, a.AuthorImage, a.UniqueId
                from Author a
                    join ProductAuthor pa on a.AuthorId = pa.AuthorId
                where pa.ProductId = @productId", new { productid = productid });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueids)
        {
            if (!uniqueids.Any())
                return 0;

            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            int index = 0;
            foreach (string uniqueid in uniqueids)
            {
                if (index == 0)
                    sb.Append($" from Category c{index} ");
                else
                    sb.Append($" inner join Category c{index} on c{index - 1}.CategoryID = c{index}.ParentCategoryId and c{index}.CategoryUniqueId = @u{index} ");

                parameters.Add($"u{index}", uniqueid);
                index++;
            }

            return await DbHelper.QueryScalarAsync<int>($"select c{index - 1}.CategoryId " + sb.ToString() + " where c0.CategoryUniqueId = @u0", parameters);
        }

        // Pls cache this request
        public async Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryid)
        {
            int? currentCategoryId = categoryid;
            List<CategoryModel> result = new List<CategoryModel>();

            while (currentCategoryId != null)
            {
                CategoryModel? model = await DbHelper.QueryScalarAsync<CategoryModel>(
                    @"select CategoryId, ParentCategoryId, CategoryName, CategoryUniqueId
                    from Category
                    where CategoryId = @categoryid", new { categoryid = currentCategoryId });

                if (model != null)
                {
                    result.Add(model);
                    currentCategoryId = model?.ParentCategoryId;
                }
            }

            return result;
        }

        public async Task<ProductModel> GetProduct(string uniqueid)
        {
            return await DbHelper.QueryScalarAsync<ProductModel>(
                @"select ProductId, CategoryId, ProductName, Price, Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId
                from Product
                where uniqueid = @id", new { id = uniqueid });
        }

        public async Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productid)
        {
            return await DbHelper.QueryAsync<ProductDetailModel>(
                @"select ParamName, StringValue
                from ProductDetail
                where ProductId = @id", new { id = productid });
        }
    }
}
