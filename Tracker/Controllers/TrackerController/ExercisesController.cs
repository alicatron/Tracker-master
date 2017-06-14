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
    public class ExercisesController : Controller
    {
        private TrackerContext db = new TrackerContext();

        // GET: Exercises
        public ActionResult Index(string searchString, string option)
        {
            var exercises = from e in db.Exercises
                            select e;

            if (!String.IsNullOrEmpty(searchString) & option == "ExerciseName")
            {
                exercises = exercises.Where(x => x.ExerciseName.Contains(searchString));
                return View(exercises);
            }
            else if (!String.IsNullOrEmpty(searchString) & option == "BodyPart")
            {
                try
                {
                    BodyPart bodyValue = (BodyPart)Enum.Parse(typeof(BodyPart), searchString);
                    if (Enum.IsDefined(typeof(BodyPart), bodyValue) | bodyValue.ToString().Contains(","))
                        exercises = exercises.Where(x => x.bodyPart.ToString().ToUpper() == bodyValue.ToString().ToUpper());
                    return View(exercises.OrderBy(x => x.ExerciseName));
                }
                catch (ArgumentException)
                {
                    return new HttpStatusCodeResult(404);
                }
            }



            else
            {
                return View(db.Exercises.OrderBy(x => x.bodyPart).ToList());
            }
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExerciseID,ExerciseName,bodyPart")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExerciseID,ExerciseName,bodyPart")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            db.Exercises.Remove(exercise);
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
