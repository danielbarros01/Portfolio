using Microsoft.AspNetCore.Mvc;
using Portfolio.Entities.Interfaces;
using Portfolio.Helpers;
using Portfolio.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Education
{
    public class EducationCreationDTO : IHasImage, IWithTechnologiesIds, IOrder
    {
        public int Order { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Institution { get; set; }

        [FileSizeValidate(maxSizeInMb: 1)]
        [FileTypeValidate(groupFileType: GroupFileType.Image)]
        public IFormFile Image { get; set; }

        [ModelBinder(binderType: typeof(TypeBinder<List<int>>))]
        public List<int> TechnologyIds { get; set; }
    }
}
