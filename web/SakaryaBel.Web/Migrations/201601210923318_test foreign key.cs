namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FileId", "dbo.File");
            DropIndex("dbo.AspNetUsers", new[] { "FileId" });
            AddColumn("dbo.File", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.File", "ApplicationUser_Id");
            AddForeignKey("dbo.File", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.File", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.File", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "FileId");
            AddForeignKey("dbo.AspNetUsers", "FileId", "dbo.File", "FileId");
        }
    }
}
