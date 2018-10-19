namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "UnitPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Username", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "Phone", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Orders", "CustomerName");
            DropColumn("dbo.Orders", "CustomerAddress");
            DropColumn("dbo.Orders", "CustomerEmail");
            DropColumn("dbo.Orders", "CustomerMobile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CustomerMobile", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "CustomerEmail", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerAddress", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Orders", "Total");
            DropColumn("dbo.Orders", "Phone");
            DropColumn("dbo.Orders", "Email");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "LastName");
            DropColumn("dbo.Orders", "FirstName");
            DropColumn("dbo.Orders", "Username");
            DropColumn("dbo.OrderDetails", "UnitPrice");
        }
    }
}
