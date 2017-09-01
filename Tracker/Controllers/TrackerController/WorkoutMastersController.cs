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

namespace Tracker.Controllers.TrackerController
{
    public class WorkoutMastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkoutMasters
        public ActionResult Index()
        {
            return View(db.WorkoutMasters.ToList());
        }

        // GET: WorkoutMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = db.WorkoutMasters.Find(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkoutMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutMasterID,WorkoutName")] WorkoutMaster workoutMaster)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = string.Format("Workout Saved Successfully!");
                db.WorkoutMasters.Add(workoutMaster);
                db.SaveChanges();
                return View();
            }

            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = db.WorkoutMasters.Find(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            return View(workoutMaster);
        }

        // POST: WorkoutMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkoutMasterID,WorkoutName")] WorkoutMaster workoutMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workoutMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workoutMaster);
        }

        // GET: WorkoutMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkoutMaster workoutMaster = db.WorkoutMasters.Find(id);
            if (workoutMaster == null)
            {
                return HttpNotFound();
            }
            return View(workoutMaster);
        }

        // POST: WorkoutMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkoutMaster workoutMaster = db.WorkoutMasters.Find(id);
            db.WorkoutMasters.Remove(workoutMaster);
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
