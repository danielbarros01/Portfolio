using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portfolio.Entities.Interfaces;

namespace Portfolio.Entities
{
    public class Technology : IId, IHasImageUrl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }
        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
