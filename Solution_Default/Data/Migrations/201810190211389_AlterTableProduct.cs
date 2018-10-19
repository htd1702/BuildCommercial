namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
