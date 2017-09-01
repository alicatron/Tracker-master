using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tracker.Models.TrackerModels
{
    public class WorkoutMaster
    {
        public int WorkoutMasterID { get; set; }
        public string WorkoutName { get; set; }

        [ForeignKey("WorkoutID")]
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}