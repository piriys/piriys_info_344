using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject.Models;

namespace ViewSparkProject.Controllers
{
    public class PresentController : ViewSparkController
    {
        //
        // GET: /Present/

        public ActionResult Index(int id = -1)
        {
            if (Request.IsAuthenticated)
            {
                if (id != -1)
                {
                    PresentModel model = new PresentModel(id);
                    ViewBag.PageState_0 = "state-inactive";
                    ViewBag.PageState_2 = "state-inactive";
                    return View(model);
                }
                else
                {
                    ViewBag.PageState_1 = "state-inactive";
                    ViewBag.PageState_2 = "state-inactive";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("LogOn", "Home");
            }
        }

    }
}
