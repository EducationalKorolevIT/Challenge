using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idk.Controllers
{
    public class HomeController : Controller
    {
        string[] acceptDatas = new string[]
        {
            "kek lol",
            "bruh aaaaaaaaa"
        };
        public ActionResult Index(bool isWrong = false)
        {
            ViewBag.isWrong = isWrong;
            return View();
        }

        public ActionResult Authorized()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public RedirectResult Auth(string data)
        {
            ViewBag.Login = data.Split(' ')[0];
            ViewBag.Password = data.Split(' ')[1];
            int id = -1;
            foreach (string d in acceptDatas)
            {
                if (data == d) id = 0;
            }
            if (id == -1)
            {
                return Redirect("/Home/Index?isWrong=true");
            }
            return Redirect("/Home/Authorized");
            Console.WriteLine(data.Split(' ')[0] + " " + data.Split(' ')[1]);
        }
    }
}