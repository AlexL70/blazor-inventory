using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Activities.Interfaces;

namespace IMS.UseCases.Activities
{

    public class SellProductUseCase(IProductTransactionRepository tranRepo, IProductRepository prodRepo) : ISellProductUseCase
    {
        private IProductTransactionRepository tranRepo { get; } = tranRepo;
        private IProductRepository prodRepo { get; } = prodRepo;

        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
        {
            await tranRepo.SellProductAsync(salesOrderNumber, product, quantity, doneBy);
            product.Quantity -= quantity;
            await prodRepo.UpdateProductAsync(product);
        }
    }
}