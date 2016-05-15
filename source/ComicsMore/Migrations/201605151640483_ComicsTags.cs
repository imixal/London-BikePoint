namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComicsTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComicStrips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Rating = c.Double(),
                        Json = c.String(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ComicStrip_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComicStrips", t => t.ComicStrip_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ComicStrip_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "ComicStrip_Id", "dbo.ComicStrips");
            DropForeignKey("dbo.ComicStrips", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Tags", new[] { "ComicStrip_Id" });
            DropIndex("dbo.ComicStrips", new[] { "Author_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.ComicStrips");
        }
    }
}
