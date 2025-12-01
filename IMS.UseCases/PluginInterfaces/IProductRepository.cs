using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByNameAsync(string name = "");
        Task AddAsync(Product Product);
        Task DeleteAsync(int ProductId);
        Task<Product> GetByIdAsync(int ProductId);
        Task UpdateAsync(Product Product);
    }
}