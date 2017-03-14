using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewSparkProject.Controllers
{
    public class ViewController : ViewSparkController
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogOn", "Home");
            }
        }

        public ActionResult Presentation()
        {
            return View();
        }
    }
}
