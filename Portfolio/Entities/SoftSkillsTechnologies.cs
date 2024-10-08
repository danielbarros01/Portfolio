﻿using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    [Table("soft_skills_technologies")]
    public class SoftSkillsTechnologies : IWithTechnologyId, IWithTechnology
    {
        [Column("technology_id")]
        public int TechnologyId { get; set; }

        [Column("soft_skill_id")]
        public int AssociationId { get; set; }

        [ForeignKey("AssociationId")]
        public SoftSkill SoftSkill { get; set; }

        [ForeignKey("TechnologyId")]
        public Technology Technology { get; set; }
    }
}
