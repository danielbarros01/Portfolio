
using Microsoft.AspNetCore.Mvc;
using Portfolio.Entities.Interfaces;
using Portfolio.Helpers;
using Portfolio.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Project
{
    public class ProjectCreationDTO : IHasImage, IWithTechnologiesIds
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String? Url { get; set; }

        [FileSizeValidate(maxSizeInMb: 1)]
        [FileTypeValidate(groupFileType: GroupFileType.Image)]
        public IFormFile Image { get; set; }

        [FileSizeValidate(maxSizeInMb: 4)]
        [FileTypeValidate(groupFileType: GroupFileType.Video)]
        public IFormFile? VideoUrl { get; set; }

        public String? LinkGithub1 { get; set; }
        public String? LinkGithub2 { get; set; }

        [ModelBinder(binderType:typeof(TypeBinder<List<int>>))]
        public List<int> TechnologyIds { get; set; }
    }
}
