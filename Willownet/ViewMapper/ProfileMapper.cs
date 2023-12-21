using Willownet.DAL.Models;
using Willownet.ViewModels;

namespace Willownet.ViewMapper
{
    public static class ProfileMapper
    {
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel
            {
                ProfileId = model.ProfileId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileName = model.ProfileName,
                //ProfileImage = model.ProfileImage,
            };
        }

        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel
            {
                ProfileId = model.ProfileId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileName = model.ProfileName,
                ProfileImage = model.ProfileImage,
            };
        }
    }
}
