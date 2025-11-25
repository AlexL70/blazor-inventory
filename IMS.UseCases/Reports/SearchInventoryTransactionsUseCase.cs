using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.Interfaces;

namespace IMS.UseCases.Reports
{

    public class SearchInventoryTransactionsUseCase(IInventoryTransactionRepository invTranRepo) : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTransactionRepository invTranRepo = invTranRepo;

        public Task<IEnumerable<InventoryTransaction>> ExecuteAsync(DateTime? startDate, DateTime? endDate, string? inventoryName, InventoryTransactionType? activityType)
        {
            if (startDate.HasValue)
                startDate = startDate.Value.Date;
            if (endDate.HasValue)
                endDate = endDate.Value.Date.AddDays(1).AddTicks(-1);
            return invTranRepo.SearchInventoryTransactionsAsync(startDate, endDate, inventoryName, activityType);
        }
    }
}