namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumBannerAndPostCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "Title", c => c.String());
            AddColumn("dbo.PostCategories", "Promotion", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "Promotion");
            DropColumn("dbo.Banners", "Title");
        }
    }
}
