namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IAuditableupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activity", "CreatedByUserId", c => c.String());
            AlterColumn("dbo.Activity", "SimulatedByUserId", c => c.String());
            AlterColumn("dbo.Activity", "LastUpdatedByUserId", c => c.String());
            AlterColumn("dbo.File", "CreatedByUserId", c => c.String());
            AlterColumn("dbo.File", "SimulatedByUserId", c => c.String());
            AlterColumn("dbo.File", "LastUpdatedByUserId", c => c.String());
            AlterColumn("dbo.Message", "CreatedByUserId", c => c.String());
            AlterColumn("dbo.Message", "SimulatedByUserId", c => c.String());
            AlterColumn("dbo.Message", "LastUpdatedByUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Message", "LastUpdatedByUserId", c => c.Int());
            AlterColumn("dbo.Message", "SimulatedByUserId", c => c.Int());
            AlterColumn("dbo.Message", "CreatedByUserId", c => c.Int());
            AlterColumn("dbo.File", "LastUpdatedByUserId", c => c.Int());
            AlterColumn("dbo.File", "SimulatedByUserId", c => c.Int());
            AlterColumn("dbo.File", "CreatedByUserId", c => c.Int());
            AlterColumn("dbo.Activity", "LastUpdatedByUserId", c => c.Int());
            AlterColumn("dbo.Activity", "SimulatedByUserId", c => c.Int());
            AlterColumn("dbo.Activity", "CreatedByUserId", c => c.Int());
        }
    }
}
