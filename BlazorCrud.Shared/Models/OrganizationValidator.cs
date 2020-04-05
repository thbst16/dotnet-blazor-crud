using FluentValidation;

namespace BlazorCrud.Shared.Models
{
    public class OrganizationValidator : AbstractValidator<Organization>
    {
        public OrganizationValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(organization => organization.Name).NotEmpty().WithMessage("Name is a required field.")
                .Length(5, 50).WithMessage("Name must be between 5 and 50 characters.");
            RuleFor(organization => organization.Type).NotEmpty().WithMessage("Type is a required field.");
            RuleFor(organization => organization.Addresses).NotEmpty().WithMessage("Organization needs to have at least one address");
            RuleForEach(organization => organization.Addresses).SetValidator(new AddressValidator());
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(address => address.Street).NotEmpty().WithMessage("Street is a required field.")
                .Length(5, 50).WithMessage("Street must be between 5 and 50 characters.");
            RuleFor(address => address.City).NotEmpty().WithMessage("City is a required field.")
                .Length(5, 50).WithMessage("City must be between 5 and 50 characters.");
            RuleFor(contactPoint => contactPoint.State).NotEmpty()
                .WithMessage("State is a required field");
        }
    }
}
