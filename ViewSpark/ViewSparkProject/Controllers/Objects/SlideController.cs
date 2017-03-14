using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject;
using Microsoft.Security.Application;
using Microsoft.Exchange.Data.TextConverters;

namespace ViewSparkProject.Controllers
{ 
    public class SlideController : ViewSparkController
    {

        //
        // GET: /Slide/

        public ViewResult Index()
        {
            var slides = db.Slides.Include("CreatedByUser").Include("ModifiedByUser");
            return View(slides.ToList());
        }

        //
        // GET: /Slide/Details/5

        public ViewResult Details(int id)
        {
            Slide slide = db.Slides.Single(s => s.ID == id);
            return View(slide);
        }

        //
        // GET: /Slide/Create

        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username");
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username");
            return View();
        } 

        //
        // POST: /Slide/Create

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.AddObject(slide);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", slide.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", slide.ModifiedBy);
            return View(slide);
        }


        public ActionResult CreateForPresentation(int id)
        {
            Slide s = new Slide();

            s.CreatedBy = CurrentUserID;
            s.CreationDate = DateTime.Now;
            s.ModifiedBy = CurrentUserID;
            s.ModificationDate = DateTime.Now;


            ViewBag.PresentationID = id;

            return View(s);
        }


        [HttpPost]
        public ActionResult CreateForPresentation(int id, Slide s)
        {

            s.CreationDate = DateTime.Now;
            s.ModificationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Slides.AddObject(s);
                db.SaveChanges();

                System.Diagnostics.Debug.Print("slide has id of {0}", s.ID);

                PresentationSlide ps = new PresentationSlide();
                ps.PresentationID = id;
                ps.SlideID = s.ID;
                int slideCount = db.PresentationSlides.Where(pslide => pslide.PresentationID == id).Count();
                ps.OrderNum = slideCount;
                db.PresentationSlides.AddObject(ps);
                s.PresentationSlides.Add(ps);
                db.SaveChanges();

                return RedirectToAction("Edit","Presentation", new { id=id });
            }

            return View(s);
        }


        
        //
        // GET: /Slide/Edit/5
 
        public ActionResult Edit(int id)
        {
            Slide slide = db.Slides.Single(s => s.ID == id);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", slide.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", slide.ModifiedBy);
            return View(slide);
        }

        //
        // POST: /Slide/Edit/5

        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Attach(slide);
                db.ObjectStateManager.ChangeObjectState(slide, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", slide.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", slide.ModifiedBy);
            return View(slide);
        }

        //
        // GET: /Slide/Delete/5
 
        public ActionResult Delete(int id)
        {
            Slide slide = db.Slides.Single(s => s.ID == id);
            return View(slide);
        }

        //
        // POST: /Slide/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Slide slide = db.Slides.Single(s => s.ID == id);
            db.Slides.DeleteObject(slide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult UserSlides(int id)
        {
            var presSlides = db.PresentationSlides.Where(ps => ps.PresentationID == id).OrderByDescending(ps => ps.Presentation.ModificationDate);

            return this.Json(presSlides);
        }


        public PartialViewResult RawSlide(int id)
        {
            var presSlides = db.Slides.Single(ps => ps.ID == id);
            ViewBag.Slide = presSlides.HtmlContentHtmlString;

            return PartialView();
        }

        public JsonResult GetPresentationSlide(int id)
        {
            Slide slide = db.Slides.Single(s => s.ID == id);

            return this.Json(new 
            { 
                ID= slide.ID,
                HtmlContentString = slide.HtmlContentHtmlString.ToHtmlString(),
                Title = slide.Title,
                Layout = slide.Layout,
                ModificationDate = slide.ModificationDate.ToString(),
                ModifiedBy = slide.ModifiedBy,
                CreationDate = slide.CreationDate.ToString(),
                CreatedBy = slide.CreatedBy
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Update(Slide s)
        {

            if (ModelState.IsValid)
            {
                db.Slides.Attach(s);
                db.ObjectStateManager.ChangeObjectState(s, EntityState.Modified);
                db.SaveChanges();
                return this.Json(new { success = true });
            }

            return this.Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// MVC/Bootstrap create dlg
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDlg(int id)
        {
            Slide s = new Slide();

            ViewBag.presentationID = id;
            return PartialView(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        [HttpPost, ActionName("CreateDlg")]
        public ActionResult CreateDlgPost(Slide s, int id, string div)
        {
            s.CreatedBy = CurrentUserID;
            s.CreationDate = DateTime.Now;
            s.ModifiedBy = CurrentUserID;
            s.ModificationDate = DateTime.Now;
            s.Content = ""; // fill with model/template perhaps?

            if (ModelState.IsValid)
            {
                db.Slides.AddObject(s);
                db.SaveChanges();

                PresentationSlide ps = new PresentationSlide();
                ps.OrderNum = db.PresentationSlides.Where(x => x.PresentationID == id).Count();
                ps.PresentationID = id;
                ps.SlideID = s.ID;
                db.PresentationSlides.AddObject(ps);
                db.SaveChanges();


                return JavaScript("closeDialog(); refreshQueuedSlides("+ id + ", " + "'Edit'" + ");");
            }

            return PartialView();
        }

        public ActionResult DeleteDlg(int id, string view)
        {
            ViewBag.slideID = id;
            try
            {
                Slide slide = db.Slides.Single(s => s.ID == id);
                ViewBag.ConfirmDelete = "Permanently delete [" + slide.Title + "] slide?";
            }
            catch
            {
                ViewBag.ConfirmDelete = "Invalid Slide ID.";
            }
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteDlg")]
        public ActionResult DeleteDlgPost(int id, string view)
        {
            if (ModelState.IsValid)
            {
                Slide slide = db.Slides.Single(s => s.ID == id);
                var ps = db.PresentationSlides.Where(s => s.SlideID == id);

                ViewBag.ItemTitle = slide.Title; 

                db.Slides.DeleteObject(slide);
                foreach (var item in ps)
                {
                    db.PresentationSlides.DeleteObject(item);
                }
                db.SaveChanges();

                return JavaScript("closeDialog(); refreshSlidesBrowser('" + view + "')");
            }

            return PartialView();
        }
    }
}