using IMS.CoreBusiness;
using IMS.CoreBusiness.Constants;
using IMS.Plugins.EFCoreSqlServer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class IMSContext(DbContextOptions<IMSContext> options) : IdentityDbContext<ApplicationUser>(options)
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
            {
                SeedUsers(modelBuilder);
                SeedData(modelBuilder);
            }
        }

        internal class UserData
        {
            public string UserName { get; set; } = string.Empty;
            public string UserId { get; set; } = string.Empty;
            public int ClaimId { get; set; } = 0;
            public string SecurityStamp { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string ClaimValue { get; set; } = string.Empty;
            public string ConcurrencyStamp { get; set; } = string.Empty;
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            List<UserData> users = [
                new UserData {
                    UserName = Policies.Admin,
                    UserId = "614dcc9d-6c37-4c4d-a882-c460b8a98fbe",
                    ClaimId = 1,
                    SecurityStamp = "f39e617d-0482-4b8a-af2e-ab4e23c8b195",
                    PasswordHash = "AQAAAAIAAYagAAAAECvM5DqYyqm7Yf+NdNzjKZb+1Jy7poambcDNEhj/391IQpLHvnulGRZqCay9hkhtoQ==",
                    ClaimValue = Departments.Administration,
                    ConcurrencyStamp = "5cd3d9e1-44a5-461d-9495-d53286627d4e"
                },
                new UserData {
                    UserName = Policies.Inventory,
                    UserId = "d3b3f4e1-8f4e-4c2a-9f7a-2e5d6c3b4a1e",
                    ClaimId = 2,
                    SecurityStamp = "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
                    PasswordHash = "AQAAAAIAAYagAAAAED/hj1mD5n14oIrf2ZrjH3ZfCuqCAMbQ4DnA7QhOAEOI1ycEOhkbDbgpOQ4nRoyt+g==",
                    ClaimValue = Departments.InventoryManagement,
                    ConcurrencyStamp = "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"
                },
                new UserData {
                    UserName = Policies.Sales,
                    UserId = "e2a1f5b6-7c8d-4e9f-9a0b-1c2d3e4f5a6b",
                    ClaimId = 3,
                    SecurityStamp = "b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e",
                    PasswordHash = "AQAAAAIAAYagAAAAEOhX5gDo0z4ReTvAnW5N8FQ/xSICxrZMBg698iIvE+66noY/KAU/X6/X3O5EOOD5Og==",
                    ClaimValue = Departments.Sales,
                    ConcurrencyStamp = "b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"
                },
                new UserData {
                    UserName = Policies.Purchasers,
                    UserId = "f4c5d6e7-8f9a-4b0c-9d1e-2f3a4b5c6d7e",
                    ClaimId = 4,
                    SecurityStamp = "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f",
                    PasswordHash = "AQAAAAIAAYagAAAAEGK/TaMWksteFtTCkLBwodJsd6MzhaOJr+QBO50XrUx3h7GzKU8sEStg02nh3ApG0w==",
                    ClaimValue = Departments.Purchasing,
                    ConcurrencyStamp = "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"
                },
                new UserData {
                    UserName = Policies.Productions,
                    UserId = "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
                    ClaimId = 5,
                    SecurityStamp = "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a",
                    PasswordHash = "AQAAAAIAAYagAAAAEIbzA6udlFefLboNrU7zEOPp8z/kTrHUdJOoYvznIuLOwhQUL8HNn3BnIvbPnBemgw==",
                    ClaimValue = Departments.ProductionManagement,
                    ConcurrencyStamp = "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"
                }
            ];

            const string emailSuffix = "@imsmail.com";
            //const string defaultPassword = "P@ssw0rd!";
            //var hasher = new PasswordHasher<ApplicationUser>();
            foreach (var user in users)
            {
                // Seed users
                var userEntity = new ApplicationUser
                {
                    Id = user.UserId,
                    UserName = $"{user.UserName.ToLower()}{emailSuffix}",
                    NormalizedUserName = $"{user.UserName.ToUpper()}{emailSuffix}",
                    Email = $"{user.UserName.ToLower()}{emailSuffix}",
                    NormalizedEmail = $"{user.UserName.ToUpper()}{emailSuffix}",
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    EmailConfirmed = true,
                    ConcurrencyStamp = user.ConcurrencyStamp
                };
                // Console.WriteLine($"Seeding user: {userEntity.UserName} with hash: {hasher.HashPassword(userEntity, defaultPassword)}");
                modelBuilder.Entity<ApplicationUser>().HasData(userEntity);

                // Seed claims for user
                modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                            new IdentityUserClaim<string>
                            {
                                Id = user.ClaimId,
                                UserId = user.UserId,
                                ClaimType = ImsClaimTypes.Department,
                                ClaimValue = user.ClaimValue
                            }
                        );
            }
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