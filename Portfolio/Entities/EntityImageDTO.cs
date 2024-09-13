using Microsoft.VisualBasic.FileIO;
using Portfolio.Validations;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities
{
    public class EntityImageDTO
    {
        [Required]
        [FileSizeValidate(maxSizeInMb: 1)]
        [FileTypeValidate(groupFileType: GroupFileType.Image)]
        public IFormFile Image { get; set; }
    }
}
