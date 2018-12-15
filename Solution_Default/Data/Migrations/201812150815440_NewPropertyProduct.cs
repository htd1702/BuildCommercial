namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertyProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TransportFee", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TransportFee");
        }
    }
}
