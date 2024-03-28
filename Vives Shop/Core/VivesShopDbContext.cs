using Microsoft.EntityFrameworkCore;
using Vives_Shop.Models;

namespace Vives_Shop.Core
{
    public class VivesShopDbContext: DbContext
    {
        public VivesShopDbContext(DbContextOptions<VivesShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<Item> Items => Set<Item>();

        public void Seed()
        {
            Items.AddRange(new List<Item>
            {
                new Item { Name = "Medium \"French\" fries", Price = 3},
                new Item { Name = "Fricadelle", Price = 5},
                new Item { Name = "Cola Zero", Price = 2},
                new Item { Name = "Water", Price = 1.5},
                new Item { Name = "Mayonnaise", Price = 0.5}

            });

            SaveChanges();
        }
    }
}
