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

        public ActionResult Authorization(int errCode = 0, string errText = null)
        {
            ViewBag.errCode = errCode;
            ViewBag.errText = errText;
            return View();
        }

        public ActionResult Profile(string errText = null)
        {
            ViewBag.errText = errText;
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

        public ActionResult SaveToCookie()
        {
            if (Session["User"] != null)
            {
                Response.Cookies["UserData"].Value = new JavaScriptSerializer().Serialize(Session["User"]);
                return new RedirectResult("/Home/Index");
            }
            return new RedirectResult("/Auth/Profile?errText=Ошибка: Session[\"User\"] равно null. Возможно, вы не вошли?");
        }

        public ActionResult LoadFromCookie()
        {
            if (Request.Cookies["UserData"] != null)
            {
                Session["User"] = new JavaScriptSerializer().Deserialize<Users>(Request.Cookies["UserData"].Value);
                return new RedirectResult("/Home/Index");
            }
            return new RedirectResult("/Auth/Authorization?errText=Ошибка: Request.Cooukies[\"UserData\"] равно null. Возможно, у вас нет сохранённого пользователя?");
        }


        async public Task<RedirectResult> Authorize()
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

        async public Task<RedirectResult> Exit()
        {
            Response.Cookies["UserData"].Value = null;
            Session["User"] = null;
            return new RedirectResult("/Auth/Authorization");
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}