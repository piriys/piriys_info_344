﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject;
using ViewSparkProject.Models;

namespace ViewSparkProject.Controllers
{
    public class PollController : ViewSparkController
    {
        //
        // GET: /Poll/

        public ViewResult Index()
        {
            var polls = db.Polls.Include("CreatedByUser");
            return View(polls.ToList());
        }

        //
        // GET: /Poll/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Poll/Create

        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username");
            return View();
        } 

        //
        // POST: /Poll/Create

        [HttpPost]
        public ActionResult Create(Poll poll)
        {
            int uid = Utils.UserHelper.GetUserID(this, db);

            if (poll != null && uid >= 0)
            {
                poll.CreationDate = DateTime.Now;
                poll.CreatedBy = uid;

                if (ModelState.IsValid)
                {
                    db.Polls.AddObject(poll);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Username", poll.CreatedBy);
            return View(poll);
        }
        
        //
        // GET: /Poll/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Poll/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Poll/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Poll/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
