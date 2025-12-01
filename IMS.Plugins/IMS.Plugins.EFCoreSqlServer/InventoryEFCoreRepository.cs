using System.Linq.Expressions;
using IMS.CoreBusiness;
using IMS.CoreBusiness.Exceptions;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class InventoryEFCoreRepository : IInventoryRepository
    {
        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public readonly IDbContextFactory<IMSContext> contextFactory;

        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            context.Inventories.Add(inventory);
            await context.SaveChangesAsync();
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var context = await contextFactory.CreateDbContextAsync();
            var inventory = await DoGetInventoryByIdAsync(context, inventoryId);
            context.Inventories.Remove(inventory);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            var context = await contextFactory.CreateDbContextAsync();
            if (!string.IsNullOrWhiteSpace(name))
            {
                Expression<Func<Inventory, bool>> predicate = (Inventory inv) => EF.Functions.Like(inv.Name, $"%{name}%");
                return await context.Inventories.Where(predicate).ToListAsync();
            }
            else
            {
                return await context.Inventories.ToListAsync();
            }
        }

        public Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            using var context = contextFactory.CreateDbContext();
            return DoGetInventoryByIdAsync(context, inventoryId);
        }

        private async Task<Inventory> DoGetInventoryByIdAsync(IMSContext context, int inventoryId)
        {
            var inventory = await context.Inventories.FirstOrDefaultAsync(x => x.Id == inventoryId)
                ?? throw new NotFoundException(typeof(Inventory), inventoryId.ToString());
            return inventory;
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var existingInventory = await DoGetInventoryByIdAsync(context, inventory.Id);
            existingInventory.Name = inventory.Name;
            existingInventory.Quantity = inventory.Quantity;
            existingInventory.Price = inventory.Price;
            await context.SaveChangesAsync();
        }
    }
}