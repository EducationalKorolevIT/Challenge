using Challenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Challenges.Controllers
{
    public class FilesController : Controller
    {
        async public Task<ActionResult> GetAvatar()
        {
            Users user = SharedObjects.GetCookieUserData() ?? new Users { Id = 0 };
            string file_path = Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg");
            string file_type = "image/jpeg";
            string file_name = user.Id + ".jpg";
            return File(file_path, file_type, file_name);
        }

        [HttpPost]
        async public Task<ActionResult> UpdateProfile(string nickname, HttpPostedFileBase avatarupload)
        {
            Users user = SharedObjects.GetCookieUserData();
            if (user != null)
            {
                user.Nickname = nickname;
                if(avatarupload!=null)
                    avatarupload.SaveAs(Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg"));
                SharedObjects.database.SaveChanges();
            }
            SharedObjects.SetCookieUserData(SharedObjects.database.Users.FirstOrDefault(e=>e.Id==user.Id && e.Password==user.Password));
            return new RedirectResult("/Auth/Profile");
        }

        async public Task<ActionResult> GetCssFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "text/css";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> GetJpgFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "image/jpg";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }
    }
}