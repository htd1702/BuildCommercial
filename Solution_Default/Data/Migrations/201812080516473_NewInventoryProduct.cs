namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInventoryProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDetails", "Inventory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductDetails", "Inventory");
        }
    }
}
