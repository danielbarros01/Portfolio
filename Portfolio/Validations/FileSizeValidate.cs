using System.ComponentModel.DataAnnotations;

namespace Portfolio.Validations
{
    public class FileSizeValidate : ValidationAttribute
    {
        private readonly int maxSizeInMb;

        public FileSizeValidate(int maxSizeInMb)
        {
            this.maxSizeInMb = maxSizeInMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            
            if (value is not IFormFile)
                return ValidationResult.Success;


            FormFile formFile = value as FormFile;
            if (formFile.Length > maxSizeInMb * Math.Pow(1024, 2))
            {
                return new ValidationResult($"The maximum file can be {maxSizeInMb}mb");
            }

            return ValidationResult.Success;
        }
    }
}
