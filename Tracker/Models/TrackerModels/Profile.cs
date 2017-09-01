using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Tracker.Models.TrackerModels
{
    public enum WeightMeasurement
    {
        Kilograms, Pounds
    }

    public enum Gender
    {
        Male, Female, Unspecified
    }
    public class Profile
    {
        public int ProfileID { get; set; }
        
        public Gender Gender { get; set; }

        [DefaultValue(WeightMeasurement.Kilograms)]
        public WeightMeasurement WeightMeasurement { get; set; }

        public double Weight { get; set; }

        [Required(ErrorMessage = "Height in centimetres")]
        public double Height { get; set; }

        public double Age { get; set; }

        [Required(ErrorMessage ="The max weight you can lift for a number of reps before failure")]
        public double MaxWeight { get; set; }

        [Required(ErrorMessage= "The number of reps you can complete with max weight before failure")]
        public double Rep { get; set; }

        public double OneRepMax     //Based on Brzycki formula: Weight ÷ ( 1.0278 - ( 0.0278 × Number of repetitions ) Add to exercise and connect to each exercise
        {
            get
            {
                double oneRepMax = MaxWeight/(1.0278 -(0.0278*Rep));
                return oneRepMax;
            }
        }          

        //public virtual ApplicationUser User { get; set; }
                
    }
}