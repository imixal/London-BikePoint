namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Comments");
            AddColumn("dbo.Comments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Comments", "UserPage_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Comments", "Id");
            CreateIndex("dbo.Comments", "ApplicationUser_Id");
            CreateIndex("dbo.Comments", "UserPage_Id");
            AddForeignKey("dbo.Comments", "UserPage_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserPage_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "UserPage_Id" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Comments", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Comments", "UserPage_Id");
            DropColumn("dbo.Comments", "ApplicationUser_Id");
            AddPrimaryKey("dbo.Comments", "Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
