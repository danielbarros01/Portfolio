using Portfolio.DTOs.Technology;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Education
{
    public class EducationDTO
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public String Institution { get; set; }

        public List<TechnologyDTO> Technologies { get; set; }
    }
}
