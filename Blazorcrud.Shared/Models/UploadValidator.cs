using FluentValidation;

namespace Blazorcrud.Shared.Models
{
    public class UploadValidator : AbstractValidator<Upload>
    {
        public UploadValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(upload => upload.FileName).NotEmpty().WithMessage("File name is a required field.")
                .Length(5, 50).WithMessage("File name must be between 5 and 50 characters.");
            RuleFor(upload => upload.FileContent).NotEmpty().WithMessage("Uploaded file is required.");
        }
    }
}