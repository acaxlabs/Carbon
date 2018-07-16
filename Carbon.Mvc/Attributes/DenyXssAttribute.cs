using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text.RegularExpressions;
namespace Carbon.Mvc.Attributes
{
    

    public class DenyXssAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var tagWithoutClosingRegex = new Regex(@"<[^>]+>");

            var hasTags = tagWithoutClosingRegex.IsMatch(value.ToString());

            if (!hasTags) return ValidationResult.Success;
            return new ValidationResult($"{validationContext.DisplayName} has invalid data.");
        }
    }
}
