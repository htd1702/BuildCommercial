namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreImgProductDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDetails", "MoreImages", c => c.String(storeType: "xml"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductDetails", "MoreImages");
        }
    }
}
