using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;
using IMS.WebApp.ViewModelsValidations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required]
        public string SalesOrderNumber { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid product.")]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity to sell must be at least 1.")]
        [Sell_EnsureEnoughProductQuantity]
        public int QuantityToSell { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
        public decimal UnitPrice { get; set; }
        public Product? Product { get; set; } = null;
    }
}