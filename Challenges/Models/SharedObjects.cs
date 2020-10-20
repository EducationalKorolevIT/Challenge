using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenges.Models
{
    public class SharedObjects
    {
        public static ChallengesDBEntities database = new ChallengesDBEntities();
    }
}