using System.Text;
using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public class ProductSearchDAL : IProductSearchDAL
    {
        static string ProductCardModelSql = @"select p.ProductImage, p.ProductName, p.Price, p.UniqueId from Product p ";

        public async Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("top", filter.Top);

            Dictionary<string, object> joins = new Dictionary<string, object>();
            StringBuilder whereSql = new StringBuilder();
            whereSql.Append(" where 1=1 ");

            if (filter.AuthorId != null )
            {
                if (!joins.ContainsKey("ProductAuthor"))
                    joins.Add(" ProductAuthor", " join ProductAuthor pa on p.ProductId = pa.ProductId ");

                whereSql.Append(" and pa.AuthorId = @authorid ");
                parameters.Add("authorid", filter.AuthorId);
            }

            if (filter.CategoryId != null)
            {
                if (!joins.ContainsKey("Categories"))
                    joins.Add(" Categories", @"
                        join Category c1 on p.CategoryId = c1.CategoryId 
                        join Category c2 on c2.CategoryId = c1.ParentCategoryId
                        join Category c3 on c3.CategoryId = c2.ParentCategoryId
                    ");

                whereSql.Append(" and @categoryid  in (c1.CategoryId, c2.CategoryId, c3.CategoryId, c3.ParentCategoryId)  ");
                parameters.Add("categoryid", filter.CategoryId);
            }

            return await DbHelper.QueryAsync<ProductCardModel>(
                ProductCardModelSql +
                String.Join(" ", joins.Values) +
                whereSql.ToString() +
                " order by " + filter.SortBy.ToString() + " " + filter.Direction.ToString() +
                " limit @top ", parameters);
        }
    }
}
