using Challenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Challenges.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Registration(int errCode = 0)
        {
            ViewBag.UserNickname = Session["userNickname"] ?? "no";
            ViewBag.errCode = errCode;
            return View();
        }

        public ActionResult Authorization(int errCode = 0)
        {
            ViewBag.UserNickname = Session["userNickname"] ?? "no";
            ViewBag.errCode = errCode;
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public RedirectResult Registrate()
        {
            string login = Request.Params["login"];
            string password = Request.Params["password"];
            Users found = SharedObjects.database.Users.FirstOrDefault(e => e.Login == login);
            if (found != null)
            {
                return new RedirectResult("/Auth/Registration?errCode=1");
            }
            else
            {
                Users addUser = new Users { Login = login, Password = password, CreateDate = DateTime.Now };
                SharedObjects.database.Users.Add(addUser);
                SharedObjects.database.SaveChanges();
                return new RedirectResult("/Home/Index");
            }
        }

        public RedirectResult Authorize()
        {
            string login = Request.Params["login"];
            string password = Request.Params["password"];
            Users found = SharedObjects.database.Users.FirstOrDefault(e => e.Login == login && e.Password == password);
            if (found != null)
            {
                Session["User"] = found;
                return new RedirectResult("/Home/Index");
            }
            else
            {
                return new RedirectResult("/Auth/Authorization?errCode=1");
            }
        }
    }
}