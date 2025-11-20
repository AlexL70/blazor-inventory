using IMS.CoreBusiness;
using IMS.CoreBusiness.Exceptions;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products = new List<Product>();

        public ProductRepository()
        {
            // Populate with some initial data
            products.Add(new Product { Id = 1, Name = "Toyota Camry", Quantity = 15, Price = 25000 });
            products.Add(new Product { Id = 2, Name = "Honda Accord", Quantity = 12, Price = 27000 });
            products.Add(new Product { Id = 3, Name = "Ford Mustang", Quantity = 8, Price = 35000 });
            products.Add(new Product { Id = 4, Name = "BMW 3 Series", Quantity = 10, Price = 42000 });
            products.Add(new Product { Id = 5, Name = "Tesla Model 3", Quantity = 20, Price = 45000 });
            products.Add(new Product { Id = 6, Name = "Mercedes-Benz C-Class", Quantity = 9, Price = 48000 });
            products.Add(new Product { Id = 7, Name = "Audi A4", Quantity = 11, Price = 44000 });
            products.Add(new Product { Id = 8, Name = "Trek Mountain Bike", Quantity = 25, Price = 800 });
            products.Add(new Product { Id = 9, Name = "Giant Road Bike", Quantity = 18, Price = 1200 });
            products.Add(new Product { Id = 10, Name = "Specialized Hybrid Bike", Quantity = 22, Price = 950 });
            products.Add(new Product { Id = 11, Name = "Cannondale Trail Bike", Quantity = 15, Price = 1100 });
            products.Add(new Product { Id = 12, Name = "Scott Electric Bike", Quantity = 12, Price = 3500 });
            products.Add(new Product { Id = 13, Name = "Harley-Davidson Street 750", Quantity = 7, Price = 7500 });
            products.Add(new Product { Id = 14, Name = "Yamaha YZF-R3", Quantity = 10, Price = 5300 });
            products.Add(new Product { Id = 15, Name = "Kawasaki Ninja 400", Quantity = 9, Price = 5200 });
            products.Add(new Product { Id = 16, Name = "Honda CBR500R", Quantity = 8, Price = 6800 });
            products.Add(new Product { Id = 17, Name = "Ducati Monster 821", Quantity = 5, Price = 11500 });
            products.Add(new Product { Id = 18, Name = "BMW R1250GS", Quantity = 6, Price = 17500 });
            products.Add(new Product { Id = 19, Name = "Suzuki GSX-R750", Quantity = 7, Price = 12000 });
            products.Add(new Product { Id = 20, Name = "KTM 390 Duke", Quantity = 11, Price = 5500 });
        }

        public Task AddProductAsync(Product Product)
        {
            if (Product.Id != 0 && products.Any(i => i.Id == Product.Id))
            {
                throw new ArgumentException($"An Product item with the same Id={Product.Id} already exists.");
            }
            Product.Id = Product.Id == 0
                ? (products.Count > 0
                    ? products.Max(i => i.Id) + 1
                    : 1)
                : Product.Id;
            if (products.Any(i => i.Name.Equals(Product.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"An Product item with the same Name='{Product.Name}' already exists.");
            }
            products.Add(Product);
            return Task.CompletedTask;
        }

        public async Task DeleteProductAsync(int ProductId)
        {
            var Product = await GetProductByIdAsync(ProductId);
            products.Remove(Product);
        }

        public Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult(products.AsEnumerable());
            }
            var result = products.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(result);
        }

        public Task<Product> GetProductByIdAsync(int ProductId)
        {
            var product = products.FirstOrDefault(i => i.Id == ProductId);
            if (product == null)
            {
                throw new NotFoundException(typeof(Product), ProductId.ToString());
            }
            return Task.FromResult(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                Inventories = [.. product.Inventories.Select(pi => new ProductInventory
                {
                    ProductId = pi.ProductId,
                    InventoryId = pi.InventoryId,
                    Quantity = pi.Quantity,
                    Product = product,
                    Inventory = pi.Inventory == null
                        ? new Inventory() { Id = pi.InventoryId }
                        : new Inventory
                        {
                            Id = pi.InventoryId,
                            Name = pi.Inventory.Name,
                            Quantity = pi.Inventory.Quantity,
                            Price = pi.Inventory.Price
                        }
                })]
            });
        }

        public Task UpdateProductAsync(Product Product)
        {
            var invToUpdate = products.FirstOrDefault(i => i.Id == Product.Id);
            if (invToUpdate == null)
            {
                throw new NotFoundException(typeof(Product), Product.Id.ToString());
            }
            if (products.Any(i => i.Id != Product.Id && i.Name.Equals(Product.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"An Product item with the same Name='{Product.Name}' already exists.");
            }
            invToUpdate.Name = Product.Name;
            invToUpdate.Quantity = Product.Quantity;
            invToUpdate.Price = Product.Price;
            invToUpdate.Inventories = Product.Inventories;
            return Task.CompletedTask;
        }
    }
}