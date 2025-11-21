using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness.Enums;

namespace IMS.CoreBusiness
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public string PONumber { get; set; } = string.Empty;
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int QuantityBefore { get; set; }
        [Required]
        public InventoryTransactionType ActivityType { get; set; }
        [Required]
        public int QuantityAfter { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string DoneBy { get; set; } = string.Empty;
        public Inventory? Inventory { get; set; } = null;
    }
}