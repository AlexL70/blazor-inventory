using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class InventoryTransactionEFCoreRepository : IInventoryTransactionRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public InventoryTransactionEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public async Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var newTransaction = new InventoryTransaction
            {
                ProductionNumber = productionNumber,
                InventoryId = inventory.Id,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.ProduceProduct,
                QuantityAfter = inventory.Quantity - quantityToConsume,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = inventory.Price,
            };
            context.InventoryTransactions.Add(newTransaction);
            await context.SaveChangesAsync();
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var newTransaction = new InventoryTransaction
            {
                PONumber = poNumber,
                InventoryId = inventory.Id,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = inventory.Quantity + quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = price,
            };
            context.InventoryTransactions.Add(newTransaction);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(DateTime? startDate, DateTime? endDate, string? inventoryName, InventoryTransactionType? activityType)
        {
            using var context = contextFactory.CreateDbContext();

            var query = context.InventoryTransactions;
            var predicate = PredicateBuilder.New<InventoryTransaction>(true);

            if (startDate.HasValue)
                predicate = predicate.And(it => it.TransactionDate >= startDate.Value);

            if (endDate.HasValue)
                predicate = predicate.And(it => it.TransactionDate <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(inventoryName))
                predicate = predicate.And(it => it.Inventory!.Name.Contains(inventoryName));

            if (activityType.HasValue)
                predicate = predicate.And(it => it.ActivityType == activityType.Value);

            return await query.Where(predicate)
                .Include(it => it.Inventory)
                .ToListAsync();
        }
    }
}