using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryTransactionRepository
    {
        Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price);
        Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy);
        Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(DateTime? startDate, DateTime? endDate, string? inventoryName, InventoryTransactionType? activityType);
    }
}