namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Quantity", c => c.Int());
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
