namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertyProduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TransportFeeFr", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "TransportFeeVN", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TransportFeeVN");
            DropColumn("dbo.Products", "TransportFeeFr");
        }
    }
}
