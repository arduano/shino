using Microsoft.EntityFrameworkCore;
using Shino.Database.Models;

namespace Shino.Database
{
    public class ShinoDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<UserServer> UserServers { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<UserActions> UserActions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ShinoDB;Trusted_Connection=True;")
                .UseLazyLoadingProxies()
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserServer>()
                .HasOne(s => s.LovePoints)
                .WithOne(l => l.UserServer)
                .HasForeignKey<Points>(l => l.UserServerId);
        }
    }
}
