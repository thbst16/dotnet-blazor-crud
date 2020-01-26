using FluentValidation;

namespace BlazorCrud.Shared.Models
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(patient => patient.Name).NotEmpty().WithMessage("Name is a required field.")
                .Length(5, 50).WithMessage("Name must be between 5 and 50 characters.");
            RuleFor(patient => patient.Gender).NotEmpty()
                .WithMessage("Gender is a required field.");
            RuleFor(patient => patient.PrimaryCareProvider).NotEmpty().WithMessage("Name is a required field.")
                .Length(5, 50).WithMessage("PCP must be between 5 and 50 characters.");
            RuleFor(patient => patient.State).NotEmpty().WithMessage("State is a required field.");
            RuleFor(patient => patient.Contacts).NotEmpty().WithMessage("Patient needs to have at least one contact point");
            RuleForEach(patient => patient.Contacts).SetValidator(new ContactPointValidator());
        }
    }

    public class ContactPointValidator : AbstractValidator<ContactPoint>
    {
        public ContactPointValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(contactPoint => contactPoint.System).NotEmpty()
                .WithMessage("System is a required field");
            RuleFor(contactPoint => contactPoint.Value).NotEmpty().WithMessage("Phone number is a required field.")
                .Length(10, 15).WithMessage("Phone number must be between 10 and 15 characters.");
            RuleFor(contactPoint => contactPoint.Use).NotEmpty()
                .WithMessage("Use is a required field");
        }
    }
}
