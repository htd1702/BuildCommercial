namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewProertyTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sizes", "ParentSizeID", c => c.Int());
            AddColumn("dbo.PostCategories", "DescriptionVN", c => c.String());
            AddColumn("dbo.Posts", "DescriptionVN", c => c.String());
            AlterColumn("dbo.PostCategories", "Description", c => c.String());
            AlterColumn("dbo.Posts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.PostCategories", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.Posts", "DescriptionVN");
            DropColumn("dbo.PostCategories", "DescriptionVN");
            DropColumn("dbo.Sizes", "ParentSizeID");
        }
    }
}
