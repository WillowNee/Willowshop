using System.ComponentModel.DataAnnotations;
using Willownet.DAL.Models;

namespace Willownet.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel user);

        Task<int> Authenticate(string email, string password, bool rememberMe);

        //Task<ValidationResult?> ValidateEmail(string email);

        Task ValidateEmail(string email);

        Task RegisterUser(UserModel user);
    }
}
