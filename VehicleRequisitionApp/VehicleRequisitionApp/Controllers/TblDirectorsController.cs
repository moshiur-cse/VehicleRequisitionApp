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
    public class TblDirectorsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblDirectors
        public ActionResult Index()
        {
            var tblDirectors = db.TblDirectors.Include(t => t.LookUpEmployee);
            return View(tblDirectors.ToList());
        }

        // GET: TblDirectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDirector tblDirector = db.TblDirectors.Find(id);
            if (tblDirector == null)
            {
                return HttpNotFound();
            }
            return View(tblDirector);
        }

        // GET: TblDirectors/Create
        public ActionResult Create()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }

        // POST: TblDirectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DirectorId,EmpId")] TblDirector tblDirector)
        {
            if (ModelState.IsValid)
            {
                db.TblDirectors.Add(tblDirector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblDirector.EmpId);
            return View(tblDirector);
        }

        // GET: TblDirectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDirector tblDirector = db.TblDirectors.Find(id);
            if (tblDirector == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblDirector.EmpId);
            return View(tblDirector);
        }

        // POST: TblDirectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DirectorId,EmpId")] TblDirector tblDirector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDirector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblDirector.EmpId);
            return View(tblDirector);
        }

        // GET: TblDirectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDirector tblDirector = db.TblDirectors.Find(id);
            if (tblDirector == null)
            {
                return HttpNotFound();
            }
            return View(tblDirector);
        }

        // POST: TblDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblDirector tblDirector = db.TblDirectors.Find(id);
            db.TblDirectors.Remove(tblDirector);
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
