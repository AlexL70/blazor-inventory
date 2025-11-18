
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IMS.CoreBusiness
{
    public class ProductInventory
    {
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        public int InventoryId { get; set; }
        [JsonIgnore]
        public Inventory? Inventory { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Quantity { get; set; }
    }
}