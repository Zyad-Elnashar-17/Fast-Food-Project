using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _MaxFileSize;

        public MaxFileSizeAttribute(int MaxFileSize)
        {
            _MaxFileSize = MaxFileSize;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {

                if (file.Length > _MaxFileSize)
                {
                    return new ValidationResult(errorMessage: $"Max Allowed Size Is {_MaxFileSize} Bytes ");
                }
            }
            return ValidationResult.Success;
        }
    }
}
