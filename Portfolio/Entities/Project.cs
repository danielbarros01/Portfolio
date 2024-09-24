using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    public class Project : IId, IHasImageUrl, IReadmeUrl
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }

        [Column("image_url")]
        public String ImageUrl { get; set; }

        [Column("video_url")]
        public String VideoUrl { get; set; }
        
        [Column("readme_url")]
        public String ReadmeUrl { get; set; }

        [Column("readme_url_es")]
        public String ReadmeUrlES { get; set; }

        [Column("link_github_1")]
        public String LinkGithub1 { get; set; }

        [Column("link_github_2")]
        public String LinkGithub2 { get; set; }

        public ICollection<ProjectsTechnologies> ProjectsTechnologies { get; set; }
    }
}
