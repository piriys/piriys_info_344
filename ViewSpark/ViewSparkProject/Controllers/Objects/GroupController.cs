using System;
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
    public class GroupController : ViewSparkController
    {

        //
        // GET: /Group/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var groups = db.Groups.Include("CreatedByUser");
                return View(groups.ToList());
            }
            else
            {
                return RedirectToAction("LogOn", "Home");
            }
        }

        //
        // GET: /Group/Details/5

        public ViewResult Details(int id)
        {
            Group gr = db.Groups.Single(g => g.ID == id);
            var members = from n in gr.GroupMembers select new { ID = n.UserID, Username = n.User.Username };
            ViewBag.Members = new SelectList(members, "ID", "Username");
            return View(gr);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            Group group = new Group();
            ViewBag.Members = new SelectList(db.Users, "ID", "Username");
            return View(group);
        } 

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Create(Group group, int[] members)
        {
            if (ModelState.IsValid)
            {
                group.CreationDate = DateTime.Now;
                group.CreatedBy = GetCurrentUserID();
                db.Groups.AddObject(group);
                db.SaveChanges();
                foreach (int x in members)
                {
                    GroupMember gp = new GroupMember();
                    gp.GroupID = group.ID;
                    gp.UserID = x;
                    db.AddToGroupMembers(gp);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Group");
            }
            return View(group);
        }
        
        //
        // GET: /Group/Edit/5
 
        public ActionResult Edit(int id)
        {
            Group group = db.Groups.Single(g => g.ID == id);
            ViewBag.Members = new SelectList(db.Users, "ID", "Username");
            return View(group);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(Group group, int[] members)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Attach(group);
                db.ObjectStateManager.ChangeObjectState(group, EntityState.Modified);
                db.SaveChanges();
                foreach (int x in members)
                {
                    GroupMember gp = new GroupMember();
                    gp.GroupID = group.ID;
                    gp.UserID = x;
                    Boolean flag = false;
                    foreach (GroupMember y in group.GroupMembers)
                    {
                        if (gp.UserID == y.UserID)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        db.AddToGroupMembers(gp);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        //
        // GET: /Group/Delete/5
 
        public ActionResult Delete(int id)
        {
            Group gr = db.Groups.Single(g => g.ID == id);
            var members = from n in gr.GroupMembers select new { ID = n.UserID, Username = n.User.Username };
            ViewBag.Members = new SelectList(members, "ID", "Username");
            return View(gr);
        }

        //
        // POST: /Group/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Group group = db.Groups.Single(g => g.ID == id);
            db.Groups.DeleteObject(group);
            db.GroupMembers.Where(g => g.GroupID == id).ToList().ForEach(db.GroupMembers.DeleteObject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}