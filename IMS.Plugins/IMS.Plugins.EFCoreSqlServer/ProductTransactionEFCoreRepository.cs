using Microsoft.EntityFrameworkCore;
using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;
using LinqKit;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;
        private readonly IProductRepository prodRepo;
        private readonly IInventoryRepository invRepo;
        private readonly IInventoryTransactionRepository invTranRepo;



        public ProductTransactionEFCoreRepository(IDbContextFactory<IMSContext> contextFactory, IProductRepository prodRepo, IInventoryRepository invRepo, IInventoryTransactionRepository invTranRepo)
        {
            this.contextFactory = contextFactory;
            this.prodRepo = prodRepo;
            this.invRepo = invRepo;
            this.invTranRepo = invTranRepo;
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var prod = await prodRepo.GetByIdAsync(product.Id);
            foreach (var prodInv in prod.Inventories)
            {
                var inventory = await invRepo.GetByIdAsync(prodInv.InventoryId);
                // log inventory transaction
                await invTranRepo.ProduceAsync(productionNumber, inventory, prodInv.Quantity * quantity, doneBy);
                // decrease inventory quantity
                inventory.Quantity -= prodInv.Quantity * quantity;
                await invRepo.UpdateAsync(inventory);
                // update inventory in product inventory
                prodInv.Inventory = inventory;
            }
            // log product transaction
            context.ProductTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.Id,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = null,
            });
            // increase product quantity
            prod.Quantity += quantity;
            await prodRepo.UpdateAsync(prod);

        }

        public async Task<IEnumerable<ProductTransaction>> SearchProductTransactionsAsync(DateTime? startDate, DateTime? endDate, string? productName, ProductTransactionType? activityType)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var predicate = PredicateBuilder.New<ProductTransaction>(true);
            if (startDate.HasValue)
                predicate = predicate.And(pt => pt.TransactionDate >= startDate.Value);
            if (endDate.HasValue)
                predicate = predicate.And(pt => pt.TransactionDate <= endDate.Value);
            if (!string.IsNullOrWhiteSpace(productName))
                predicate = predicate.And(pt => pt.Product!.Name.Contains(productName));
            if (activityType.HasValue)
                predicate = predicate.And(pt => pt.ActivityType == activityType.Value);
            return await context.ProductTransactions.Where(predicate).Include(pt => pt.Product).ToListAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal priceToSell, string doneBy)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            context.ProductTransactions.Add(new ProductTransaction
            {
                SONumber = salesOrderNumber,
                ProductId = product.Id,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.SellProduct,
                QuantityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = product.Price,
            });
            await context.SaveChangesAsync();
        }
    }
}