using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class PictureValidationAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
    private readonly long _maxFileSizeBytes;

    public PictureValidationAttribute(long maxFileSizeBytes)
    {
        _maxFileSizeBytes = maxFileSizeBytes;
        ErrorMessage = $"Only {string.Join(", ", _allowedExtensions)} files are allowed, and size must be less than {maxFileSizeBytes / 1024 / 1024} MB.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file == null)
            return ValidationResult.Success; 

        var extension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!_allowedExtensions.Contains(extension))
        {
            return new ValidationResult($"Invalid file type. Allowed types are: {string.Join(", ", _allowedExtensions)}");
        }

        if (file.Length > _maxFileSizeBytes)
        {
            return new ValidationResult($"File size exceeds the limit of {_maxFileSizeBytes / 1024 / 1024} MB.");
        }

        return ValidationResult.Success;
    }
}
