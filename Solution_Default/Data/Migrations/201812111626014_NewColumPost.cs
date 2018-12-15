namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "MoreImages", c => c.String(storeType: "xml"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "MoreImages");
        }
    }
}
