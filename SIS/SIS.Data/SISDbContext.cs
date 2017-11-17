namespace SIS.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SISDbContext : DbContext
    {
        public SISDbContext()
            : base("name=SISDbContext")
        {
        }

        public virtual DbSet<Department_LUT> Department_LUT { get; set; }
        public virtual DbSet<Item_Location> Item_Location { get; set; }
        public virtual DbSet<Item_Master> Item_Master { get; set; }
        public virtual DbSet<Location_LUT> Location_LUT { get; set; }
        public virtual DbSet<Master_Transactions> Master_Transactions { get; set; }
        public virtual DbSet<Supplier_LUT> Supplier_LUT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department_LUT>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<Department_LUT>()
                .Property(e => e.C_Dept_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Location>()
                .Property(e => e.Item_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Location>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Item_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Item_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Item_Master>()
                .HasMany(e => e.Item_Location)
                .WithRequired(e => e.Item_Master)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item_Master>()
                .HasMany(e => e.Master_Transactions)
                .WithRequired(e => e.Item_Master)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location_LUT>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Location_LUT>()
                .Property(e => e.Loc_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Location_LUT>()
                .Property(e => e.Old_Loc)
                .IsUnicode(false);

            modelBuilder.Entity<Master_Transactions>()
                .Property(e => e.Item_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Master_Transactions>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<Master_Transactions>()
                .Property(e => e.Item_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Master_Transactions>()
                .Property(e => e.Transaction_Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.SupplierID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.Supplier)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_LUT>()
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}
