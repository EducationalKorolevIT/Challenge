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
            ViewBag.UserNickname = Session["userNickname"] ?? "no";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.UserNickname = Session["userNickname"] ?? "no";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.UserNickname = Session["userNickname"] ?? "no";
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}