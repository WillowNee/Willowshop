using Willownet.DAL.Models;

namespace Willownet.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<ProfileModel>> Search(int top);
    }
}
