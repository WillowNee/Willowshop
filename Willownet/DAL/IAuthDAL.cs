using Willownet.DAL.Models;

namespace Willownet.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);

        Task<UserModel> GetUser(int id);

        // Return Id
        Task<int> CreateUser(UserModel model);
    }
}
