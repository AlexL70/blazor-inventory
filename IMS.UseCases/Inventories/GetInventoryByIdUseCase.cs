using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Inventories.Interfaces;

namespace IMS.UseCases.Inventories
{
    public class GetInventoryByIdUseCase : IGetInventoryByIdUseCase
    {
        private readonly IInventoryRepository repository;

        public GetInventoryByIdUseCase(IInventoryRepository inventoryRepository)
        {
            repository = inventoryRepository;
        }

        public async Task<Inventory> ExecuteAsync(int inventoryId)
        {
            return await repository.GetByIdAsync(inventoryId);
        }
    }
}