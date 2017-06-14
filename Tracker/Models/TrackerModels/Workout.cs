using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tracker.Models.TrackerModels
{
    public class Workout
    {

        public int WorkoutID { get; set; }

        public String ExerciseName { get; set; }

        public double WeightLifted { get; set; }  //kilograms

        public int Repetition { get; set; } //The amount of lifts done

        public int Set { get; set; } // The amount of times a group of repitions are done

        [Display(Name = "Workout Date")]
        [DataType(DataType.Date)]
        public DateTime WorkoutDate { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}