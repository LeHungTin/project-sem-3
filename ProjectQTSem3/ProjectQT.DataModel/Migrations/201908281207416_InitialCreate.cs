namespace ProjectQT.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UpdateAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        AttributesId = c.Int(nullable: false),
                        ProduceCode = c.String(),
                        Name = c.String(),
                        Price = c.Long(nullable: false),
                        SalePrice = c.Long(nullable: false),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        UpdateAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attributes", t => t.AttributesId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AttributesId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UpdateAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        HandelerId = c.Int(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        FullName = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        UpdateAt = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Email = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        UpdateAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "AttributesId", "dbo.Attributes");
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "AttributesId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Attributes");
        }
    }
}
