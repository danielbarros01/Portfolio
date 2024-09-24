using System.ComponentModel.DataAnnotations;

namespace Portfolio.Validations
{
    public class FileTypeValidate : ValidationAttribute
    {
        private readonly string[] validTypes;

        public FileTypeValidate(string[] validTypes )
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidate(GroupFileType groupFileType)
        {
            var validImageTypes = new string[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            var validVideoTypes = new string[] { "video/mp4", "video/webm", "video/avi" };
            var validReadmeTypes = new string[] { "text/plain", "text/markdown", "text/html" };

            validTypes = groupFileType switch
            {
                GroupFileType.Image => validImageTypes,
                GroupFileType.Video => validVideoTypes,
                GroupFileType.Readme => validReadmeTypes,
                _ => validImageTypes.Union(validVideoTypes).ToArray()
            };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is not IFormFile)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (!(validTypes.Contains(formFile.ContentType)))
            {
                return new ValidationResult($"The file can be of type: {string.Join(",", validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
