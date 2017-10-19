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
    public class LookUpManagementTypesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookUpManagementTypes
        public ActionResult Index()
        {
            return View(db.LookUpManagementTypes.ToList());
        }

        // GET: LookUpManagementTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpManagementType lookUpManagementType = db.LookUpManagementTypes.Find(id);
            if (lookUpManagementType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpManagementType);
        }

        // GET: LookUpManagementTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookUpManagementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagementTypeId,SortingSerialNo,ManagementType")] LookUpManagementType lookUpManagementType)
        {
            if (ModelState.IsValid)
            {
                db.LookUpManagementTypes.Add(lookUpManagementType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookUpManagementType);
        }

        // GET: LookUpManagementTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpManagementType lookUpManagementType = db.LookUpManagementTypes.Find(id);
            if (lookUpManagementType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpManagementType);
        }

        // POST: LookUpManagementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagementTypeId,SortingSerialNo,ManagementType")] LookUpManagementType lookUpManagementType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookUpManagementType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookUpManagementType);
        }

        // GET: LookUpManagementTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpManagementType lookUpManagementType = db.LookUpManagementTypes.Find(id);
            if (lookUpManagementType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpManagementType);
        }

        // POST: LookUpManagementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookUpManagementType lookUpManagementType = db.LookUpManagementTypes.Find(id);
            db.LookUpManagementTypes.Remove(lookUpManagementType);
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
