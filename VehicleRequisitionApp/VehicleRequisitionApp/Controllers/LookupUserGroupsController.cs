using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionApp.Models;

namespace VehicleRequisitionApp.Controllers
{
    public class LookupUserGroupsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookupUserGroups
        public ActionResult Index()
        {
            return View(db.LookupUserGroups.ToList());
        }

        // GET: LookupUserGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupUserGroup lookupUserGroup = db.LookupUserGroups.Find(id);
            if (lookupUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(lookupUserGroup);
        }

        // GET: LookupUserGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookupUserGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGroupsId,SortingSerialNo,UserGroupName")] LookupUserGroup lookupUserGroup)
        {
            if (ModelState.IsValid)
            {
                db.LookupUserGroups.Add(lookupUserGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookupUserGroup);
        }

        // GET: LookupUserGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupUserGroup lookupUserGroup = db.LookupUserGroups.Find(id);
            if (lookupUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(lookupUserGroup);
        }

        // POST: LookupUserGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserGroupsId,SortingSerialNo,UserGroupName")] LookupUserGroup lookupUserGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookupUserGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookupUserGroup);
        }

        // GET: LookupUserGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupUserGroup lookupUserGroup = db.LookupUserGroups.Find(id);
            if (lookupUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(lookupUserGroup);
        }

        // POST: LookupUserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookupUserGroup lookupUserGroup = db.LookupUserGroups.Find(id);
            db.LookupUserGroups.Remove(lookupUserGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
