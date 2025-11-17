using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products.Interfaces;

namespace IMS.UseCases.Products
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository repository;

        public GetProductByIdUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Product> ExecuteAsync(int productId)
        {
            return await repository.GetProductByIdAsync(productId);
        }
    }
}