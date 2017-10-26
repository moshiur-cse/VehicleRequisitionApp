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
    public class TblDriversController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblDrivers
        public ActionResult Index()
        {
            var tblDrivers = db.TblDrivers.Include(t => t.LookUpEmployee);
            return View(tblDrivers.ToList());
        }

        // GET: TblDrivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriver tblDriver = db.TblDrivers.Find(id);
            if (tblDriver == null)
            {
                return HttpNotFound();
            }
            return View(tblDriver);
        }

        // GET: TblDrivers/Create
        public ActionResult Create()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName");
            return View();
        }

        // POST: TblDrivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverId,EmpId")] TblDriver tblDriver)
        {
            if (ModelState.IsValid)
            {
                db.TblDrivers.Add(tblDriver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblDriver.EmpId);
            return View(tblDriver);
        }

        // GET: TblDrivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriver tblDriver = db.TblDrivers.Find(id);
            if (tblDriver == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblDriver.EmpId);
            return View(tblDriver);
        }

        // POST: TblDrivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverId,EmpId")] TblDriver tblDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblDriver.EmpId);
            return View(tblDriver);
        }

        // GET: TblDrivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriver tblDriver = db.TblDrivers.Find(id);
            if (tblDriver == null)
            {
                return HttpNotFound();
            }
            return View(tblDriver);
        }

        // POST: TblDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblDriver tblDriver = db.TblDrivers.Find(id);
            db.TblDrivers.Remove(tblDriver);
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
