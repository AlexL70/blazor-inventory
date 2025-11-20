using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products
{

    public class EditProductUseCase(IProductRepository repository) : IEditProductUseCase
    {
        private readonly IProductRepository repository = repository;

        public async Task ExecuteAsync(Product product)
        {
            await repository.UpdateProductAsync(product);
        }
    }
}