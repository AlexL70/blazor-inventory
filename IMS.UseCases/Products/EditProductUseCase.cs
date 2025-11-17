using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products
{

    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository repository;

        public EditProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await repository.UpdateProductAsync(product);
        }
    }
}