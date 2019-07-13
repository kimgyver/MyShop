namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcreatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Baskets", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ProductCategories", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Products", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreatedAt");
            DropColumn("dbo.ProductCategories", "CreatedAt");
            DropColumn("dbo.Baskets", "CreatedAt");
            DropColumn("dbo.BasketItems", "CreatedAt");
        }
    }
}
