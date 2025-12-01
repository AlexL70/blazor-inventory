using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Inventories.Interfaces;

namespace IMS.UseCases.Inventories
{

    public class AddInventoryUseCase : IAddInventoryUseCase
    {
        private readonly IInventoryRepository repository;

        public AddInventoryUseCase(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Inventory inventory)
        {
            await repository.AddAsync(inventory);
        }
    }
}