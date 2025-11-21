using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly List<InventoryTransaction> inventoryTransactions = new List<InventoryTransaction>();
        public void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            // Implementation for purchasing inventory transaction
            inventoryTransactions.Add(new InventoryTransaction
            {
                PONumber = poNumber,
                InventoryId = inventory.Id,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = inventory.Quantity + quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = price,
                Inventory = inventory
            });
        }
    }
}