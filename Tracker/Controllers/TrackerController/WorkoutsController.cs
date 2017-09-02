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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Tracker.Controllers.TrackerController
{
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public WorkoutsController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Workouts
        [Authorize]
        public ActionResult Index(string searchString, int? pageNumber)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var exercises = from x in db.Workouts
                            select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(x => x.Exercises.ExerciseName.Contains(searchString)).Include(w=>w.Exercises).Include(w=>w.WorkoutMaster).Where(workout => workout.User.Id == currentUser.Id);
               return View(exercises.OrderBy(x => x.Exercises.ExerciseName).ToList().ToPagedList(pageNumber ?? 1, 20));
            }
           

            else
            {
                var workouts = db.Workouts.Include(w => w.Exercises).Include(w=>w.WorkoutMaster).ToList().Where(workout => workout.User.Id == currentUser.Id);
                return View(workouts.OrderByDescending(x=>x.WorkoutDate).ToList().ToPagedList(pageNumber ?? 1,20));
            }
        }

        // GET: Workouts/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            if (workout.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(workout);
        }

        // GET: Workouts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName");
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WorkoutID,WeightLifted,Repetition,Set,WorkoutDate,Duration,ExerciseID,WorkoutMasterID")] Workout workout)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                workout.User = currentUser;
                ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
                db.Workouts.Add(workout);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            if (workout.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WorkoutID,WeightLifted,Repetition,Set,WorkoutDate,Duration,ExerciseID,WorkoutMasterID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workout).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExerciseID = new SelectList(db.Exercises, "ExerciseID", "ExerciseName", workout.ExerciseID);
            ViewBag.WorkoutMasterID = new SelectList(db.WorkoutMasters, "WorkoutMasterID", "WorkoutName", workout.WorkoutMasterID);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            if (workout.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();
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
