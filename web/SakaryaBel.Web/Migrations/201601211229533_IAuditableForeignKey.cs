namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IAuditableForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Message", new[] { "Id" });
            AddColumn("dbo.Activity", "CreatedByUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Activity", "LastUpdatedByUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Message", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Message", "CreatedByUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Message", "LastUpdatedByUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Message", "Id", c => c.String());
            CreateIndex("dbo.Activity", "CreatedByUser_Id");
            CreateIndex("dbo.Activity", "LastUpdatedByUser_Id");
            CreateIndex("dbo.Message", "ApplicationUser_Id");
            CreateIndex("dbo.Message", "CreatedByUser_Id");
            CreateIndex("dbo.Message", "LastUpdatedByUser_Id");
            AddForeignKey("dbo.Activity", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Activity", "LastUpdatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Message", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Message", "LastUpdatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Message", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Activity", "CreatedByUserId");
            DropColumn("dbo.Activity", "LastUpdatedByUserId");
            DropColumn("dbo.Message", "CreatedByUserId");
            DropColumn("dbo.Message", "LastUpdatedByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "LastUpdatedByUserId", c => c.String());
            AddColumn("dbo.Message", "CreatedByUserId", c => c.String());
            AddColumn("dbo.Activity", "LastUpdatedByUserId", c => c.String());
            AddColumn("dbo.Activity", "CreatedByUserId", c => c.String());
            DropForeignKey("dbo.Message", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Message", "LastUpdatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Message", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activity", "LastUpdatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activity", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Message", new[] { "LastUpdatedByUser_Id" });
            DropIndex("dbo.Message", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.Message", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Activity", new[] { "LastUpdatedByUser_Id" });
            DropIndex("dbo.Activity", new[] { "CreatedByUser_Id" });
            AlterColumn("dbo.Message", "Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Message", "LastUpdatedByUser_Id");
            DropColumn("dbo.Message", "CreatedByUser_Id");
            DropColumn("dbo.Message", "ApplicationUser_Id");
            DropColumn("dbo.Activity", "LastUpdatedByUser_Id");
            DropColumn("dbo.Activity", "CreatedByUser_Id");
            CreateIndex("dbo.Message", "Id");
            AddForeignKey("dbo.Message", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
