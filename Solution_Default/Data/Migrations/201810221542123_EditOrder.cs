namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Orders", "Username");
            DropColumn("dbo.Orders", "FirstName");
            DropColumn("dbo.Orders", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Orders", "Username", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Orders", "CustomerName");
        }
    }
}
