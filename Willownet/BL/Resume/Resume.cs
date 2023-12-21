using Willownet.DAL;
using Willownet.DAL.Models;

namespace Willownet.BL.Resume
{
    public class Resume : IResume
    {
        private readonly IProfileDAL profileDAL;
        public Resume(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }
        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await profileDAL.Search(top);
        }
    }
}
