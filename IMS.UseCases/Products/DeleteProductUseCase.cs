using IMS.CoreBusiness;
using IMS.CoreBusiness.Exceptions;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository repository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public async Task ExecuteAsync(int productId)
        {
            var product = await repository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product), productId.ToString());
            }
            await repository.DeleteAsync(productId);
        }
    }
}