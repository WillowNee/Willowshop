using Microsoft.AspNetCore.Mvc;
using Willowstore.BL.Catalog;
using Willowstore.ViewModels;

namespace Willowstore.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProduct product;

        public ProductCategoryController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("/product-category/{uniqueid}/{uniqueid1?}/{uniqueid2?}/{uniqueid3?}")]
        public async Task<IActionResult> Index(string uniqueid, string? uniqueid1, string? uniqueid2, string? uniqueid3)
        {
            string?[] uniqueids = {uniqueid, uniqueid1, uniqueid2, uniqueid3};
            int? categoryId = await product.GetCategoryId((IEnumerable<string>)uniqueids.Where(p => p != null));

            if (categoryId != null)
            {
                var products = await product.GetByCategory((int)categoryId);

                CatalogViewModel model = new CatalogViewModel() { Products = products.ToList() };
                return View(model);
            }

            return NotFound();
        }
    }
}
