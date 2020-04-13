using FluentValidation;

namespace BlazorCrud.Shared.Models
{
    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(claim => claim.Patient).NotEmpty().WithMessage("Patient is a required field.")
                .Length(5, 50).WithMessage("Patient must be between 5 and 50 characters.");
            RuleFor(claim => claim.Organization).NotEmpty().WithMessage("Organization is a required field.")
                .Length(5, 50).WithMessage("Organization must be between 5 and 50 characters.");
            RuleFor(claim => claim.Status).NotEmpty().WithMessage("Status is a required field.");
            RuleFor(claim => claim.Type).NotEmpty().WithMessage("Type is a required field.");
            RuleFor(claim => claim.LineItems).NotEmpty().WithMessage("Claim needs to have at least one line item");
            RuleForEach(claim => claim.LineItems).SetValidator(new LineItemValidator());
        }
    }

    public class LineItemValidator : AbstractValidator<LineItem>
    {
        public LineItemValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lineitem => lineitem.Service).NotEmpty().WithMessage("Service is a required field.");
            RuleFor(lineitem => lineitem.Amount).NotEmpty().WithMessage("Amount is a required field.");
        }
    }
}