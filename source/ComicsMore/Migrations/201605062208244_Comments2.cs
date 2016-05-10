namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "ApplicationUserId_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_ApplicationUserId_Id");
            AddColumn("dbo.Comments", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "Time");
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUserId_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Comments", name: "ApplicationUserId_Id", newName: "User_Id");
        }
    }
}
