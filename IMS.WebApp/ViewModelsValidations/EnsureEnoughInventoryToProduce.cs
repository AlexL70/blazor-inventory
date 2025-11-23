using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModelsValidations
{
    public class EnsureEnoughInventoryToProduce : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as ViewModels.ProduceViewModel;
            if (viewModel is null)
            {
                ErrorMessage = $"{typeof(EnsureEnoughInventoryToProduce).Name} is only applicable to {typeof(ViewModels.ProduceViewModel).Name} type.";
                return new ValidationResult(ErrorMessage);
            }
            if (validationContext.MemberName != nameof(ViewModels.ProduceViewModel.QuantityToProduce))
            {
                ErrorMessage = $"{typeof(EnsureEnoughInventoryToProduce).Name} is only applicable to {nameof(ViewModels.ProduceViewModel.QuantityToProduce)} property.";
                return new ValidationResult(ErrorMessage);
            }

            if (viewModel.Product is not null)
            {
                foreach (var prodInv in viewModel.Product.Inventories)
                {
                    var availableQty = prodInv.Inventory?.Quantity ?? 0;
                    var requiredQty = prodInv.Quantity * viewModel.QuantityToProduce;
                    if (availableQty < requiredQty)
                    {
                        ErrorMessage = $"Not enough inventory to produce the product. Inventory '{prodInv.Inventory?.Name}' has {availableQty} units available, but {requiredQty} units are required.";
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}