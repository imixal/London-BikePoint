namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedalNameAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medals", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medals", "Name", c => c.String());
        }
    }
}
