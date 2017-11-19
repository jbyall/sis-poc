namespace SIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department_LUT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNumber = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Qty_OnHand_Dist = c.Int(),
                        Qty_OnHand_Store = c.Int(),
                        Qty_OnHand_Sub = c.Int(),
                        Qty_Change_Dist = c.Int(nullable: false),
                        Qty_Change_Stor = c.Int(nullable: false),
                        Qty_Change_Sub = c.Int(nullable: false),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department_LUT", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemNumber)
                .Index(t => t.ItemNumber)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemNumber = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 70),
                        Unit = c.String(nullable: false, maxLength: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        ReorderPoint = c.Int(),
                        SupplierId = c.String(nullable: false, maxLength: 10),
                        Comment = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ItemNumber)
                .ForeignKey("dbo.Supplier_LUT", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.ItemLocations",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        LocationId = c.String(nullable: false, maxLength: 8),
                        QuantityOnHand = c.Int(),
                    })
                .PrimaryKey(t => new { t.ItemId, t.LocationId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Location_LUT", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Location_LUT",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 8),
                        Type = c.String(maxLength: 12),
                        OldLocation = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplier_LUT",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 70),
                        Address2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(nullable: false, maxLength: 2),
                        Zip = c.String(nullable: false, maxLength: 10),
                        Comment = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ItemNumber", "dbo.Items");
            DropForeignKey("dbo.Items", "SupplierId", "dbo.Supplier_LUT");
            DropForeignKey("dbo.ItemLocations", "LocationId", "dbo.Location_LUT");
            DropForeignKey("dbo.ItemLocations", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Transactions", "DepartmentId", "dbo.Department_LUT");
            DropIndex("dbo.ItemLocations", new[] { "LocationId" });
            DropIndex("dbo.ItemLocations", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "SupplierId" });
            DropIndex("dbo.Transactions", new[] { "DepartmentId" });
            DropIndex("dbo.Transactions", new[] { "ItemNumber" });
            DropTable("dbo.Supplier_LUT");
            DropTable("dbo.Location_LUT");
            DropTable("dbo.ItemLocations");
            DropTable("dbo.Items");
            DropTable("dbo.Transactions");
            DropTable("dbo.Department_LUT");
        }
    }
}
