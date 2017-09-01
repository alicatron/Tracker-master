using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.Models.TrackerModels;
using PagedList;

namespace Tracker.Controllers.TrackerController
{
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workouts
        public ActionResult Index(string searchString, int? pageNumber)
        {
            var exercises = from x in db.Workouts
                            select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(x => x.Exercises.ExerciseName.Contains(searchString));
               return View(exercises.OrderBy(x => x.Exercises.ExerciseName).ToList().ToPagedList(pageNumber ?? 1, 20));
            }
           

            else
            {
                var workouts = db.Workouts.Include(w => w.Exercises);
                return View(workouts.OrderByDescending(x=>x.WorkoutDate).ToList().ToPagedList(pageNumber ?? 1,20));
            }
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName");
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutMasterID,WorkoutID,WeightLifted,Repetition,Set,WorkoutDate,Duration,ExerciseID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutMasterID,WorkoutID,WeightLifted,Repetition,Set,WorkoutDate,Duration,ExerciseID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            db.Workouts.Remove(workout);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
