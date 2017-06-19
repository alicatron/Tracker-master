namespace Tracker.Migrations
{
    using Models.TrackerModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tracker.Models.TrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Tracker.Models.TrackerContext";
        }

        protected override void Seed(Tracker.Models.TrackerContext context)
        {
            context.Exercises.AddOrUpdate(x => x.ExerciseID,
                new Exercise() { ExerciseName = "Dumbbell Press", bodyPart = BodyPart.Shoulders },
                new Exercise() { ExerciseName = "Bicep Curl", bodyPart = BodyPart.Arms },
                new Exercise() { ExerciseName = "Bench Press", bodyPart = BodyPart.Arms },
                new Exercise() { ExerciseName = "Deadlift", bodyPart = BodyPart.Back },
                new Exercise() { ExerciseName = "Barbell Row", bodyPart = BodyPart.Arms },
                new Exercise() { ExerciseName = "Squat", bodyPart = BodyPart.Legs },
                new Exercise() { ExerciseName = "Lunges", bodyPart = BodyPart.Legs },
                new Exercise() { ExerciseName = "Shoulder Fly", bodyPart = BodyPart.Chest },
                new Exercise() { ExerciseName = "Dumbbell Kickback", bodyPart = BodyPart.Arms },
                new Exercise() { ExerciseName = "Hammer Curl", bodyPart = BodyPart.Arms },
                new Exercise() { ExerciseName = "Bent-Over Barbell Row", bodyPart = BodyPart.Shoulders },
                new Exercise() { ExerciseName = "Shoulder Press", bodyPart = BodyPart.Shoulders }
                );
        }
    }
}
