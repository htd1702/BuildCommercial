namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitleTypeBanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "TitleType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "TitleType");
        }
    }
}
