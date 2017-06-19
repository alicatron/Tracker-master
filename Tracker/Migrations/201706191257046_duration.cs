namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "Duration");
        }
    }
}
