using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Metadata.Profiles.Icc;
using Willownet.BL.Auth;
using Willownet.BL.Profile;
using Willownet.DAL.Models;
using Willownet.Middleware;
using Willownet.Service;
using Willownet.ViewMapper;
using Willownet.ViewModels;

namespace Willownet.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        
        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            this.currentUser = currentUser;
            this.profile = profile;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await currentUser.GetProfiles();

            ProfileModel? profileModel = profiles.FirstOrDefault();

            return View(profileModel != null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        //[Route("/profile/index")] // If no Route upon Index 
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null)
                throw new Exception("User does not exist");

            var profiles = await profile.Get((int)userId);

            if (model.ProfileId != null && !profiles.Any(m => m.ProfileId == model.ProfileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                profileModel.UserId = (int)userId;
                await profile.AddOrUpdate(profileModel);
                return Redirect("/");
            }

            return View("Index", new ProfileViewModel());
        }

        [HttpPost]
        // controller name/ html form action name
        [Route("/profile/uploadimage")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ImageSave(int? profileId)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null)
                throw new Exception("User does not exist");

            var profiles = await profile.Get((int)userId);

            if (profileId != null && !profiles.Any(m => m.ProfileId == profileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = profiles.FirstOrDefault(m => m.ProfileId == profileId) ?? new ProfileModel();
                profileModel.UserId = (int)userId;

                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    WebFile webFile = new WebFile();
                    string fileName = webFile.GetWebFileName(Request.Form.Files[0].FileName);
                    await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), fileName, 800, 600);
                    profileModel.ProfileImage = fileName;
                    await profile.AddOrUpdate(profileModel);
                }
            }
            return Redirect("/profile");
        }
    }
}
