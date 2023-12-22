using System.ComponentModel.DataAnnotations;
using Willowstore.DAL.Models;

namespace Willowstore.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel user);

        Task<int> Authenticate(string email, string password, bool rememberMe);

        Task ValidateEmail(string email);

        Task RegisterUser(UserModel user);
    }
}
