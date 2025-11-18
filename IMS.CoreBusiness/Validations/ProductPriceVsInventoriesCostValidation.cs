using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations
{
    /// <summary>
    /// This validation attribute ensures that the price of a product
    /// is greater than the total cost of its associated inventories.
    /// It can only be applied to the Inventories property of the Product class.
    /// </summary>
    public class ProductPriceVsInventoriesCostValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = validationContext.ObjectInstance as Product;
            // Validate that the attribute is applied to Product class's property
            if (product == null)
            {
                ErrorMessage = $"Invalid type is checked. {typeof(ProductPriceVsInventoriesCostValidation).Name} attribute must only be applied to {typeof(Product).Name}.";
                return new ValidationResult(ErrorMessage!, [validationContext.MemberName!]);
            }
            // If it is not applied to Inventories property
            if (validationContext.MemberName != nameof(Product.Inventories))
            {
                ErrorMessage = $"{typeof(ProductPriceVsInventoriesCostValidation).Name} attribute must only be applied to {nameof(Product.Inventories)} property of {typeof(Product).Name}.";
                return new ValidationResult(ErrorMessage!, [validationContext.MemberName!]);
            }
            // Calculate total cost of inventories
            var cost = product.Inventories?
                .Sum(pi => (pi.Inventory?.Price ?? 0) * pi.Quantity) ?? 0;
            if (product.Price <= cost)
            {
                ErrorMessage = $"Product price ({product.Price.ToString("c")}) must be greater than total inventories cost ({cost.ToString("c")}).";
                return new ValidationResult(ErrorMessage!, [validationContext.MemberName!]);
            }
            return ValidationResult.Success;
        }
    }
}