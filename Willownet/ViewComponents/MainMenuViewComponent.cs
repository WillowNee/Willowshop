using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Willownet.BL.Auth;

namespace Willownet.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {

        private readonly ICurrentUser currentUser;

        public MainMenuViewComponent(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await currentUser.IsLoggedIn(); ;

            return View("Index", isLoggedIn);
        }
    }
}
