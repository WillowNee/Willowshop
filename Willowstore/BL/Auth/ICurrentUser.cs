using Willowstore.DAL.Models;

namespace Willowstore.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();

        Task<int?> GetCurrentUserId();

        bool IsAdmin();
    }
}
