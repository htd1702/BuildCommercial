namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Composition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Composition");
        }
    }
}
