using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("soft_skills")]
    public class SoftSkill : IId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<SoftSkillsTechnologies> SoftSkillsTechnologies { get; set; }
    }
}
