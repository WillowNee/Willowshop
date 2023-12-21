using Willownet.DAL.Models;
using Willownet.ViewModels;

namespace Willownet.ViewMapper
{
    public class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                SkillName = model.Name,
                Level = model.Level
            };
        }
    }
}
