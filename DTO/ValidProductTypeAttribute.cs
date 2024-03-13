using System.ComponentModel.DataAnnotations;

namespace _netstore.DTO;

public class ValidProductTypeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string[] validProductTypes = new string[] { "Shorts", "Jewelry", "Sports", "HomeAppliances", "Electronics", "Womens", "Mens", "Kids", "Toys" };

        if (value == null || !Array.Exists(validProductTypes, type => type.Equals(value)))
        {
            return new ValidationResult("Invalid product type.");
        }

        return ValidationResult.Success;
    }
}
