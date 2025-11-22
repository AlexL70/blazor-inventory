using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public string PONumber { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid inventory item.")]
        public int InventoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity to purchase must be at least 1.")]
        public int QuantityToPurchase { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Inventory price must be greater than zero.")]
        public decimal InventoryPrice { get; set; }
    }
}