namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewProertyTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "DescriptionFr", c => c.String());
            AddColumn("dbo.Posts", "DescriptionFr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DescriptionFr");
            DropColumn("dbo.PostCategories", "DescriptionFr");
        }
    }
}
