using Willownet.DAL.Models;

namespace Willownet.BL.Profile
{
    public interface ISkill
    {
        Task<IEnumerable<SkillModel>?> Search(int top, string skillname);
    }
}
