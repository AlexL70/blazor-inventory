using IMS.CoreBusiness;
using IMS.CoreBusiness.Exceptions;
using IMS.Plugins.EFCoreSqlServer.Base;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ProductEFCoreRepository : BaseEFCoreRepository<Product>, IProductRepository
    {
        public ProductEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
            : base(contextFactory, UpdateProductEntity, (entity, searchString) => entity.Name.Contains(searchString))
        {
        }

        private static void UpdateProductEntity(ref Product existingEntity, Product changedEntity)
        {
            existingEntity.Name = changedEntity.Name;
            existingEntity.Quantity = changedEntity.Quantity;
            existingEntity.Price = changedEntity.Price;
            // Update existing inventories
            foreach (var inv in existingEntity.Inventories.Where(i => changedEntity.Inventories.Any(ni => ni.InventoryId == i.InventoryId)))
            {
                var changedInv = changedEntity.Inventories.First(i => i.InventoryId == inv.InventoryId);
                inv.Quantity = changedInv.Quantity;
            }
            // Add new inventories
            var existingInventoryIds = existingEntity.Inventories.Select(i => i.InventoryId).ToHashSet();
            foreach (var newInv in changedEntity.Inventories.Where(i => !existingInventoryIds.Contains(i.InventoryId)))
            {
                existingEntity.Inventories.Add(newInv);
            }
            // Remove deleted inventories
            var idsToRemove = existingEntity.Inventories
                .Where(i => !changedEntity.Inventories.Any(ni => ni.InventoryId == i.InventoryId))
                .Select(i => i.InventoryId)
                .ToList();
            foreach (var id in idsToRemove)
            {
                var invToRemove = existingEntity.Inventories.First(i => i.InventoryId == id);
                existingEntity.Inventories.Remove(invToRemove);
            }
        }

        private void MarkInventoryUnchanged(Product product, IMSContext context)
        {
            foreach (var inventory in product.Inventories)
            {
                if (inventory.Inventory != null)
                    context.Entry(inventory.Inventory).State = EntityState.Unchanged;
            }
        }

        protected override async Task<Product> DoGetByIdAsync(IMSContext context, int Id)
        {
            var product = await context.Products
                .Include(p => p.Inventories)
                .ThenInclude(pi => pi.Inventory)
                .FirstOrDefaultAsync(p => p.Id == Id) ?? throw new NotFoundException(typeof(Product), Id.ToString());

            return product;
        }

        protected override void DoAdd(Product entity, IMSContext context)
        {
            base.DoAdd(entity, context);
            // Avoid looping updating for Inventories/Products
            MarkInventoryUnchanged(entity, context);
        }

        protected override void DoUpdate(Product existingEntity, Product changedEntity, IMSContext context)
        {
            base.DoUpdate(existingEntity, changedEntity, context);
        }
    }
}