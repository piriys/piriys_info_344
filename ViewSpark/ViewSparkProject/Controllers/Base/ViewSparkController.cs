using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewSparkProject.Controllers
{
    public class ViewSparkController : Controller
    {
        // database object
        protected ViewSparkEntities db = new ViewSparkEntities();
        private User currentUser = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            return Utils.UserHelper.GetCurrentUser(this, db);
        }


        public int GetCurrentUserID()
        {
            return Utils.UserHelper.GetUserID(this, db);
        }


        // vague singleton like pattern, though I don't think this occurs in the MVC controller stuff
        // but be safe and protected against repeated calls
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    lock(syncRoot)
                    {
                        if (currentUser == null)
                        {
                            currentUser = GetCurrentUser();
                        }
                    }
                }

                return currentUser;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public int CurrentUserID
        {
            get
            {
                User c = CurrentUser;
                return c != null ? c.ID : -1;
            }
        }




        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            object divid = RouteData.Values["divid"];
            ViewBag.divid = (divid != null) ? (divid.ToString()) : null;
            base.OnActionExecuting(ctx);
            //ViewBag.DivID = RouteData
        }

        /// <summary>
        /// Tear down database connections
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
