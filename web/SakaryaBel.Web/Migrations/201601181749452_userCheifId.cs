namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userCheifId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CheifId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CheifId");
        }
    }
}
