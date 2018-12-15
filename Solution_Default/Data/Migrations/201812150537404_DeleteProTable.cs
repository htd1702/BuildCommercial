namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteProTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Content", c => c.String());
        }
    }
}
