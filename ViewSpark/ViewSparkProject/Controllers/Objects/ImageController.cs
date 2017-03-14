using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject;
using System.IO;
using System.Drawing;

namespace ViewSparkProject.Controllers
{ 
    public class ImageController : ViewSparkController
    {

        //
        // GET: /Image/

        public ViewResult Index()
        {
            var images = db.Images.Include("CreatedByUser").Include("ModifiedByUser");
            return View(images.ToList());
        }

        //
        // GET: /Image/Details/5

        public ViewResult Details(int id)
        {
            Image image = db.Images.Single(i => i.ID == id);
            return View(image);
        }

        //
        // GET: /Image/Create

        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username");
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username");
            return View();
        }

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult Create(Image image, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid && imageFile != null)
            {
                //
                // todo, verify mimetypes
                //

                image.MimeType = imageFile.ContentType;
                
                MemoryStream ms = new MemoryStream();
                imageFile.InputStream.CopyTo(ms);
                image.Data = ms.ToArray();
                image.CreationDate = DateTime.Now;
                image.ModificationDate = DateTime.Now;
                image.Url = "";

                db.Images.AddObject(image);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", image.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", image.ModifiedBy);
            return View(image);
        }
        
        //
        // GET: /Image/Edit/5
 
        public ActionResult Edit(int id)
        {
            Image image = db.Images.Single(i => i.ID == id);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", image.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", image.ModifiedBy);
            return View(image);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Attach(image);
                db.ObjectStateManager.ChangeObjectState(image, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", image.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.Users, "ID", "Username", image.ModifiedBy);
            return View(image);
        }

        //
        // GET: /Image/Delete/5
 
        public ActionResult Delete(int id)
        {
            Image image = db.Images.Single(i => i.ID == id);
            return View(image);
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Image image = db.Images.Single(i => i.ID == id);
            db.Images.DeleteObject(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        /// <summary>
        /// Returns the image data stored from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult View(int id)
        {
            Image image = db.Images.Single(i => i.ID == id);
            if (image != null)
            {
                return File(image.Data, image.MimeType);
            }

            return null;
        }

        /// <summary>
        /// Resizes an image and returns the result
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public FileContentResult Resize(int id, int width, int height)
        {
            try
            {

                Image image = db.Images.Single(i => i.ID == id);
                if (image != null)
                {
                    using (MemoryStream msImage = new MemoryStream(image.Data))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(msImage))
                        {
                            int newWidth = img.Width;
                            int newHeight = img.Height;

                            if (newWidth > width)
                            {
                                newHeight = (int)Math.Floor((((double)width / (double)newWidth) * (double)newHeight));
                                newWidth = width;
                            }

                            if (newHeight > height)
                            {
                                newWidth = (int)Math.Floor((((double)height / (double)newHeight) * (double)newWidth));
                                newHeight = height;
                            }

                            System.Drawing.Image resizedImage = new System.Drawing.Bitmap(newWidth, newHeight, img.PixelFormat);
                            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(resizedImage);
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));

                            MemoryStream newFile = new MemoryStream();
                            resizedImage.Save(newFile, img.RawFormat);

                            return File(newFile.ToArray(), image.MimeType);
                        }
                    }

                    
                }

            }
            catch (Exception)
            {

            }

            return null;
        }

    }
}