using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Inventories.Interfaces;

namespace IMS.UseCases.Inventories
{

    public class ViewInventoriesByNameUserCase : IViewInventoriesByNameUserCase
    {
        private readonly IInventoryRepository inventoryRepository;
        public ViewInventoriesByNameUserCase(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
        {
            return await inventoryRepository.GetByNameAsync(name);
        }
    }
}