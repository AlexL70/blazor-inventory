using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IList<ProductTransaction> productTransactions;
        private readonly IProductRepository prodRepo;
        private readonly IInventoryRepository invRepo;
        private readonly IInventoryTransactionRepository invTranRepo;

        public ProductTransactionRepository(IProductRepository prodRepo, IInventoryRepository invRepo, IInventoryTransactionRepository invTranRepo)
        {
            this.prodRepo = prodRepo;
            this.invRepo = invRepo;
            this.invTranRepo = invTranRepo;
            productTransactions = new List<ProductTransaction>();

            // Seed one transaction per product seeded in ProductRepository
            var products = this.prodRepo.GetByNameAsync(string.Empty).GetAwaiter().GetResult().ToList();
            var rng = new Random();
            var today = DateTime.Today;
            var start = today.AddMonths(-1);
            var daySpan = (today - start).Days;
            var id = 1;

            foreach (var p in products)
            {
                var isSell = rng.Next(2) == 0; // 50/50
                var delta = Math.Max(1, Math.Min(5, Math.Max(1, p.Quantity / 4)));
                var txDate = start.AddDays(rng.Next(daySpan + 1))
                                  .AddHours(rng.Next(0, 24))
                                  .AddMinutes(rng.Next(0, 60));

                var tx = new ProductTransaction
                {
                    Id = id++,
                    SONumber = isSell ? $"SO-{1000 + p.Id}" : string.Empty,
                    ProductionNumber = isSell ? string.Empty : $"PRD-{1000 + p.Id}",
                    ProductId = p.Id,
                    QuantityBefore = isSell ? p.Quantity + delta : Math.Max(0, p.Quantity - delta),
                    ActivityType = isSell ? ProductTransactionType.SellProduct : ProductTransactionType.ProduceProduct,
                    QuantityAfter = p.Quantity,
                    UnitPrice = isSell ? p.Price : null, // selling only
                    TransactionDate = txDate,
                    DoneBy = "System",
                    // Product left null; it will be populated during queries
                };

                productTransactions.Add(tx);
            }
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
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
            productTransactions.Add(new ProductTransaction
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
            var products = await prodRepo.GetByNameAsync(productName ?? string.Empty);
            var inventories = await invRepo.GetByNameAsync(string.Empty);
            var query = productTransactions.Where(pt =>
                (!startDate.HasValue || pt.TransactionDate >= startDate.Value) &&
                (!endDate.HasValue || pt.TransactionDate <= endDate.Value) &&
                (string.IsNullOrWhiteSpace(productName) || prodRepo.GetByIdAsync(pt.ProductId).Result.Name.Contains(productName, StringComparison.OrdinalIgnoreCase)) &&
                (!activityType.HasValue || pt.ActivityType == activityType.Value)
            ).Join(products, pt => pt.ProductId, p => p.Id, (pt, p) => pt)
            .Select(pt =>
            {
                var prod = products.First(p => p.Id == pt.ProductId);
                pt.Product = prod;
                foreach (var prodInv in prod.Inventories)
                {
                    prodInv.Inventory = inventories.First(i => i.Id == prodInv.InventoryId);
                }
                return pt;
            });
            return query;
        }

        public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal priceToSell, string doneBy)
        {
            productTransactions.Add(new ProductTransaction
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
            return Task.CompletedTask;
        }
    }
}
