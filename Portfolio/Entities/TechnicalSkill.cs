using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("technical_skills")]
    public class TechnicalSkill : IId, IOrder
    {
        public int Id { get; set; }
        public string Proficiency { get; set; }
        public int Order { get; set; }

        [Column("technology_id")]
        public int TechnologyId { get; set; }

        public Technology Technology { get; set; }
    }
}
