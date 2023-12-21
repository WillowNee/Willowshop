using Microsoft.AspNetCore.Mvc;
using System.Net;
using Willownet.BL.Auth;
using Willownet.Middleware;
using Willownet.ViewMapper;
using Willownet.ViewModels;

namespace Willownet.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBL;

        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Willownet.BL.Exceptions.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Name or Email arent correct");
                }
            }

            return View("Index", model);
        }
    }
}
