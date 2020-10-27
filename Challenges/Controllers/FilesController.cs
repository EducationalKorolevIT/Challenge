using Challenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenges.Controllers
{
    public class FilesController : Controller
    {
        public ActionResult GetAvatar()
        {
            Users user = (Users)Session["User"] ?? new Users { Id = 0 };
            string file_path = Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg");
            string file_type = "image/jpeg";
            string file_name = user.Id + ".jpg";
            return File(file_path, file_type, file_name);
        }

        [HttpPost]
        public ActionResult UpdateProfile(string nickname, HttpPostedFileBase avatarupload)
        {
            Users user = (Users)Session["User"];
            if (user != null)
            {
                user.Nickname = nickname;
                if(avatarupload!=null)
                    avatarupload.SaveAs(Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg"));
                SharedObjects.database.SaveChanges();
            }

            return new RedirectResult("/Auth/Profile");
        }

        public ActionResult GetCssFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "text/css";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }

        public ActionResult GetJpgFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "image/jpg";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }
    }
}