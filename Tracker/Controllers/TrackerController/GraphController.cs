using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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

            string connectionString = @"Data Source = Server=tcp:aliron.database.windows.net,1433;Initial Catalog=trackerDatabase;Persist Security Info=False;User ID=alicatron;Password=Acdsrqj16!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT WorkoutDate, TotalLifted FROM Workouts WHERE User_Id = 'currentUserId'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workouts.Add(new Workout { WorkoutDate = reader.GetDateTime(0), TotalLifted = reader.GetDouble(1) });
                    }
                }
            }

            return View(workouts);
        }

       /* public ActionResult CreateChart()
        {
            var mychart = new Chart(width: 800, height: 500)
                    .AddTitle("Weight Lifting Progress")
                    .AddSeries(chartType: "Line",
                               xValue: M, xField: "WorkoutDate",
                               yValues: db, yFields: "TotalLifted")
                    .Write("png")
                    .Save("png");

            return View(mychart);
        }*/

    }
}
