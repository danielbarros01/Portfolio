using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities
{
    public class Education : IId, IHasImageUrl, IOrder, IReadmeUrl
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        [Column("image_url")]
        public String ImageUrl { get; set; }
        [Column("readme_url")]
        public String ReadmeUrl { get; set; }
        [Column("readme_url_es")]
        public String ReadmeUrlES { get; set; }
        public String Institution { get; set; }

        public ICollection<EducationsTechnologies> EducationsTechnologies { get; set; }
    }
}
