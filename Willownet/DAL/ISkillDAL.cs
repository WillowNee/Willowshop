using Willownet.DAL.Models;

namespace Willownet.DAL
{
    public interface ISkillDAL
    {
        Task<int> Create(string skillName);

        Task<IEnumerable<SkillModel>?> Search(int top, string skillName);

        Task<SkillModel> Get(string skillName);

        Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);

        Task<int> AddProfileSkill(ProfileSkillModel model);
    }
}
