using KGM.Framework.Domain.Model.U8;
using KGM.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{
    public class U8Context : DbContext
    {
        public DbSet<U8Entity.WarehouseEntity> Warehouse { get; set; }
        public DbSet<U8Entity.InventorysEntity> Inventorys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<U8Entity.WarehouseEntity>()
                .HasKey(c => c.CWhCode);
            modelBuilder.Entity<U8Entity.InventorysEntity>()
                .HasKey(c => c.cInvCode);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = JSonConfigReader.ReadFile("Config/U8Database.json");
            optionsBuilder.UseSqlServer(config[$"ConnectionStrings:{config["ConnectionStrings:DbType"]}Connection"]);
        }
    }
}
