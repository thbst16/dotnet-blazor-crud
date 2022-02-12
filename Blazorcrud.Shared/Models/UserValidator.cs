using FluentValidation;

namespace Blazorcrud.Shared.Models
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("First name is a required field.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Last name is a required field.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");
            RuleFor(user => user.Username).NotEmpty().WithMessage("User name is a required field.")
                .Length(3, 50).WithMessage("User name must be between 3 and 50 characters.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is a required field.")
                .Length(6, 50).WithMessage("Password must be between 6 and 50 characters.");
        }
    }
}