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

        async public Task<ActionResult> GetJpgFile(string fileString)
        {
            string file_path = Server.MapPath("~/" + fileString);
            string file_type = "image/jpg";
            string file_name = "file";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> GetChallengeVideoFile(long videoId)
        {
            string file_path = Server.MapPath("~/Files/Challenges/" + videoId + ".mp4");
            string file_type = "video/mp4";
            string file_name = videoId + ".mp4";
            return File(file_path, file_type, file_name);
        }

        async public Task<ActionResult> UploadChallengePage()
        {
            return View();
        }

        async public Task<ActionResult> UploadChallengeResponsePage(long srcChallengeId = 0)
        {
            ViewBag.ChallengeId = srcChallengeId;
            return View();
        }

        [HttpPost]
        async public Task<ActionResult> UploadChallenge(string userNameTo, HttpPostedFileBase videoFile)
        {
            if (videoFile != null)
            {
                Users userTo = SharedObjects.database.Users.FirstOrDefault(e => e.Login == userNameTo);
                if (userTo != null)
                {
                    if (videoFile.ContentLength < 262144000)
                    {
                        Challenges_Table[] challenges = SharedObjects.database.Challenges_Table.ToArray();
                        Challenges_Table lastChallenge = challenges.Length > 0 ? challenges[challenges.Length - 1] : new Challenges_Table { Id = 0 };
                        long nextId = lastChallenge.Id + 1;
                        Challenges_Table challenge = new Challenges_Table()
                        {
                            Id_User_From = ((Users)Session["User"]).Id,
                            Id_User_To = userTo.Id,
                            Completed = false
                        };
                        SharedObjects.database.Challenges_Table.Add(challenge);
                        videoFile.SaveAs(Server.MapPath("~/Files/Challenges/" + nextId + ".mp4"));
                        SharedObjects.database.SaveChanges();
                        return Redirect("/Home/Index");
                    }
                }
            }

            return Redirect("/Files/UploadChallenge");
        }

        [HttpPost]
        async public Task<ActionResult> UploadChallengeResponse(long srcChallengeId, HttpPostedFileBase videoFile)
        {
            if (videoFile != null)
            {
                Challenges_Table srcChallenge = SharedObjects.database.Challenges_Table.FirstOrDefault(e => e.Id == srcChallengeId);
                if (srcChallenge != null)
                {
                    if (videoFile.ContentLength < 262144000)
                    {
                        Responses_Table[] responses = SharedObjects.database.Responses_Table.ToArray();
                        Responses_Table lastResponse = responses.Length > 0 ? responses[responses.Length - 1] : new Responses_Table { Id = 0 };
                        long nextId = lastResponse.Id + 1;
                        Responses_Table response = new Responses_Table()
                        {
                            Id_Challenges_Table = srcChallenge.Id
                        };
                        SharedObjects.database.Responses_Table.Add(response);
                        videoFile.SaveAs(Server.MapPath("~/Files/Responses/" + nextId + ".mp4"));
                        SharedObjects.database.SaveChanges();
                        return Redirect("/Home/Index");
                    }
                }
            }

            return Redirect("/Files/UploadChallenge");
        }
    }
}