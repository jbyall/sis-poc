using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    // Represents the database context in memory
    public class SisDbContext : DbContext
    {
        public SisDbContext()
            : base("name=SisDbContext")
        {
            // Disable EF proxies. See https://msdn.microsoft.com/en-us/library/jj592886%28v=vs.113%29.aspx?f=255&MSPPError=-2147217396
            this.Configuration.ProxyCreationEnabled = false;
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SIS");
            base.OnModelCreating(modelBuilder);

        }
        // Defines the tables in the database that will be used in-memory
        public DbSet<Department> Departments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLocation> ItemLocations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
