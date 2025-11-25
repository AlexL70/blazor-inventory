using IMS.CoreBusiness;
using IMS.CoreBusiness.Enums;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.Interfaces;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionsUseCase(IProductTransactionRepository prodTranRepo) : ISearchProductTransactionsUseCase
    {
        private readonly IProductTransactionRepository prodTranRepo = prodTranRepo;

        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(DateTime? startDate, DateTime? endDate, string? productName, ProductTransactionType? activityType)
        {
            if (startDate.HasValue)
                startDate = startDate.Value.Date;
            if (endDate.HasValue)
                endDate = endDate.Value.Date.AddDays(1).AddTicks(-1);
            return await prodTranRepo.SearchProductTransactionsAsync(startDate, endDate, productName, activityType);
        }
    }
}