using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
        Task<IEnumerable<ProductTransaction>> SearchProductTransactionsAsync(DateTime? startDate, DateTime? endDate, string? productName, ProductTransactionType? activityType);
        Task SellProductAsync(string salesOrderNumber, Product product, int quantity, decimal priceToSell, string doneBy);
    }
}