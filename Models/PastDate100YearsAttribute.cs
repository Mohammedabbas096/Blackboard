using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group1Blackboard.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PastDate100YearsAttribute : ValidationAttribute, IClientModelValidator
    {
        public int MaxYears { get; set; } = 100;

        public PastDate100YearsAttribute(int maxYears = 100)
        {
            MaxYears = maxYears;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(GetDefaultErrorMessage());
            }

            if (value is DateTime dateValue)
            {
                DateTime today = DateTime.Today;
                DateTime minDate = today.AddYears(-MaxYears);

                if (dateValue.Date > today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date of birth must be a past date.");
                }

                if (dateValue.Date < minDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"Date of birth cannot be more than {MaxYears} years ago.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Please enter a valid date.");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }

            context.Attributes.Add("data-val-pastdate100years", GetDefaultErrorMessage());
            context.Attributes.Add("data-val-pastdate100years-maxyears", MaxYears.ToString());
        }

        private string GetDefaultErrorMessage() =>
            ErrorMessage ?? $"Date of birth must be a past date, no more than {MaxYears} years ago.";
    }
}

