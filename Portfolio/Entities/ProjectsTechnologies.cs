using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("project_technologies")]
    public class ProjectsTechnologies : IWithTechnology
    {
        [Column("technology_id")]
        public int TechnologyId { get; set; }

        [Column("project_id")]
        public int AssociationId { get; set; }

        [ForeignKey("AssociationId")]
        public Project Project { get; set; }

        [ForeignKey("TechnologyId")]
        public Technology Technology { get; set; }
    }
}
