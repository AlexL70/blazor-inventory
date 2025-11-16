using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Inventories.Interfaces;

namespace IMS.UseCases.Inventories
{
    public class DeleteInventoryUseCase : IDeleteInventoryUseCase
    {
        private readonly IInventoryRepository repository;

        public DeleteInventoryUseCase(IInventoryRepository inventoryRepository)
        {
            repository = inventoryRepository;
        }

        public async Task ExecuteAsync(int inventoryId)
        {
            var inventory = await repository.GetInventoryByIdAsync(inventoryId);
            if (inventory == null)
            {
                throw new ArgumentException($"Inventory with Id={inventoryId} does not exist.");
            }
            await repository.DeleteInventoryAsync(inventoryId);
        }
    }
}