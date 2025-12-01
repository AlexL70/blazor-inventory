using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Inventories.Interfaces;

namespace IMS.UseCases.Inventories
{

    public class EditInventoryUseCase : IEditInventoryUseCase
    {
        private readonly IInventoryRepository repository;

        public EditInventoryUseCase(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Inventory inventory)
        {
            await repository.UpdateAsync(inventory);
        }
    }
}