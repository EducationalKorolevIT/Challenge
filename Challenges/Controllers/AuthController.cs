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
        public ActionResult Registration(int errCode = 0)
        {
            ViewBag.errCode = errCode;
            return View();
        }

        public ActionResult Authorization(int errCode = 0)
        {
            ViewBag.errCode = errCode;
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        async public Task<RedirectResult> Registrate()
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

        async public Task<RedirectResult> Authorize()
        {
            string login = Request.Params["login"];
            string password = Request.Params["password"];
            Users found = SharedObjects.database.Users.FirstOrDefault(e => e.Login == login && e.Password == password);
            if (found != null)
            {
                SharedObjects.SetCookieUserData(found);
                return new RedirectResult("/Home/Index");
            }
            else
            {
                return new RedirectResult("/Auth/Authorization?errCode=1");
            }
        }

        async public Task<RedirectResult> Exit()
        {
            Response.Cookies["User"].Value = null;
            return new RedirectResult("/Auth/Authorization");
        }
    }
}