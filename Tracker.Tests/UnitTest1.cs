using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracker.Models.TrackerModels;
using Tracker.Controllers.TrackerController;

namespace Tracker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTotalLifted() //fail expected
        {
            Workout workout = new Workout
            {
                WeightLifted = 5.5,
                Set = 10,
                Repetition = 7,
            };
            Assert.AreEqual(workout.TotalLifted, 380);

        }

        [TestMethod]
        public void TestTotalLifted1() //pass expected
        {
            Workout workout = new Workout
            {
                WeightLifted = 5.5,
                Set = 10,
                Repetition = 7,
            };
            Assert.AreEqual(workout.TotalLifted, 385);

        }

        [TestMethod]
        public void CreateExercise()
        {
            Exercise exercise1 = new Exercise
            {
                ExerciseName = "Push Up",
                bodyPart = BodyPart.Abs
            };
        }

        [TestMethod]
        public void OneRepMax() //pass expected
        {
            Profile newProfile = new Profile
            {
                MaxWeight = 5,
                Rep = 7,
            };
            Assert.AreEqual(newProfile.OneRepMax, 6.00096015362458);

        }

        [TestMethod]
        public void OneRepMax1() //fail expected
        {
            Profile newProfile = new Profile
            {
                MaxWeight = 5,
                Rep = 7,
            };
            Assert.AreEqual(newProfile.OneRepMax, 6);

        }

        [TestMethod]
        public void CreateWorkoutName()
        {
            WorkoutMaster workout = new WorkoutMaster
            {
                WorkoutName = "My Workout"
            };
        }

        [TestMethod]
        public void CreateProfile()
        {
            Profile profile = new Profile
            {
                Age = 18,
                Gender = Gender.Male,
                Height = 170,
                Weight = 70,
                WeightMeasurement = WeightMeasurement.Kilograms,
                MaxWeight = 5,
                Rep = 5
            };
        }

        [TestMethod]
        public void CreateWorkout()
        {
            Workout workout = new Workout
            {
                WorkoutDate = DateTime.Now,
                WeightLifted = 10,
                Set = 5,
                Repetition = 5,
                Duration = 60,
                ExerciseID = 1
            };
        }

        [TestMethod]
        public void CreateRole()
        {
            UserRole role = new UserRole
            {
               Name = "New User"
            };
        }
    }
}
