namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerAddress", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerEmail", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Orders", "CustomerMobile", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "CustomerMessage", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Orders", "CustomerID");
            DropColumn("dbo.Orders", "Description");
            DropTable("dbo.Customers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FullName = c.String(nullable: false, maxLength: 60),
                        PhoneNumber = c.String(maxLength: 13),
                        Address = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                        Image = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "Description", c => c.String(maxLength: 500));
            AddColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "CustomerMessage");
            DropColumn("dbo.Orders", "CustomerMobile");
            DropColumn("dbo.Orders", "CustomerEmail");
            DropColumn("dbo.Orders", "CustomerAddress");
            DropColumn("dbo.Orders", "CustomerName");
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
        }
    }
}
