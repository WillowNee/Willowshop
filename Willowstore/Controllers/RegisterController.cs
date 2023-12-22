using Microsoft.AspNetCore.Mvc;
using Willowstore.BL.Auth;
using Willowstore.BL.Exceptions;
using Willowstore.Middleware;
using Willowstore.ViewMapper;
using Willowstore.ViewModels;

namespace Willowstore.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth authBL;

        public RegisterController(IAuth authBL) 
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.RegisterUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplecateEmailException)
                {
                    ModelState.TryAddModelError("Email", "Email already exists");
                }
            }

            return View("Index", model);
        }
    }
}
