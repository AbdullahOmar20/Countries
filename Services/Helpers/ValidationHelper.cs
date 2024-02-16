using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            ValidationContext validation = new ValidationContext(obj);
            List<ValidationResult> validationresult = new List<ValidationResult>();
            bool isvalid = Validator.TryValidateObject(obj, validation, validationresult, true);
            if (!isvalid)
            {
                throw new ArgumentException(validationresult.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
