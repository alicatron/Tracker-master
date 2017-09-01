using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tracker.Controllers.TrackerController
{
    public class GraphController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
    }
}