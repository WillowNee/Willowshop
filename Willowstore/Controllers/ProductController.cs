using Microsoft.AspNetCore.Mvc;
using Willowstore.BL.Catalog;
using Willowstore.BL.Models;

namespace Willowstore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;

        public ProductController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("/product/{uniqueid}")]
        public async Task<IActionResult> Index(string uniqueid)
        {
            CompleteProductDataModel model = await this.product.GetProduct(uniqueid);
            return View(model);
        }
    }
}
