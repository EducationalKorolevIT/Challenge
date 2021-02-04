using Challenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Challenges.Controllers
{
    public class APIController : Controller
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        class UserCredentials
        {
            public string login;
            public string password;
        }

        public ActionResult GetChallenges(string json)
        {
            string responseData = "";
            Challenges_Table[] challenges = SharedObjects.database.Challenges_Table.ToArray();
            List<Challenges_Table> challengesTrim = new List<Challenges_Table>();
            foreach(Challenges_Table ch in challenges)
            {
                Challenges_Table trim = new Challenges_Table
                {
                    Description = ch.Description,
                    Id = ch.Id,
                    Id_User_From = ch.Id_User_From,
                    Id_User_To = ch.Id_User_To,
                    Status = ch.Status
                };
                challengesTrim.Add(trim);
            }
            responseData = jsonSerializer.Serialize(challengesTrim.ToArray());
            return Content(responseData, "application/json");
        }

        public ActionResult GetUserProfile(string json)
        {
            string responseData = "";
            UserCredentials userData = jsonSerializer.Deserialize<UserCredentials>(json);
            if(userData.login == null || userData.password == null)
                return Content("{\"errorMessage\": \"Wrong credentials\"}", "application/json");

            Users user = SharedObjects.database.Users.FirstOrDefault(e => e.Login == userData.login && e.Password == userData.password);
            if (user == null)
                return Content("{\"errorMessage\": \"Wrong credentials\"}", "application/json");

            Users trim = new Users
            {
                Login = user.Login,
                Password = user.Password,
                CreateDate = user.CreateDate,
                Id = user.Id,
                Nickname = user.Nickname,
                Reputation = user.Reputation
            };

            responseData = jsonSerializer.Serialize(trim);
            return Content(responseData, "application/json");
        }
    }
}