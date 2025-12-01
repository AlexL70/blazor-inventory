using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly List<InventoryTransaction> inventoryTransactions;
        private readonly IInventoryRepository invRepo;

        public InventoryTransactionRepository(IInventoryRepository inventoryRepo)
        {
            invRepo = inventoryRepo;
            inventoryTransactions = new List<InventoryTransaction>();
            // Populate with initial purchase transactions matching the inventories
            var today = DateTime.Today;
            var random = new Random();

            // Frame
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 1,
                PONumber = $"PO-{1001}",
                InventoryId = 1,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 10,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 150.00m
            });

            // Front Wheel
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 2,
                PONumber = $"PO-{1002}",
                InventoryId = 2,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 20,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 45.00m
            });

            // Rear Wheel
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 3,
                PONumber = $"PO-{1003}",
                InventoryId = 3,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 20,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 45.00m
            });

            // Handlebars
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 4,
                PONumber = $"PO-{1004}",
                InventoryId = 4,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 15,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 25.00m
            });

            // Seat
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 5,
                PONumber = $"PO-{1005}",
                InventoryId = 5,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 12,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 30.00m
            });

            // Pedals
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 6,
                PONumber = $"PO-{1006}",
                InventoryId = 6,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 25,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 20.00m
            });

            // Chain
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 7,
                PONumber = $"PO-{1007}",
                InventoryId = 7,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 18,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 15.00m
            });

            // Brakes (Front)
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 8,
                PONumber = $"PO-{1008}",
                InventoryId = 8,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 22,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 35.00m
            });

            // Brakes (Rear)
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 9,
                PONumber = $"PO-{1009}",
                InventoryId = 9,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 22,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 35.00m
            });

            // Gear Shifter
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 10,
                PONumber = $"PO-{1010}",
                InventoryId = 10,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 14,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 40.00m
            });

            // Fork
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 11,
                PONumber = $"PO-{1011}",
                InventoryId = 11,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 11,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 60.00m
            });

            // Crankset
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 12,
                PONumber = $"PO-{1012}",
                InventoryId = 12,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 16,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 55.00m
            });

            // Cassette
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 13,
                PONumber = $"PO-{1013}",
                InventoryId = 13,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 19,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 50.00m
            });

            // Derailleur
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 14,
                PONumber = $"PO-{1014}",
                InventoryId = 14,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 13,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 45.00m
            });

            // Tire (Front)
            inventoryTransactions.Add(new InventoryTransaction
            {
                Id = 15,
                PONumber = $"PO-{1015}",
                InventoryId = 15,
                QuantityBefore = 0,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = 30,
                TransactionDate = today.AddDays(-random.Next(1, 30)),
                DoneBy = "System",
                UnitPrice = 22.00m
            });
        }

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
            var inventories = (await invRepo.GetByNameAsync(inventoryName ?? string.Empty)).ToList();
            var query = inventoryTransactions.Where(it =>
                (!startDate.HasValue || it.TransactionDate >= startDate.Value) &&
                (!endDate.HasValue || it.TransactionDate <= endDate.Value) &&
                (string.IsNullOrEmpty(inventoryName) || inventories.Any(i => i.Id == it.InventoryId)) &&
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