using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Skills.TechnologySkill
{
    public class TechnicalSkillCreationDTO : IOrder
    {
        public string Proficiency { get; set; }
        public int Order { get; set; }
        public int TechnologyId { get; set; }
    }
}
