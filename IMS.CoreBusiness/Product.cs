using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness.Validations;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        [ProductPriceVsInventoriesCostValidation]
        public IList<ProductInventory> Inventories { get; set; } = [];

        public void AddInventory(Inventory inventory)
        {
            var existing = Inventories
                .FirstOrDefault(pi => pi.InventoryId == inventory.Id);
            if (existing != null)
                return;
            else
                Inventories.Add(new ProductInventory
                {
                    ProductId = Id,
                    Product = this,
                    InventoryId = inventory.Id,
                    Inventory = inventory,
                    Quantity = 1
                });
        }

        public void RemoveInventory(ProductInventory productInventory)
        {
            if (!Inventories.Contains(productInventory))
                return;
            Inventories.Remove(productInventory);
        }
    }
}