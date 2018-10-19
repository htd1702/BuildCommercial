namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableProudct1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Double(nullable: false));
        }
    }
}
