namespace SakaryaBel.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FaliyetAdetAlani : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Number", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.Activity", "Number");
        }
    }
}
