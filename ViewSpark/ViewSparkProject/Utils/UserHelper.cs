using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ViewSparkProject.Utils
{
    public static class UserHelper
    {
        public static User GetCurrentUser(Controller c, ViewSparkEntities db)
        {
            if (c.Request.IsAuthenticated && c.User != null && c.User.Identity != null && !string.IsNullOrEmpty(c.User.Identity.Name))
            {
                return db.Users.Single(u => u.Username == c.User.Identity.Name);
            }

            return null;
        }



        /// <summary>
        /// Safely grab the user ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static int GetUserID(Controller c, ViewSparkEntities db)
        {
            User u = GetCurrentUser(c, db);

            return (u != null) ? u.ID : -1;
        }



        /// <summary>
        /// Set the user cookie
        /// </summary>
        /// <param name="response"></param>
        /// <param name="u"></param>
        public static void SetUserCookie(HttpResponseBase response, User u)
        {
            FormsAuthentication.SetAuthCookie(u.Username, true);
        }





    }
}
