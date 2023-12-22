using System.Threading.Tasks;
using Willowstore.DAL.Models;

namespace Willowstore.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);

        Task<UserModel> GetUser(int id);

        // Return Id
        Task<int> CreateUser(UserModel model);

        Task<IEnumerable<AppRoleModel>> GetRoles(int appUserId);
    }
}
