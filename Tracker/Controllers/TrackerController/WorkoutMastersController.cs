using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.Models.TrackerModels;

namespace Tracker.Controllers.TrackerController
{
    public class WorkoutMastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public WorkoutMastersController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: WorkoutMasters
        [Authorize]
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(db.WorkoutMasters.ToList().Where(workoutmaster => workoutmaster.User.Id == currentUser.Id));
        }

        // GET: WorkoutMasters/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = await db.WorkoutMasters.FindAsync(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            if (workoutMaster.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkoutMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WorkoutMasterID,WorkoutName")] WorkoutMaster workoutMaster)
        {
            if (db.WorkoutMasters.Any(x => x.WorkoutName == workoutMaster.WorkoutName)) 
            {
                ModelState.AddModelError("WorkoutMaster", "Workout Name already exists, please enter a different name");
                ViewBag.Message = string.Format("Workout Name already exists, please enter a different name");
            }
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                ViewBag.Message = string.Format("Workout Saved Successfully!");
                workoutMaster.User = currentUser;
                db.WorkoutMasters.Add(workoutMaster);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return View();
            }

            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = await db.WorkoutMasters.FindAsync(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            if (workoutMaster.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(workoutMaster);
        }

        // POST: WorkoutMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WorkoutMasterID,WorkoutName")] WorkoutMaster workoutMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workoutMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = await db.WorkoutMasters.FindAsync(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            if (workoutMaster.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(workoutMaster);
        }

        // POST: WorkoutMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkoutMaster workoutMaster = await db.WorkoutMasters.FindAsync(id);
            db.WorkoutMasters.Remove(workoutMaster);
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
