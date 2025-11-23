using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid product.")]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity to produce must be at least 1.")]
        [ViewModelsValidations.EnsureEnoughInventoryToProduce]
        public int QuantityToProduce { get; set; }
        public Product? Product { get; set; } = null;
    }
}