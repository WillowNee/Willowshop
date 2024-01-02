using Willowstore.DAL.Models;

namespace Willowstore.BL.Models
{
    public class CompleteProductDataModel
    {
        public ProductModel Product { get; set; } = null!;

        public List<ProductDetailModel> ProductDetails { get; set; } = null!;

        public List<AuthorModel>? Authors { get; set; }

        public List<CategoryModel>? Categories { get; set; }

        public string CategoryPath(int index)
        {
            if (Categories == null)
                return "";
            return String.Join("/", Categories.Skip(index).Select(m => m.CategoryUniqueId).Reverse());
        }
    }
}
