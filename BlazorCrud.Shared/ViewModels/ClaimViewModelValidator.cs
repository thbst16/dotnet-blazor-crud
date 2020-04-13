using FluentValidation;

namespace BlazorCrud.Shared.ViewModels
{
    public class ClaimViewModelValidator : AbstractValidator<ClaimViewModel>
    {
        public ClaimViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(claim => claim.SelectedPatient).NotEmpty().WithMessage("Patient is a required field.");
            RuleFor(claim => claim.SelectedOrganization).NotEmpty().WithMessage("Provider is a required field.");
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
