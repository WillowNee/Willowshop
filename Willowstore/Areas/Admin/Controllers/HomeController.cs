using Microsoft.AspNetCore.Mvc;
using Willowstore.Middleware;

namespace Willowstore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [SiteAuthorize(RequireAdmin: true)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
