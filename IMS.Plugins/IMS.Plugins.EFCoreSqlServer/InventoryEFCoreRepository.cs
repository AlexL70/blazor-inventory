using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using IMS.Plugins.EFCoreSqlServer.Base;

namespace IMS.Plugins.EFCoreSqlServer
{

    public class InventoryEFCoreRepository : BaseEFCoreRepository<Inventory>, IInventoryRepository
    {
        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
            : base(contextFactory, UpdateInventoryEntity, (entity, searchString) => entity.Name.Contains(searchString))
        {
        }

        private static void UpdateInventoryEntity(ref Inventory existingEntity, Inventory newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.Quantity = newEntity.Quantity;
            existingEntity.Price = newEntity.Price;
        }
    }
}