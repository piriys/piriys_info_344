using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject.Models;

namespace ViewSparkProject.Controllers
{

    public class SharedController : ViewSparkController
    {
        public ActionResult _Chat()
        {
            return PartialView();
        }


        public ActionResult _LogOnState()
        {
            return PartialView();
        }

        public ActionResult _FileBrowser(string view, string filetype, int page, int itemPerPage)
        {
            if (filetype == "Slides")
            {
                var ownedSlides = db.Slides.Where(p => p.CreatedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate);
                _FileBrowserModel model = new _FileBrowserModel(view, filetype, Json(ownedSlides.Skip((page - 1) * itemPerPage).Take(itemPerPage)), ownedSlides.Count(), itemPerPage, page);
                ViewBag.OwnedFiles = db.Slides.Where(p => p.CreatedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate).Skip((page - 1) * itemPerPage).Take(itemPerPage);

                return PartialView(model);
            }
            //else if(filetype == "polls")
            //{
            //    var ownedpolls = db.polls.where(p => p.createdby == currentuser.id).orderbydescending(p => p.modificationdate).take(itemPerPage);
            //    var modifiedPolls = db.Polls.Where(p => p.ModifiedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate).Take(itemPerPage);     
            //}
            else //Presentations
            {
                var ownedPresentations = db.Presentations.Where(p => p.CreatedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate);
                _FileBrowserModel model = new _FileBrowserModel(view, filetype, Json(ownedPresentations.Skip((page - 1) * itemPerPage).Take(itemPerPage)), ownedPresentations.Count(), itemPerPage, page);
                ViewBag.OwnedFiles = db.Presentations.Where(p => p.CreatedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate).Skip((page - 1) * itemPerPage).Take(itemPerPage);

                return PartialView(model);
            }
        }

        public ActionResult _Slide(string view, int id)
        {
            var slide = db.Slides.Where(p => p.CreatedBy == CurrentUser.ID).Where(p => p.ID == id).SingleOrDefault();
            ViewBag.CurrentSlide = id;

            if (slide != null)
            {
                _SlideModel model = new _SlideModel(slide, view);
                return PartialView(model);
            }
            else
            {
                return PartialView();
            }
        }

        public PartialViewResult _QueuedSlides(int id, int page, int maxColumn, int maxRow, string view)
        {
            try
            {
                Presentation presentation = db.Presentations.Include("PresentationSlides").Single(p => p.ID == id);
                ViewBag.PresentationTitle = db.Presentations.Single(p => p.ID == id).Title;
                ViewBag.QueuedSlides = presentation.PresentationSlides.Skip((page - 1) * (maxColumn * maxRow)).Take((maxColumn * maxRow));
                ViewBag.PresentationID = presentation.ID;
                
                _QueuedSlidesModel model = new _QueuedSlidesModel(id, presentation.PresentationSlides.Count, page, presentation.Title, view, maxColumn, maxRow);
                if (presentation != null)
                {
                    return PartialView(model);
                }
                else 
                {
                    return PartialView();
                }      
            }
            catch
            {
                return PartialView();
            }
        }
    }
}
