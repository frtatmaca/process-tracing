namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertableforeignkey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "CreatedByUserIds_Id", newName: "Cheif_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_CreatedByUserIds_Id", newName: "IX_Cheif_Id");
            AddColumn("dbo.AspNetUsers", "CreatedByUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "LastUpdatedByUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CreatedByUser_Id");
            CreateIndex("dbo.AspNetUsers", "LastUpdatedByUser_Id");
            AddForeignKey("dbo.AspNetUsers", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "LastUpdatedByUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "CheifId");
            DropColumn("dbo.AspNetUsers", "CreatedByUserId");
            DropColumn("dbo.AspNetUsers", "LastUpdatedByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastUpdatedByUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedByUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "CheifId", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "LastUpdatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "LastUpdatedByUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CreatedByUser_Id" });
            DropColumn("dbo.AspNetUsers", "LastUpdatedByUser_Id");
            DropColumn("dbo.AspNetUsers", "CreatedByUser_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Cheif_Id", newName: "IX_CreatedByUserIds_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Cheif_Id", newName: "CreatedByUserIds_Id");
        }
    }
}
