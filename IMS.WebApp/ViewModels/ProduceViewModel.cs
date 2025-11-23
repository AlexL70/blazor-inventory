using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid inventory item.")]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity to produce must be at least 1.")]
        public int QuantityToProduce { get; set; }
    }
}