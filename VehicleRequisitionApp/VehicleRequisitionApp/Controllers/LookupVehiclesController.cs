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
    public class LookupVehiclesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookupVehicles
        public ActionResult Index()
        {
            return View(db.LookupVehicles.ToList());
        }

        // GET: LookupVehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupVehicles lookupVehicles = db.LookupVehicles.Find(id);
            if (lookupVehicles == null)
            {
                return HttpNotFound();
            }
            return View(lookupVehicles);
        }

        // GET: LookupVehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookupVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehiclesId,SortingSerialNo,VehicleNo,LicenseNo,VehicleModel")] LookupVehicles lookupVehicles)
        {
            if (ModelState.IsValid)
            {
                db.LookupVehicles.Add(lookupVehicles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookupVehicles);
        }

        // GET: LookupVehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupVehicles lookupVehicles = db.LookupVehicles.Find(id);
            if (lookupVehicles == null)
            {
                return HttpNotFound();
            }
            return View(lookupVehicles);
        }

        // POST: LookupVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehiclesId,SortingSerialNo,VehicleNo,LicenseNo,VehicleModel")] LookupVehicles lookupVehicles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookupVehicles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookupVehicles);
        }

        // GET: LookupVehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupVehicles lookupVehicles = db.LookupVehicles.Find(id);
            if (lookupVehicles == null)
            {
                return HttpNotFound();
            }
            return View(lookupVehicles);
        }

        // POST: LookupVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookupVehicles lookupVehicles = db.LookupVehicles.Find(id);
            db.LookupVehicles.Remove(lookupVehicles);
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
