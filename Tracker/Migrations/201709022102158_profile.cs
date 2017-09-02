namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileID = c.Int(nullable: false, identity: true),
                        Gender = c.Int(nullable: false),
                        WeightMeasurement = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Age = c.Double(nullable: false),
                        MaxWeight = c.Double(nullable: false),
                        Rep = c.Double(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.Users", "ProfileID");
            DropColumn("dbo.Users", "Gender");
            DropColumn("dbo.Users", "WeightMeasurement");
            DropColumn("dbo.Users", "Weight");
            DropColumn("dbo.Users", "Height");
            DropColumn("dbo.Users", "Age");
            DropColumn("dbo.Users", "MaxWeight");
            DropColumn("dbo.Users", "Rep");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Rep", c => c.Double());
            AddColumn("dbo.Users", "MaxWeight", c => c.Double());
            AddColumn("dbo.Users", "Age", c => c.Double());
            AddColumn("dbo.Users", "Height", c => c.Double());
            AddColumn("dbo.Users", "Weight", c => c.Double());
            AddColumn("dbo.Users", "WeightMeasurement", c => c.Int());
            AddColumn("dbo.Users", "Gender", c => c.Int());
            AddColumn("dbo.Users", "ProfileID", c => c.Int());
            DropForeignKey("dbo.Profiles", "User_Id", "dbo.Users");
            DropIndex("dbo.Profiles", new[] { "User_Id" });
            DropTable("dbo.Profiles");
        }
    }
}
