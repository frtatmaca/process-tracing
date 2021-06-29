namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdByUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedByUserIds_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CreatedByUserIds_Id");
            AddForeignKey("dbo.AspNetUsers", "CreatedByUserIds_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CreatedByUserIds_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "CreatedByUserIds_Id" });
            DropColumn("dbo.AspNetUsers", "CreatedByUserIds_Id");
        }
    }
}
