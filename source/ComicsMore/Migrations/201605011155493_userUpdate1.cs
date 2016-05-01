namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "PageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PageUrl", c => c.String());
        }
    }
}
