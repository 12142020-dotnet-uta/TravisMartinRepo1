using Microsoft.EntityFrameworkCore;

namespace TravisMartin_Project0
{
    public class GameStopDBContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Inventory> inventory { get; set; }
        public DbSet<StoreLocation> storeLocations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GameStopDB;Trusted_Connection=True;");
        }
    }
}