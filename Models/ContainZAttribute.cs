using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication1.Models
{
    public class ContainZAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-containz", GetErrorMessage());
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string str = value as string;
            if (str != null && (str.Contains('z') || str.Contains('Z')))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() => ErrorMessage ?? "String does not contain Z";

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
