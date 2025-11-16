using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly List<Inventory> _inventories = new List<Inventory>();

        public InventoryRepository()
        {
            // Populate with some initial data
            _inventories.Add(new Inventory { Id = 1, Name = "Frame", Quantity = 10, Price = 150.00m });
            _inventories.Add(new Inventory { Id = 2, Name = "Front Wheel", Quantity = 20, Price = 45.00m });
            _inventories.Add(new Inventory { Id = 3, Name = "Rear Wheel", Quantity = 20, Price = 45.00m });
            _inventories.Add(new Inventory { Id = 4, Name = "Handlebars", Quantity = 15, Price = 25.00m });
            _inventories.Add(new Inventory { Id = 5, Name = "Seat", Quantity = 12, Price = 30.00m });
            _inventories.Add(new Inventory { Id = 6, Name = "Pedals", Quantity = 25, Price = 20.00m });
            _inventories.Add(new Inventory { Id = 7, Name = "Chain", Quantity = 18, Price = 15.00m });
            _inventories.Add(new Inventory { Id = 8, Name = "Brakes (Front)", Quantity = 22, Price = 35.00m });
            _inventories.Add(new Inventory { Id = 9, Name = "Brakes (Rear)", Quantity = 22, Price = 35.00m });
            _inventories.Add(new Inventory { Id = 10, Name = "Gear Shifter", Quantity = 14, Price = 40.00m });
            _inventories.Add(new Inventory { Id = 11, Name = "Fork", Quantity = 11, Price = 60.00m });
            _inventories.Add(new Inventory { Id = 12, Name = "Crankset", Quantity = 16, Price = 55.00m });
            _inventories.Add(new Inventory { Id = 13, Name = "Cassette", Quantity = 19, Price = 50.00m });
            _inventories.Add(new Inventory { Id = 14, Name = "Derailleur", Quantity = 13, Price = 45.00m });
            _inventories.Add(new Inventory { Id = 15, Name = "Tire (Front)", Quantity = 30, Price = 22.00m });
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            if (inventory.Id != 0 && _inventories.Any(i => i.Id == inventory.Id))
            {
                throw new ArgumentException($"An inventory item with the same Id={inventory.Id} already exists.");
            }
            inventory.Id = inventory.Id == 0
                ? (_inventories.Count > 0
                    ? _inventories.Max(i => i.Id) + 1
                    : 1)
                : inventory.Id;
            if (_inventories.Any(i => i.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"An inventory item with the same Name='{inventory.Name}' already exists.");
            }
            _inventories.Add(inventory);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult(_inventories.AsEnumerable());
            }
            var result = _inventories.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(result);
        }

        public Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            var inventory = _inventories.FirstOrDefault(i => i.Id == inventoryId);
            if (inventory == null)
            {
                throw new ArgumentException($"Inventory with Id={inventoryId} not found.");
            }
            return Task.FromResult(inventory);
        }

        public Task UpdateInventoryAsync(Inventory inventory)
        {
            var invToUpdate = _inventories.FirstOrDefault(i => i.Id == inventory.Id);
            if (invToUpdate == null)
            {
                throw new ArgumentException($"Inventory with Id={inventory.Id} not found.");
            }
            if (_inventories.Any(i => i.Id != inventory.Id && i.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"An inventory item with the same Name='{inventory.Name}' already exists.");
            }
            invToUpdate.Name = inventory.Name;
            invToUpdate.Quantity = inventory.Quantity;
            invToUpdate.Price = inventory.Price;
            return Task.CompletedTask;
        }
    }
}