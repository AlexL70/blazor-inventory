using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Activities.Interfaces;

namespace IMS.UseCases.Activities
{

    public class PurchaseInventoryUseCase(IInventoryTransactionRepository tranRepo, IInventoryRepository invRepo) : IPurchaseInventoryUseCase
    {
        IInventoryTransactionRepository tranRepo = tranRepo;
        IInventoryRepository invRepo = invRepo;

        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
        {
            // insert a record into Transaction table
            await tranRepo.PurchaseAsync(poNumber, inventory, quantity, doneBy, inventory.Price);
            // increase the inventory quantity
            inventory.Quantity += quantity;
            await invRepo.UpdateInventoryAsync(inventory);
        }
    }
}