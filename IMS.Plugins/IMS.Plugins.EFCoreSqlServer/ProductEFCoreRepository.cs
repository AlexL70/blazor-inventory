using IMS.CoreBusiness;
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

        private static void UpdateProductEntity(ref Product existingEntity, Product newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.Quantity = newEntity.Quantity;
            existingEntity.Price = newEntity.Price;
            existingEntity.Inventories = newEntity.Inventories;
        }

        private void MarkInventoryUnchanged(Product product, IMSContext context)
        {
            foreach (var inventory in product.Inventories)
            {
                if (inventory.Inventory != null)
                    context.Entry(inventory.Inventory).State = EntityState.Unchanged;
            }
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
            // Avoid looping updating for Inventories/Products
            MarkInventoryUnchanged(changedEntity, context);
        }
    }
}