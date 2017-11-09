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
    public class LookupProjectLeadersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookupProjectLeaders
        public ActionResult Index()
        {
            var lookupProjectLeaders = db.LookupProjectLeaders.Include(l => l.LookUpEmployee).Include(l => l.LookupProject);
            return View(lookupProjectLeaders.ToList());
        }

        // GET: LookupProjectLeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProjectLeader lookupProjectLeader = db.LookupProjectLeaders.Find(id);
            if (lookupProjectLeader == null)
            {
                return HttpNotFound();
            }
            return View(lookupProjectLeader);
        }

        // GET: LookupProjectLeaders/Create
        public ActionResult Create()
        {
            ViewBag.EmpInfo = db.LookUpEmployees.Select(i => new
            {
                empId = i.EmpId,
                empInitialAndName = i.EmpInitial + " : " + i.EmpFullName
            }).ToList();

            ViewBag.ProjectInfo = db.LookupProjects.Select(i => new
            {
                ProjId = i.ProjectId,
                CodeAndTitle = i.ProjectCode + " : "+i.ProjectTitle

            }).ToList();
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
            return View();
        }

        // POST: LookupProjectLeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectLeaderId,SortingSerialNo,EmpId,ProjectId")] LookupProjectLeader lookupProjectLeader)
        {
            if (ModelState.IsValid)
            {
                db.LookupProjectLeaders.Add(lookupProjectLeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpInfo = db.LookUpEmployees.Select(i => new
            {
                empId=i.EmpId,
                empInitialAndName = i.EmpInitial + " : " + i.EmpFullName
            }).ToList();

            ViewBag.ProjectInfo = db.LookupProjects.Select(i => new
            {
                ProjId = i.ProjectId,
                CodeAndTitle=i.ProjectCode+" : "+i.ProjectTitle

            }).ToList();



            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", lookupProjectLeader.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", lookupProjectLeader.ProjectId);
            return View(lookupProjectLeader);
        }

        // GET: LookupProjectLeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProjectLeader lookupProjectLeader = db.LookupProjectLeaders.Find(id);
            if (lookupProjectLeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", lookupProjectLeader.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", lookupProjectLeader.ProjectId);
            return View(lookupProjectLeader);
        }

        // POST: LookupProjectLeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectLeaderId,SortingSerialNo,EmpId,ProjectId")] LookupProjectLeader lookupProjectLeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookupProjectLeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", lookupProjectLeader.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", lookupProjectLeader.ProjectId);
            return View(lookupProjectLeader);
        }

        // GET: LookupProjectLeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupProjectLeader lookupProjectLeader = db.LookupProjectLeaders.Find(id);
            if (lookupProjectLeader == null)
            {
                return HttpNotFound();
            }
            return View(lookupProjectLeader);
        }

        // POST: LookupProjectLeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookupProjectLeader lookupProjectLeader = db.LookupProjectLeaders.Find(id);
            db.LookupProjectLeaders.Remove(lookupProjectLeader);
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
