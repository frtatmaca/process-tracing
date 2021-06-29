namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserIAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedByUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "SimulatedByUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastUpdatedByUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastUpdatedDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TrackingGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TrackingGuid");
            DropColumn("dbo.AspNetUsers", "LastUpdatedDate");
            DropColumn("dbo.AspNetUsers", "LastUpdatedByUserId");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            DropColumn("dbo.AspNetUsers", "SimulatedByUserId");
            DropColumn("dbo.AspNetUsers", "CreatedByUserId");
        }
    }
}
