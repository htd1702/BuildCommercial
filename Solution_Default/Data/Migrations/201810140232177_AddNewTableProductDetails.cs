namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableProductDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ColorID = c.Int(nullable: false),
                        SizeID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDetails", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductDetails", new[] { "ProductID" });
            DropTable("dbo.ProductDetails");
        }
    }
}
