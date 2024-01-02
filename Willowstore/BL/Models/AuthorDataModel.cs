using Willowstore.DAL.Models;

namespace Willowstore.BL.Models
{
    public class AuthorDataModel
    {
        public AuthorModel Author { get; set; } = null!;

        public List<ProductCardModel> ProductCards { get; set; } = null!;
    }
}
