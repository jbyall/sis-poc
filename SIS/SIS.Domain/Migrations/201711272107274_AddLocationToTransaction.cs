namespace SIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "LocationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "LocationId");
        }
    }
}
