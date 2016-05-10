namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "ApplicationUserId_Id", newName: "Author_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUserId_Id", newName: "IX_Author_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_Author_Id", newName: "IX_ApplicationUserId_Id");
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "ApplicationUserId_Id");
        }
    }
}
