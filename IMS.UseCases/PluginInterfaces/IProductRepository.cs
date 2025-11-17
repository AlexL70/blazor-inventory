using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name = "");
        Task AddProductAsync(Product Product);
        Task DeleteProductAsync(int ProductId);
        Task<Product> GetProductByIdAsync(int ProductId);
        Task UpdateProductAsync(Product Product);
    }
}