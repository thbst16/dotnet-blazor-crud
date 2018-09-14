using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Client.Services
{
    public class ModelValidator : IModelValidator
    {
        public event EventHandler<ValidationErrorEventArgs> OnValidationDone;

        public bool Validate(object instance)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(instance, new ValidationContext(instance, null, null), res, true);

            OnValidationDone?.Invoke(this, new ValidationErrorEventArgs() { Errors = res, Instance = instance });
            Console.Write("Validation result : " + isValid);
            return isValid;
        }
    }
}
