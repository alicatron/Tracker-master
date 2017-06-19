namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error : DbMigration //updated error message on ExerciseName to ensure Name is input
    {
        public override void Up()
        {
            AlterColumn("dbo.Exercises", "ExerciseName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exercises", "ExerciseName", c => c.String());
        }
    }
}
