using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication1.Models
{
    public class TotalSelectedIs100 : ValidationAttribute, IClientModelValidator
    {
        public string ListProperty { get; }
        public bool UseExact { get; }
        public string CSSSelectionGroup { get; }
        public string CSSNumberGroup { get; }


        public TotalSelectedIs100(string cssSelectionGroup, string cssNumberGroup, string listProperty = "Applicants", bool useExact = true)
        {
            ListProperty = listProperty;
            UseExact = useExact;
            CSSSelectionGroup = cssSelectionGroup;
            CSSNumberGroup = cssNumberGroup;
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
            decimal total = applicants.Where(a => a.IsSelected).Sum(a => a.OwnedPercentage ?? 0);
            if (total == 100M)
            {
                return ValidationResult.Success;
            }
            if (!UseExact && total >= 99.99M && total <= 100M)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() => ErrorMessage ?? "Total must be 100";

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-totalselectedis100", GetErrorMessage());
            MergeAttribute(context.Attributes, "data-val-totalselectedis100-selectiongroup", CSSSelectionGroup);
            MergeAttribute(context.Attributes, "data-val-totalselectedis100-numbergroup", CSSNumberGroup);
            MergeAttribute(context.Attributes, "data-val-totalselectedis100-exact", UseExact.ToString());
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
