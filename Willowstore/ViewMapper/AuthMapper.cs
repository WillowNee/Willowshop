using Microsoft.AspNetCore.Identity;
using Willowstore.DAL.Models;
using Willowstore.ViewModels;

namespace Willowstore.ViewMapper
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
