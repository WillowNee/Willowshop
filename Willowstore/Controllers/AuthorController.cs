using Microsoft.AspNetCore.Mvc;
using Willowstore.BL.Auth;
using Willowstore.BL.Catalog;

namespace Willowstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthor author;

        public AuthorController(IAuthor author)
        {
            this.author = author;
        }

        [HttpGet]
        [Route("/author/{uniqueid}")]
        public async Task<IActionResult> Index(string uniqueid)
        {
            var model = await author.GetAuthor(uniqueid);
            return View(model);
        }
    }
}
