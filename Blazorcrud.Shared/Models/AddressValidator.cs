using FluentValidation;

namespace Blazorcrud.Shared.Models
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(address => address.Street).NotEmpty().WithMessage("Street is a required field.")
                .Length(5, 50).WithMessage("Street must be between 5 and 50 characters.");
            RuleFor(address => address.City).NotEmpty().WithMessage("City is a required field.")
                .Length(5, 50).WithMessage("City must be between 5 and 50 characters.");
            RuleFor(address => address.State).NotEmpty().WithMessage("State is a required field.")
                .Length(5, 50).WithMessage("State must be between 5 and 50 characters.");
            RuleFor(address => address.ZipCode).NotEmpty().WithMessage("Zip code is a required field.")
                .Length(5, 30).WithMessage("Zip code must be between 5 and 30 characters.");
        }
    }
}