using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models.TrackerModels
{

    public enum BodyPart
    {
        Abs, Arms, Back, Chest, Legs, Shoulders
    }
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public String ExerciseName { get; set; }

        public BodyPart bodyPart { get; set; }

        public virtual Workout Workout { get; set; }
    }
}
