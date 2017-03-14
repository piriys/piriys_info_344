using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewSparkProject.Utils;

namespace ViewSparkProject.Controllers
{
    public class TestController : ViewSparkController
    {
        //
        // GET: /Test/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.divid = "pagebody";
            if (Request.Cookies["Test"] == null)
            {
                Response.Cookies["Test"].Value = "ing";
                Response.Cookies["Test2"].Value = "abc";
            }
            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DummyCookie()
        {
            ViewBag.ID = new SelectList(db.Users, "ID", "Username");

            User u = CurrentUser ?? new User();

            

            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DummyCookie")]
        public ActionResult DummyCookiePost(User u)
        {
            User fullData = db.Users.Single(usr => usr.ID == u.ID);

            if(fullData != null && fullData.ID > -1 )
            {
                Utils.UserHelper.SetUserCookie(Response, fullData);

                return RedirectToAction("Index");
            }

            return View(u);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index");
        }
    }
}
