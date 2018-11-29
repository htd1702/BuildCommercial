namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPriceProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceVN", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "PriceFr", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PriceFr");
            DropColumn("dbo.Products", "PriceVN");
        }
    }
}
