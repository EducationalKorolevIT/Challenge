using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Challenges.Models
{
    public class SharedObjects
    {
        public static ChallengesDBEntities database = new ChallengesDBEntities();
        public static void SetCookieUserData(Users user)
        {
            if (user != null)
                HttpContext.Current.Response.Cookies["User"].Value = new JavaScriptSerializer().Serialize(user);
            else
                HttpContext.Current.Response.Cookies["User"].Value = null;
        }

        public static Users GetCookieUserData()
        {
            try
            {
                return new JavaScriptSerializer().Deserialize<Users>(HttpContext.Current.Request.Cookies["User"].Value);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}