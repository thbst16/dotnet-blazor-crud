using FluentValidation;

namespace BlazorCrud.Client.Shared
{
    public class ClaimModelViewValidator : AbstractValidator<ClaimModelView>
    {
        public ClaimModelViewValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(claim => claim.SelectedPatient).NotEmpty().WithMessage("Patient is a required field.");
            RuleFor(claim => claim.SelectedProvider).NotEmpty().WithMessage("Provider is a required field.");
            RuleFor(claim => claim.Status).NotEmpty().WithMessage("Status is a required field.");
            RuleFor(claim => claim.Type).NotEmpty().WithMessage("Type is a required field.");
            RuleFor(claim => claim.LineItems).NotEmpty().WithMessage("Claim needs to have at least one line item");
            RuleForEach(claim => claim.LineItems).SetValidator(new LineItemValidator());
        }
    }

    public class LineItemValidator : AbstractValidator<_LineItem>
    {
        public LineItemValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lineitem => lineitem.Service).NotEmpty().WithMessage("Service is a required field.");
            RuleFor(lineitem => lineitem.Amount).NotEmpty().WithMessage("Amount is a required field.");
        }
    }
}
