namespace Portfolio.DTOs.Skills.SoftSkill
{
    public class SoftSkillCreationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<int> TechnologiesIds { get; set; }
    }
}
