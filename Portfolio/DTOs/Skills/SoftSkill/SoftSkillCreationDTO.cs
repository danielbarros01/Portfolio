using Portfolio.Entities.Interfaces;

namespace Portfolio.DTOs.Skills.SoftSkill
{
    public class SoftSkillCreationDTO : IWithTechnologiesIds
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<int> TechnologyIds { get; set; }
    }
}
