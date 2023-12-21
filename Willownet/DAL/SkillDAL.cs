using Willownet.DAL.Models;

namespace Willownet.DAL
{
    public class SkillDAL : ISkillDAL
    {
        public async Task<int> AddProfileSkill(ProfileSkillModel model)
        {
            string sql = @"insert into ProfileSkill (ProfileId, SkillId, Level)
                    values (@ProfileId, @SkillId, @Level) returning ProfileSkillId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<int> Create(string skillName)
        {
            string sql = @"insert into Skill (SkillName)
                    values (@skillName) returning SkillId";

            return await DbHelper.QueryScalarAsync<int>(sql, new { skillName = skillName });
        }

        public async Task<SkillModel> Get(string skillName)
        {
            string sql = @"select SkillId, SkillName from Skill where SkillName = @skillName limit 1";

            return await DbHelper.QueryScalarAsync<SkillModel>(sql, new { skillName = skillName });
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            string sql = @$"select ProfileSkillId, ProfileId, ps.SkillId, SkillName, Level
                from ProfileSkill ps join Skill s on ps.SkillId = s.SkillId
                where ProfileId = @profileId";

            return await DbHelper.QueryAsync<ProfileSkillModel>(sql, new { profileId = profileId });
        }

        public async Task<IEnumerable<SkillModel>?> Search(int top, string skillName)
        {
            string sql = @"select SkillId, SkillName from Skill 
                where SkillName like @skillName limit @top";

            return await DbHelper.QueryAsync<SkillModel>(sql, new { top = top, skillname = "%" + skillName + "%" });
        }
    }
}
