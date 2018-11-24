namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NameVN", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Products", "NameFr", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.ProductCategories", "NameVN", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.ProductCategories", "NameFr", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.PostCategories", "NameVN", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.PostCategories", "NameFr", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Posts", "NameVN", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Posts", "NameFr", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "NameFr");
            DropColumn("dbo.Posts", "NameVN");
            DropColumn("dbo.PostCategories", "NameFr");
            DropColumn("dbo.PostCategories", "NameVN");
            DropColumn("dbo.ProductCategories", "NameFr");
            DropColumn("dbo.ProductCategories", "NameVN");
            DropColumn("dbo.Products", "NameFr");
            DropColumn("dbo.Products", "NameVN");
        }
    }
}
