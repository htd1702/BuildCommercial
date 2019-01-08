namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTypeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Colors", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.ProductCategories", "CategoryType", c => c.Int(nullable: false));
            AddColumn("dbo.Sizes", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sizes", "Type");
            DropColumn("dbo.ProductCategories", "CategoryType");
            DropColumn("dbo.Colors", "Type");
        }
    }
}
