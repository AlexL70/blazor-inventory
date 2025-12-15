using IMS.CoreBusiness;
using IMS.Plugins.EFCoreSqlServer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class IMSContext(DbContextOptions<IMSContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DefineConfigurations(modelBuilder);
            // Seed initial data (for development mode only)
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Mountain Trail Pro", Quantity = 0, Price = 1299.99m },
                new Product { Id = 2, Name = "City Commuter Elite", Quantity = 0, Price = 899.99m },
                new Product { Id = 3, Name = "Road Racer X500", Quantity = 0, Price = 1899.99m },
                new Product { Id = 4, Name = "Electric Cruiser Plus", Quantity = 0, Price = 2499.99m },
                new Product { Id = 5, Name = "Kids Explorer 20", Quantity = 0, Price = 549.99m }
            );

            // Seed Inventories
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, Name = "Aluminum Frame", Quantity = 50, Price = 250.00m },
                new Inventory { Id = 2, Name = "Carbon Frame", Quantity = 30, Price = 600.00m },
                new Inventory { Id = 3, Name = "Wheel Set 30\"", Quantity = 100, Price = 150.00m },
                new Inventory { Id = 4, Name = "Wheel 20\"", Quantity = 120, Price = 40.00m },
                new Inventory { Id = 5, Name = "Wheel 26\"", Quantity = 150, Price = 75.00m },
                new Inventory { Id = 6, Name = "Disc Brake Set", Quantity = 80, Price = 120.00m },
                new Inventory { Id = 7, Name = "Electric Motor Kit", Quantity = 25, Price = 800.00m },
                new Inventory { Id = 8, Name = "Gear System 21-Speed", Quantity = 70, Price = 180.00m },
                new Inventory { Id = 9, Name = "Gear System 7-Speed", Quantity = 90, Price = 80.00m },
                new Inventory { Id = 10, Name = "Suspension Fork", Quantity = 45, Price = 200.00m },
                new Inventory { Id = 11, Name = "Battery Pack", Quantity = 30, Price = 400.00m },
                new Inventory { Id = 12, Name = "Pedals", Quantity = 200, Price = 35.00m }
            );

            // Seed ProductInventories
            modelBuilder.Entity<ProductInventory>().HasData(
                // Mountain Trail Pro - uses wheel set
                new ProductInventory { ProductId = 1, InventoryId = 1, Quantity = 1 }, // Aluminum Frame
                new ProductInventory { ProductId = 1, InventoryId = 3, Quantity = 1 }, // Wheel Set 30"
                new ProductInventory { ProductId = 1, InventoryId = 6, Quantity = 1 }, // Disc Brake Set
                new ProductInventory { ProductId = 1, InventoryId = 8, Quantity = 1 }, // 21-Speed Gear
                new ProductInventory { ProductId = 1, InventoryId = 10, Quantity = 1 }, // Suspension Fork
                new ProductInventory { ProductId = 1, InventoryId = 12, Quantity = 2 }, // Pedals (qty: 2)

                // City Commuter Elite - uses 2 individual wheels
                new ProductInventory { ProductId = 2, InventoryId = 1, Quantity = 1 }, // Aluminum Frame
                new ProductInventory { ProductId = 2, InventoryId = 5, Quantity = 2 }, // Wheel 26" (qty: 2)
                new ProductInventory { ProductId = 2, InventoryId = 6, Quantity = 1 }, // Disc Brake Set
                new ProductInventory { ProductId = 2, InventoryId = 9, Quantity = 1 }, // 7-Speed Gear
                new ProductInventory { ProductId = 2, InventoryId = 12, Quantity = 2 }, // Pedals (qty: 2)

                // Road Racer X500 - uses wheel set
                new ProductInventory { ProductId = 3, InventoryId = 2, Quantity = 1 }, // Carbon Frame
                new ProductInventory { ProductId = 3, InventoryId = 3, Quantity = 1 }, // Wheel Set 30"
                new ProductInventory { ProductId = 3, InventoryId = 6, Quantity = 1 }, // Disc Brake Set
                new ProductInventory { ProductId = 3, InventoryId = 8, Quantity = 1 }, // 21-Speed Gear
                new ProductInventory { ProductId = 3, InventoryId = 12, Quantity = 2 }, // Pedals (qty: 2)

                // Electric Cruiser Plus - uses 2 individual wheels
                new ProductInventory { ProductId = 4, InventoryId = 1, Quantity = 1 }, // Aluminum Frame
                new ProductInventory { ProductId = 4, InventoryId = 5, Quantity = 2 }, // Wheel 26" (qty: 2)
                new ProductInventory { ProductId = 4, InventoryId = 6, Quantity = 1 }, // Disc Brake Set
                new ProductInventory { ProductId = 4, InventoryId = 7, Quantity = 1 }, // Electric Motor
                new ProductInventory { ProductId = 4, InventoryId = 9, Quantity = 1 }, // 7-Speed Gear
                new ProductInventory { ProductId = 4, InventoryId = 11, Quantity = 1 }, // Battery Pack
                new ProductInventory { ProductId = 4, InventoryId = 12, Quantity = 2 }, // Pedals (qty: 2)

                // Kids Explorer 20 - uses 2 individual wheels
                new ProductInventory { ProductId = 5, InventoryId = 1, Quantity = 1 }, // Aluminum Frame
                new ProductInventory { ProductId = 5, InventoryId = 4, Quantity = 2 }, // Wheel 20" (qty: 2)
                new ProductInventory { ProductId = 5, InventoryId = 9, Quantity = 1 }, // 7-Speed Gear
                new ProductInventory { ProductId = 5, InventoryId = 12, Quantity = 2 }  // Pedals (qty: 2)
            );
        }

        protected virtual void DefineConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInventoryConfiguration());
        }
    }
}