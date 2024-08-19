using Portfolio.Entities.Interfaces;
using Portfolio.Validations;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.DTOs.Technology
{
    public class TechnologyCreationDTO : IHasImage
    {
        [Required]
        public string Name { get; set; }

        [FileSizeValidate(maxSizeInMb:1)]
        [FileTypeValidate(groupFileType: GroupFileType.Image)]
        public IFormFile Image { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
}
