using Challenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenges.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CompetitorsIncoming()
        {
            return View();
        }

        public ActionResult CompetitorsOutcoming()
        {
            return View();
        }
    }
}