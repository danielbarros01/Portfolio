using Portfolio.DTOs.Technology;
using Portfolio.Entities;

namespace Portfolio.DTOs.Project
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public String ImageUrl { get; set; }
        public String VideoUrl { get; set; }
        public String LinkGithub1 { get; set; }
        public String LinkGithub2 { get; set; }

        public List<TechnologyDTO> Technologies { get; set; }
    }
}
