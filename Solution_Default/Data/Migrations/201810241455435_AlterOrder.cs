namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "CreatedDate");
            DropColumn("dbo.Orders", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CreatedBy", c => c.String());
            AddColumn("dbo.Orders", "CreatedDate", c => c.DateTime());
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
