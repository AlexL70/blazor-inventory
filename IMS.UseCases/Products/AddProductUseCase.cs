using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products
{

    public class AddProductUseCase : IAddProductUseCase
    {
        private readonly IProductRepository repository;

        public AddProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await repository.AddAsync(product);
        }
    }
}