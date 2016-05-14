namespace ComicsMore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedalsInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contidion = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medals", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Medals", new[] { "Owner_Id" });
            DropTable("dbo.Medals");
        }
    }
}
