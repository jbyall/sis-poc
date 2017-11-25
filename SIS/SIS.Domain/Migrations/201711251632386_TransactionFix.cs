namespace SIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "QuantityChange", c => c.Int(nullable: false));
            DropColumn("dbo.Transactions", "Qty_OnHand_Dist");
            DropColumn("dbo.Transactions", "Qty_OnHand_Store");
            DropColumn("dbo.Transactions", "Qty_OnHand_Sub");
            DropColumn("dbo.Transactions", "Qty_Change_Dist");
            DropColumn("dbo.Transactions", "Qty_Change_Stor");
            DropColumn("dbo.Transactions", "Qty_Change_Sub");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Qty_Change_Sub", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Qty_Change_Stor", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Qty_Change_Dist", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Qty_OnHand_Sub", c => c.Int());
            AddColumn("dbo.Transactions", "Qty_OnHand_Store", c => c.Int());
            AddColumn("dbo.Transactions", "Qty_OnHand_Dist", c => c.Int());
            DropColumn("dbo.Transactions", "QuantityChange");
        }
    }
}
