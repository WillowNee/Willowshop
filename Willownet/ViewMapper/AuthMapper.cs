using Microsoft.AspNetCore.Identity;
using Willownet.DAL.Models;
using Willownet.ViewModels;

namespace Willownet.ViewMapper
{
    public static class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model) 
        {
            return new UserModel
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
