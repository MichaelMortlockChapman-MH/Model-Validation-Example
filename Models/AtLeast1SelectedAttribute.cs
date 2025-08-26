using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication1.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AtLeast1SelectedAttribute : ValidationAttribute, IClientModelValidator
    {
        public string ListProperty { get; }

        public AtLeast1SelectedAttribute(string listProperty = "Applicants")
        {
            ListProperty = listProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the list property by reflection
            var listProp = validationContext.ObjectType.GetProperty(ListProperty);
            if (listProp == null)
            {
                return new ValidationResult($"Property '{ListProperty}' not found.");
            }

            var applicants = listProp.GetValue(validationContext.ObjectInstance) as IEnumerable<ApplicantModel>;
            if (applicants != null && applicants.Any(a => a.IsSelected))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "At least one applicant must be selected.");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-atleast1selected", ErrorMessage ?? "At least one applicant must be selected");
            MergeAttribute(context.Attributes, "data-val-atleast1selected-group", ".applicant-group");
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
            {
                attributes.Add(key, value);
            }
        }
    }
}
