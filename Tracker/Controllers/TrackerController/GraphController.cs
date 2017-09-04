using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.Models.TrackerModels;

namespace Tracker.Controllers.TrackerController
{
    public class GraphController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public GraphController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DrawChart()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            var workouts = new List<Workout>(db.Workouts.ToList().Where(workout => workout.User.Id == currentUser.Id));

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\aliso\Desktop\Project\Tracker-master\Tracker-master\Tracker\App_Data\TrackerConnection.mdf;Initial Catalog=TrackerContext-201709040925188;Integrated Security=True;MultipleActiveResultSets=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT WorkoutDate, TotalLifted, ExerciseID, WeightLifted FROM Workouts WHERE User_Id = 'currentUserId' ORDER BY ExerciseID", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workouts.Add(new Workout { WorkoutDate = reader.GetDateTime(0), TotalLifted = reader.GetDouble(1) });
                        workouts.Add(new Workout { WorkoutDate = reader.GetDateTime(0), WeightLifted = reader.GetDouble(2) });
        
                    }
                }
            }
            return View(workouts);

            //ViewBag.Graph = workouts;
            //return RedirectToAction("CreateChart", new { model = workouts });
        }

/*        public ActionResult CreateChart()
        {
            var myChart = new Chart(width: 800, height: 500)
                    .AddTitle("Weight Lifting Progress")
                    .AddSeries(chartType: "Line",
                               xValue: workouts, xField: "WorkoutDate",
                               yValues: workouts, yFields: "TotalLifted")
                    .Write("png")
                    .Save("png");

            return View();
        }*/

    }
}
