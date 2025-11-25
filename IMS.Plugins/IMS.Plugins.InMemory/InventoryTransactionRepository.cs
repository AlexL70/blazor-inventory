using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository(IInventoryRepository invRepo) : IInventoryTransactionRepository
    {
        private readonly List<InventoryTransaction> inventoryTransactions = new List<InventoryTransaction>();
        private readonly IInventoryRepository invRepo = invRepo;

        public Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
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
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(DateTime? startDate, DateTime? endDate, string? inventoryName, InventoryTransactionType? activityType)
        {
            var inventories = (await invRepo.GetInventoriesByNameAsync(inventoryName ?? string.Empty)).ToList();
            var query = inventoryTransactions.Where(it =>
                (!startDate.HasValue || it.TransactionDate >= startDate.Value) &&
                (!endDate.HasValue || it.TransactionDate <= endDate.Value) &&
                (string.IsNullOrEmpty(inventoryName) || inventories.Any(i => i.Name.Contains(inventoryName, StringComparison.OrdinalIgnoreCase))) &&
                (!activityType.HasValue || it.ActivityType == activityType.Value))
                .Join(inventories,
                    it => it.InventoryId,
                    i => i.Id,
                    (it, i) => { it.Inventory = i; return it; });
            return query;
        }

        Task IInventoryTransactionRepository.ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy)
        {
            inventoryTransactions.Add(new InventoryTransaction
            {
                ProductionNumber = productionNumber,
                InventoryId = inventory.Id,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.ProduceProduct,
                QuantityAfter = inventory.Quantity - quantityToConsume,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = inventory.Price,
                Inventory = inventory
            });
            return Task.CompletedTask;
        }
    }
}