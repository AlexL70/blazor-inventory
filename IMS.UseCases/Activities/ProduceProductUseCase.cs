using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities
{
    public class ProduceProductUseCase(IProductTransactionRepository transactionRepository, IProductRepository productRepository) : IProduceProductUseCase
    {
        public IProductTransactionRepository TransactionRepository { get; } = transactionRepository;
        public IProductRepository ProductRepository { get; } = productRepository;

        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            // add transaction record and decrease the quantity of inventories used
            await TransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);
        }
    }
}
