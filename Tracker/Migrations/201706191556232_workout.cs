namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "ExerciseID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "ExerciseID");
        }
    }
}
