using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.Models.TrackerModels
{
    public class Workout
    {
        [Display(Name = "Workout Name")]
        public int WorkoutMasterID { get; set; }

        [ForeignKey("WorkoutMasterID")]
        public virtual WorkoutMaster WorkoutMaster { get; set; }

        public int WorkoutID { get; set; }

        public double WeightLifted { get; set; }  //kilograms

        [DefaultValue(1)]
        public int Repetition { get; set; } //The amount of lifts done

        [DefaultValue(1)]
        public int Set { get; set; } // The amount of times a group of repitions are done

        [Display(Name = "Workout Date")]
        [DataType(DataType.Date)]
        public DateTime WorkoutDate { get; set; }

        [DefaultValue (1)]
        public int Duration { get; set; }
        
        [Display(Name = "Exercise Name")]
        public int ExerciseID { get; set; }

        [ForeignKey("ExerciseID")]
        public virtual Exercise Exercises { get; set; }

        public double TotalLifted
        {
            get
            {
                double totalLifted = (Repetition * WeightLifted * Set);
                return totalLifted;
            }
        }

    }
}