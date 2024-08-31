using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("soft_skills_technologies")]
    public class SoftSkillsTechnologies
    {
        [Column("technology_id")]
        public int SoftSkillId { get; set; }
        [Column("soft_skill_id")]
        public int TechnologyId { get; set; }

        public SoftSkill SoftSkill { get; set; }
        public Technology Technology { get; set; }
    }
}
