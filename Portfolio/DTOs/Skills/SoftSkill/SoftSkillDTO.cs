using Portfolio.DTOs.Technology;

namespace Portfolio.DTOs.Skills.SoftSkill
{
    public class SoftSkillDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<TechnologyDTO> Technologies { get; set; }
    }
}
