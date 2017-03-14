using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ViewSparkProject.Models;

namespace ViewSparkProject.Controllers
{
    public class EditController : ViewSparkController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="edit">Filetype</param>
        /// <returns></returns>
        public ActionResult Index(string type = "none", int id = -1)
        {
            if (Request.IsAuthenticated)
            {
                    EditModel model = new EditModel(type, id);
                    ViewBag.SlideTitle = "Slide Editor"; 

                    if (model.type == "presentation")
                    {
                        ViewBag.PageState_1 = "state-inactive";
                        ViewBag.PageState_2 = "state-inactive";
                        return View(model);
                    }
                    else if (model.type == "poll")
                    {
                        ViewBag.PageState_0 = "state-inactive";
                        ViewBag.PageState_1 = "state-inactive";
                        return View(model);
                    }
                    else if (model.type == "slide")
                    {
                       ViewBag.PageState_0 = "state-inactive";
                       ViewBag.PageState_2 = "state-inactive";
                      
                        try
                       {
                           ViewBag.SlideTitle = db.Slides.Where(p => p.CreatedBy == CurrentUser.ID).Where(p => p.ID == id).SingleOrDefault().Title.ToString();
                       }
                       catch
                       {

                       }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public PartialViewResult SlideList(int id, int page)
        {
            Presentation presentation = db.Presentations.Include("PresentationSlides.Slide").Single(p => p.ID == id);
            ViewBag.Presentation = presentation;

            return PartialView();
        }
    }
}
