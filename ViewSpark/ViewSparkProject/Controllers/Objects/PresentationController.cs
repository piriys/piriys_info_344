using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject;

namespace ViewSparkProject.Controllers
{ 
    public class PresentationController : ViewSparkController
    {
        //
        // GET: /Presentation/

        public ViewResult Index()
        {
            var presentations = db.Presentations.Include("CreatedByUser").Include("ModifiedByUser");
            return View(presentations.ToList());
        }

        //
        // GET: /Presentation/Details/5

        public ViewResult Details(int id)
        {
            Presentation presentation = db.Presentations.Single(p => p.ID == id);
            return View(presentation);
        }

        //
        // GET: /Presentation/Create

        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username");
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username");
            return View();
        } 

        //
        // POST: /Presentation/Create

        [HttpPost]
        public ActionResult Create(Presentation presentation)
        {
            int uid = Utils.UserHelper.GetUserID(this, db);

            if (presentation != null && uid >= 0)
            {
                presentation.ModificationDate = DateTime.Now;
                presentation.ModifiedBy = uid;
                presentation.CreationDate = DateTime.Now;
                presentation.CreatedBy = uid;

                if (ModelState.IsValid)
                {
                    db.Presentations.AddObject(presentation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", presentation.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", presentation.ModifiedBy);
            return View(presentation);
        }
        
        //
        // GET: /Presentation/EditDetails/5
 
        public ActionResult EditDetails(int id)
        {
            Presentation presentation = db.Presentations.Single(p => p.ID == id);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", presentation.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", presentation.ModifiedBy);
            return View(presentation);
        }

        //
        // POST: /Presentation/EditDetails/5

        [HttpPost]
        public ActionResult EditDetails(Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                db.Presentations.Attach(presentation);
                db.ObjectStateManager.ChangeObjectState(presentation, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", presentation.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", presentation.ModifiedBy);
            return View(presentation);
        }

        //
        // GET: /Presentation/Delete/5
 
        public ActionResult Delete(int id)
        {
            Presentation presentation = db.Presentations.Single(p => p.ID == id);
            return View(presentation);
        }

        //
        // POST: /Presentation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Presentation presentation = db.Presentations.Single(p => p.ID == id);
            db.Presentations.DeleteObject(presentation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /Presentation/Edit/5

        public ActionResult Edit(int id)
        {
            Presentation presentation = db.Presentations.Include("PresentationSlides").Single(p => p.ID == id);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", presentation.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", presentation.ModifiedBy);
            return View(presentation);
        }

        //
        // POST: /Presentation/Edit/5

        [HttpPost]
        public ActionResult Edit(Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                presentation.ModificationDate = DateTime.Now;
                db.Presentations.Attach(presentation);
                db.ObjectStateManager.ChangeObjectState(presentation, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", presentation.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", presentation.ModifiedBy);
            return View(presentation);
        }






        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPresentations()
        {
            var ownedPresentations = db.Presentations.Where(p => p.CreatedBy == CurrentUser.ID).OrderByDescending(p => p.ModificationDate).Take(5);

            return this.Json(ownedPresentations, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PresentationSlides(int id)
        {
            try
            {
                var p = db.Presentations.Include("PresentationSlides.Slide").Single(x => x.CreatedBy == CurrentUser.ID && x.ID == id);

                var presSlides = new
                {
                    ID = p.ID,
                    Title = p.Title,
                    Status = p.Status,
                    CreatedBy = p.CreatedBy,
                    CreationDate = p.CreationDate.ToString(),
                    ModifiedBy = p.ModifiedBy,
                    ModificationDate = p.ModificationDate.ToString(),
                    PresentationSlides = from ps in p.PresentationSlides
                                         orderby ps.OrderNum
                                         select new
                                         {
                                             ID = ps.ID,
                                             SlideID = ps.SlideID,
                                             PresentationID = ps.PresentationID,
                                             PresentationDate = ps.PresentationDate.ToString(),
                                             OrderNum = ps.OrderNum,
                                             Slide = new
                                             {
                                                 ID = ps.Slide.ID,
                                                 Title = ps.Slide.Title,
                                                 PresentationDate = ps.Slide.PresentationDate,
                                                 CreatedBy = ps.Slide.CreatedBy,
                                                 CreationDate = ps.Slide.CreationDate.ToString(),
                                                 ModifiedBy = ps.Slide.ModifiedBy,
                                                 ModificationDate = ps.Slide.ModificationDate.ToString(),
                                                 HtmlContentHtmlString = ps.Slide.HtmlContentString
                                             }
                                         }
                };

                //return Newtonsoft.Json.Linq.JObject.FromObject(ownedPresentations);

                return this.Json(presSlides, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SlideID"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Slide(int id, string Title)
        {
            var presentation = db.Presentations.Single(p => p.CreatedBy == CurrentUser.ID && p.ID == id);

            if (presentation != null)
            {
                Slide s = new Slide();
                s.Content = "";
                s.CreatedBy = CurrentUserID;
                s.CreationDate = DateTime.Now;
                s.ModificationDate = DateTime.Now;
                s.ModifiedBy = CurrentUserID;
                s.Title = Title;
                db.Slides.AddObject(s);
                db.SaveChanges();

                PresentationSlide ps = new PresentationSlide();
                ps.PresentationID = presentation.ID;
                ps.SlideID = s.ID;
                ps.OrderNum = db.PresentationSlides.Where(x => x.PresentationID == id).Count() + 1;
                db.PresentationSlides.AddObject(ps);
                db.SaveChanges();

                return this.Json(new
                {
                    ID = ps.ID,
                    PresentationID = ps.PresentationID,
                    SlideID = ps.SlideID,
                    OrderNum = ps.OrderNum,
                    Slide = new
                    {
                        ID = ps.Slide.ID,
                        HtmlContentString = ps.Slide.HtmlContentHtmlString.ToHtmlString(),
                        Title = ps.Slide.Title,
                        Layout = ps.Slide.Layout,
                        ModificationDate = ps.Slide.ModificationDate.ToString(),
                        ModifiedBy = ps.Slide.ModifiedBy,
                        CreationDate = ps.Slide.CreationDate.ToString(),
                        CreatedBy = ps.Slide.CreatedBy
                    }
                });

            }

            return this.Json( new { result = false } );
        }


        [HttpDelete,ActionName("Slide")]
        public JsonResult SlideDelete(int id, int slideID)
        {
            PresentationSlide ps = db.PresentationSlides.Include("Slide").Single(p => p.PresentationID == id && p.SlideID == slideID);
            db.PresentationSlides.DeleteObject(ps);

            int cnt = db.PresentationSlides.Where(p => p.SlideID == slideID).Count();
            if (cnt == 0)
            {
                db.Slides.DeleteObject(ps.Slide);
            }
            db.SaveChanges();

            return this.Json(new { result = true });
        }



        [HttpDelete, ActionName("Index")]
        public JsonResult PresentationDelete(int id)
        {
            Presentation p = db.Presentations.Single(x => x.ID == id);
            db.Presentations.DeleteObject(p);
            db.SaveChanges();

            return this.Json(new { result = true });
        }


        [HttpPut, ActionName("Index")]
        public JsonResult PresentationPut(int id, string title)
        {
            return this.Json(new { result = true });
        }



        public ActionResult CreateDlg()
        {
            Presentation p = new Presentation();

            return PartialView(p);
        }

        [HttpPost, ActionName("CreateDlg")]
        public ActionResult CreateDlgPost(Presentation p, string div)
        {
            p.CreatedBy = CurrentUserID;
            p.CreationDate = DateTime.Now;
            p.ModifiedBy = CurrentUserID;
            p.ModificationDate = DateTime.Now;
            p.Status = 0;

            if (ModelState.IsValid)
            {
                db.Presentations.AddObject(p);
                db.SaveChanges();
                return JavaScript("closeDialog(); refreshPresentations('Edit');");
            }

            return PartialView();
        }

        public ActionResult DeleteDlg(int id, string view)
        {
            ViewBag.presentationID = id;
            try
            {
                Presentation presentation = db.Presentations.Single(p => p.ID == id);
                ViewBag.ConfirmDelete = "Permanently delete [" + presentation.Title + "] presentation?";
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
                Presentation presentation = db.Presentations.Single(p => p.ID == id);
                ViewBag.ItemTitle = presentation.Title;
                db.Presentations.DeleteObject(presentation);

                var ps = db.PresentationSlides.Where(s => s.PresentationID == id);
                foreach (var item in ps)
                {
                    db.PresentationSlides.DeleteObject(item);
                }

                db.SaveChanges();

                return JavaScript("closeDialog(); refreshPresentations('" + view + "'); refreshQueuedSlides("+ id + ", '" + view + "');");
            }

            return PartialView();
        }
    }
}
