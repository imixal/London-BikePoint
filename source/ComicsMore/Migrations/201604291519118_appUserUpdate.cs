namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appUserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImage", c => c.String());
            AddColumn("dbo.AspNetUsers", "PageUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "About");
            DropColumn("dbo.AspNetUsers", "PageUrl");
            DropColumn("dbo.AspNetUsers", "ProfileImage");
        }
    }
}
