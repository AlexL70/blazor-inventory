using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Plugins.EFCoreSqlServer.Configurations
{
    public class ProductTransactionConfiguration : IEntityTypeConfiguration<ProductTransaction>
    {
        public void Configure(EntityTypeBuilder<ProductTransaction> builder)
        {
            builder.Property(pt => pt.UnitPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}