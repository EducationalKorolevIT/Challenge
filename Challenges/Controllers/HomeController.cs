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

        public ActionResult Competitors()
        {
            return View();
        }

        public ActionResult ErrorPage(string errorMessage)
        {
            ViewBag.errMsg = errorMessage;
            return View();
        }
    }
}