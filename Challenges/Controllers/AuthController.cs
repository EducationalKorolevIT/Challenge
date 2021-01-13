using Challenges.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Challenges.Controllers
{
    public class AuthController : Controller
    {

        public ActionResult Profile(string errText = null)
        {
            ViewBag.errText = errText;
            return View();
        }

        public ActionResult ProfileSettings()
        {
            return View();
        }

        async public Task<ActionResult> Registrate()
        {
            string login = Request.Params["login"];
            string password = Request.Params["password"];
            Users found = SharedObjects.database.Users.FirstOrDefault(e => e.Login == login);
            if (found != null)
            {
                return Content("ОШИБКА");
            }
            else
            {
                Users addUser = new Users { Login = login, Password = password, CreateDate = DateTime.Now };
                SharedObjects.database.Users.Add(addUser);
                SharedObjects.database.SaveChanges();
                return new RedirectResult("/Home/Index");
            }
        }

        public ActionResult SaveToCookie()
        {
            if (Session["User"] != null)
            {
                SharedObjects.SetCookieUserData((Users)Session["User"]);
                return new RedirectResult("/Home/Index");
            }
            return Content("ОШИБКА");
        }

        public ActionResult LoadFromCookie()
        {
            if (Request.Cookies["UserData"] != null)
            {
                Session["User"] = SharedObjects.GetCookieUserData();
                return new RedirectResult("/Home/Index");
            }
            return Content("ОШИБКА");
        }


        async public Task<ActionResult> Authorize()
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
                return Content("ОШИБКА");
            }
        }

        async public Task<RedirectResult> Exit()
        {
            Response.Cookies["UserData"].Value = null;
            Session["User"] = null;
            return new RedirectResult("/");
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}