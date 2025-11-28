using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Plugins.EFCoreSqlServer.Configurations
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> builder)
        {
            builder.HasKey(pi => new { pi.ProductId, pi.InventoryId });

            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(pi => pi.ProductId);

            builder.HasOne(pi => pi.Inventory)
                     .WithMany(i => i.Products)
                     .HasForeignKey(pi => pi.InventoryId);
        }
    }
}