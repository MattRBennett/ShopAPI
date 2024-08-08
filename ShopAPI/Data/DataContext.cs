using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        
        }

        public DbSet<Item> items => Set<Item>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Cart> Carts => Set<Cart>();

    }
}
