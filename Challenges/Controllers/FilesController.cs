using Challenges.Models;
using Microsoft.Ajax.Utilities;
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
            Users user = (Users)Session["User"] ?? new Users { Id = 0 };
            string file_path = Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg");
            string file_type = "image/jpeg";
            string file_name = user.Id + ".jpg";
            return File(file_path, file_type, file_name);
        }

        [HttpPost]
        async public Task<ActionResult> UpdateProfile(string nickname, HttpPostedFileBase avatarupload)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            if (user != null)
            {
                user.Nickname = nickname;
                if(avatarupload!=null)
                    avatarupload.SaveAs(Server.MapPath("~/Files/Avatars/" + user.Id + ".jpg"));
                SharedObjects.database.SaveChanges();
            }
            
            return new RedirectResult("/Auth/Profile");
        }

        async public Task<ActionResult> GetCssFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "text/css";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> GetJsFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "text/javascript";
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

        async public Task<ActionResult> GetChallengeVideoFile(long videoId)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            string file_path = Server.MapPath("~/Files/Challenges/" + videoId + ".mp4");
            string file_type = "video/mp4";
            string file_name = videoId + ".mp4";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> GetChallengeResponseVideoFile(long videoId)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            string file_path = Server.MapPath("~/Files/Responses/" + videoId + ".mp4");
            string file_type = "video/mp4";
            string file_name = videoId + ".mp4";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> UploadChallengePage()
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            return View();
        }

        async public Task<ActionResult> UploadChallengeResponsePage(long srcChallengeId = 0)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            ViewBag.ChallengeId = srcChallengeId;
            return View();
        }

        [HttpPost]
        async public Task<ActionResult> UploadChallenge(string userNameTo, string challengeDescription, HttpPostedFileBase videoFile)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            if (videoFile != null)
            {
                Users userTo = SharedObjects.database.Users.FirstOrDefault(e => e.Login == userNameTo);
                if (userTo != null)
                {
                    if (videoFile.ContentLength < 262144000)
                    {
                        Challenges_Table challenge = new Challenges_Table()
                        {
                            Id_User_From = ((Users)Session["User"]).Id,
                            Id_User_To = userTo.Id,
                            Description = challengeDescription,
                            Status = 0
                        };
                        SharedObjects.database.Challenges_Table.Add(challenge);
                        SharedObjects.database.SaveChanges();

                        videoFile.SaveAs(Server.MapPath("~/Files/Challenges/" + challenge.Id + ".mp4"));
                        return Redirect("/Home/Index");
                    }
                }
            }

            return Redirect("/Files/UploadChallenge");
        }

        [HttpPost]
        async public Task<ActionResult> UploadChallengeResponse(long srcChallengeId, HttpPostedFileBase videoFile)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            if (videoFile != null)
            {
                Challenges_Table srcChallenge = SharedObjects.database.Challenges_Table.FirstOrDefault(e => e.Id == srcChallengeId);
                if (srcChallenge != null)
                {
                    if (videoFile.ContentLength < 262144000)
                    {
                        Responses_Table response = new Responses_Table()
                        {
                            Id_Challenges_Table = srcChallenge.Id
                        };
                        SharedObjects.database.Responses_Table.Add(response);
                        srcChallenge.Status = 1;
                        Users userDB = SharedObjects.database.Users.FirstOrDefault(e => e.Id == user.Id && e.Login == user.Login && e.Password == user.Password);
                        userDB.Reputation += 15;
                        SharedObjects.database.SaveChanges();

                        videoFile.SaveAs(Server.MapPath("~/Files/Responses/" + response.Id + ".mp4"));
                        return Redirect("/Home/Index");
                    }
                }
            }

            return Redirect("/Files/UploadChallenge");
        }

        public ActionResult CancelChallengePage(int chId)
        {
            ViewBag.chId = chId;
            return View();
        }

        public ActionResult CancelChallenge(int chId)
        {
            Users user = (Users)Session["User"];
            if (SharedObjects.CheckNonAuthorized(user)) return new HttpUnauthorizedResult();
            Challenges_Table srcChallenge = SharedObjects.database.Challenges_Table.FirstOrDefault(e => e.Id == chId);
            if (srcChallenge == null) return new HttpNotFoundResult("Такого челленджа не существует.");
            srcChallenge.Status = 2;
            Users userDB = SharedObjects.database.Users.FirstOrDefault(e => e.Id == user.Id && e.Login == user.Login && e.Password == user.Password);
            userDB.Reputation -= 10;
            SharedObjects.database.SaveChanges();
            return Redirect("/Home/CompetitorsIncoming");
        }
    }
}