using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository(IProductRepository prodRepo, IInventoryRepository invRepo, IInventoryTransactionRepository invTranRepo) : IProductTransactionRepository
    {
        private IList<ProductTransaction> productTransactions { get; } = new List<ProductTransaction>();
        private IProductRepository prodRepo { get; } = prodRepo;
        private IInventoryRepository invRepo { get; } = invRepo;
        private IInventoryTransactionRepository invTranRepo { get; } = invTranRepo;

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            var prod = await prodRepo.GetProductByIdAsync(product.Id);
            foreach (var prodInv in prod.Inventories)
            {
                var inventory = await invRepo.GetInventoryByIdAsync(prodInv.InventoryId);
                // log inventory transaction
                await invTranRepo.ProduceAsync(productionNumber, inventory, prodInv.Quantity * quantity, doneBy);
                // decrease inventory quantity
                inventory.Quantity -= prodInv.Quantity * quantity;
                await invRepo.UpdateInventoryAsync(inventory);
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
            await prodRepo.UpdateProductAsync(prod);
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
