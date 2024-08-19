using System.ComponentModel.DataAnnotations;

namespace Portfolio.DTOs.Category
{
    public class CategoryCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
