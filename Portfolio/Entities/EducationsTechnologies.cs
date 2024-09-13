using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("education_technologies")]
    public class EducationsTechnologies : IWithTechnologyId, IWithTechnology
    {
        [Column("technology_id")]
        public int TechnologyId { get; set; }

        [Column("education_id")]
        public int AssociationId { get; set; }

        [ForeignKey("AssociationId")]
        public Education Education { get; set; }

        [ForeignKey("TechnologyId")]
        public Technology Technology { get; set; }
    }
}
