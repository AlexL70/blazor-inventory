using System.ComponentModel.DataAnnotations;
using IMS.UseCases.PluginInterfaces;

namespace IMS.WebApp.ViewModelsValidations
{
    public class Sell_EnsureEnoughProductQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectType != typeof(ViewModels.SellViewModel))
            {
                return new ValidationResult($"{typeof(Sell_EnsureEnoughProductQuantity).Name} attribute is only applicable to {nameof(ViewModels.SellViewModel)}");
            }
            if (validationContext.MemberName != nameof(ViewModels.SellViewModel.QuantityToSell))
            {
                return new ValidationResult($"{typeof(Sell_EnsureEnoughProductQuantity).Name} attribute is only applicable to {nameof(ViewModels.SellViewModel.QuantityToSell)} property.");
            }
            var viewModel = (ViewModels.SellViewModel)validationContext.ObjectInstance;
            if (viewModel.Product is not null && viewModel.QuantityToSell > viewModel.Product.Quantity)
            {
                ErrorMessage = $"Not enough product quantity to sell. Product '{viewModel.Product.Name}' has {viewModel.Product.Quantity} units available, but {viewModel.QuantityToSell} units are requested to sell.";
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}