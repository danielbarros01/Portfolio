﻿using Portfolio.DTOs.Technology;

namespace Portfolio.DTOs.Skills.TechnologySkill
{
    public class TechnicalSkillDTO
    {
        public int Id { get; set; }
        public string Proficiency { get; set; }
        public int Order { get; set; }
        public TechnologyDTO Technology { get; set; }
    }
}
