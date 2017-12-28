namespace SIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchemaChange : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Department_LUT", newSchema: "SIS");
            MoveTable(name: "dbo.Transactions", newSchema: "SIS");
            MoveTable(name: "dbo.Items", newSchema: "SIS");
            MoveTable(name: "dbo.ItemLocations", newSchema: "SIS");
            MoveTable(name: "dbo.Location_LUT", newSchema: "SIS");
            MoveTable(name: "dbo.Supplier_LUT", newSchema: "SIS");
            MoveTable(name: "dbo.Units", newSchema: "SIS");
        }
        
        public override void Down()
        {
            MoveTable(name: "SIS.Units", newSchema: "dbo");
            MoveTable(name: "SIS.Supplier_LUT", newSchema: "dbo");
            MoveTable(name: "SIS.Location_LUT", newSchema: "dbo");
            MoveTable(name: "SIS.ItemLocations", newSchema: "dbo");
            MoveTable(name: "SIS.Items", newSchema: "dbo");
            MoveTable(name: "SIS.Transactions", newSchema: "dbo");
            MoveTable(name: "SIS.Department_LUT", newSchema: "dbo");
        }
    }
}
