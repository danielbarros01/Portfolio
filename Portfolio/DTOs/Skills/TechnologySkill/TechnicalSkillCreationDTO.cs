using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Skills.TechnologySkill
{
    public class TechnicalSkillCreationDTO
    {
        public string Proficiency { get; set; }
        public int Order { get; set; }
        public int TechnologyId { get; set; }
    }
}
