namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserStyle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Style", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Style");
        }
    }
}
