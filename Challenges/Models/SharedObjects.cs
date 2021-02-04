using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Challenges.Models
{
    public class SharedObjects
    {
        public static ChallengesDBEntities database = new ChallengesDBEntities();

        public static bool CheckNonAuthorized(Users user)
        {
            if (user == null) return true;
            Users checkUser = database.Users.FirstOrDefault(e => e.Id == user.Id && e.Login == user.Login && e.Password == user.Password);
            return checkUser == null;
        }

        public static void SetCookieUserData(Users user)
        {
            if (user != null)
            {
                Users userNoCyclic = new Users
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Nickname = user.Nickname,
                    CreateDate = user.CreateDate
                };

                HttpContext.Current.Response.Cookies["UserData"].Value = new JavaScriptSerializer().Serialize(userNoCyclic);
            }
            else
                HttpContext.Current.Response.Cookies["UserData"].Value = null;
        }

        public static Users GetCookieUserData()
        {
            if (HttpContext.Current.Request.Cookies["UserData"] == null) return null;
            if (HttpContext.Current.Request.Cookies["UserData"].Value == null) return null;
            try
            {
                return new JavaScriptSerializer().Deserialize<Users>(HttpContext.Current.Request.Cookies["UserData"].Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public class ReputationComparer : Comparer<Users>
        {
            public override int Compare(Users x, Users y)
            {
                if (x.Reputation>y.Reputation)
                {
                    return -1;
                }
                else if (x.Reputation>y.Reputation)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class VideoDataResult : ActionResult
    {
        string path;

        public VideoDataResult(string filePath)
        {
            path = filePath;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + path);

            var objFile = new FileInfo(path);

            var stream = objFile.OpenRead();
            var objBytes = new byte[stream.Length];
            stream.Read(objBytes, 0, (int)objFile.Length);
            context.HttpContext.Response.BinaryWrite(objBytes);
        }
    }
}