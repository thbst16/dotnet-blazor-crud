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
        }
    }
}
