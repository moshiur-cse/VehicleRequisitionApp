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
    public class LookupProjectsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookupProjects
        public ActionResult Index()
        {
            return View(db.LookupProjects.ToList());
        }

        // GET: LookupProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProject lookupProject = db.LookupProjects.Find(id);
            if (lookupProject == null)
            {
                return HttpNotFound();
            }
            return View(lookupProject);
        }

        // GET: LookupProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookupProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,SortingSerialNo,ProjectCode,ProjectName,ProjectClient,ProjectCluster,ProjectPl")] LookupProject lookupProject)
        {
            if (ModelState.IsValid)
            {
                db.LookupProjects.Add(lookupProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookupProject);
        }

        // GET: LookupProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProject lookupProject = db.LookupProjects.Find(id);
            if (lookupProject == null)
            {
                return HttpNotFound();
            }
            return View(lookupProject);
        }

        // POST: LookupProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]



        public ActionResult Edit([Bind(Include = "ProjectId,SortingSerialNo,ProjectCode,ProjectName,ProjectClient,ProjectCluster,ProjectPl")] LookupProject lookupProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookupProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookupProject);
        }

        // GET: LookupProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProject lookupProject = db.LookupProjects.Find(id);
            if (lookupProject == null)
            {
                return HttpNotFound();
            }
            return View(lookupProject);
        }

        // POST: LookupProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookupProject lookupProject = db.LookupProjects.Find(id);
            db.LookupProjects.Remove(lookupProject);
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
