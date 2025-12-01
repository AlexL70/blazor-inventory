using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.CoreBusiness;

namespace IMS.UseCases.Products
{

    public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
    {
        private readonly IProductRepository repository;

        public ViewProductsByNameUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
        {
            return await repository.GetByNameAsync(name);
        }
    }
}