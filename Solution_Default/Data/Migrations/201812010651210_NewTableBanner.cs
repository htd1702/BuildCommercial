namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableBanner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        type = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        MoreImages = c.String(storeType: "xml"),
                        Description = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Banners");
        }
    }
}
