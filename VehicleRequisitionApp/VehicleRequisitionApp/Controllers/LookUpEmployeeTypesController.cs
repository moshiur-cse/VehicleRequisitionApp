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
    public class LookUpEmployeeTypesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookUpEmployeeTypes
        public ActionResult Index()
        {
            return View(db.LookUpEmployeeTypes.ToList());
        }

        // GET: LookUpEmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployeeType lookUpEmployeeType = db.LookUpEmployeeTypes.Find(id);
            if (lookUpEmployeeType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpEmployeeType);
        }

        // GET: LookUpEmployeeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookUpEmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpTypeId,SortingSerialNo,EmpType")] LookUpEmployeeType lookUpEmployeeType)
        {
            if (ModelState.IsValid)
            {
                db.LookUpEmployeeTypes.Add(lookUpEmployeeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookUpEmployeeType);
        }

        // GET: LookUpEmployeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployeeType lookUpEmployeeType = db.LookUpEmployeeTypes.Find(id);
            if (lookUpEmployeeType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpEmployeeType);
        }

        // POST: LookUpEmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpTypeId,SortingSerialNo,EmpType")] LookUpEmployeeType lookUpEmployeeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookUpEmployeeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookUpEmployeeType);
        }

        // GET: LookUpEmployeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployeeType lookUpEmployeeType = db.LookUpEmployeeTypes.Find(id);
            if (lookUpEmployeeType == null)
            {
                return HttpNotFound();
            }
            return View(lookUpEmployeeType);
        }

        // POST: LookUpEmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookUpEmployeeType lookUpEmployeeType = db.LookUpEmployeeTypes.Find(id);
            db.LookUpEmployeeTypes.Remove(lookUpEmployeeType);
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
