namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class convertstringuserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "CheifId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CheifId", c => c.Int());
        }
    }
}
