using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Please Enter an Exercise Name")]
        public String ExerciseName { get; set; }

        public BodyPart bodyPart { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
