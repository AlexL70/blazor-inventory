using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;

namespace IMS.UseCases.Reports.Interfaces
{
    public interface ISearchInventoryTransactionsUseCase
    {
        Task<IEnumerable<InventoryTransaction>> ExecuteAsync(DateTime? startDate, DateTime? endDate, string? inventoryName, InventoryTransactionType? activityType);
    }
}