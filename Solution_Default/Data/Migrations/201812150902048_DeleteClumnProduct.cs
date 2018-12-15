namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteClumnProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "TransportFee");
            DropColumn("dbo.Products", "TransportFeeFr");
            DropColumn("dbo.Products", "TransportFeeVN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "TransportFeeVN", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "TransportFeeFr", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "TransportFee", c => c.Double(nullable: false));
        }
    }
}
