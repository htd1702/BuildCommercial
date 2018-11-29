namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScaleProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Scale", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Scale");
        }
    }
}
