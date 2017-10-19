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
    public class TblManagementDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblManagementDetails
        public ActionResult Index()
        {
            var tblManagementDetails = db.TblManagementDetails.Include(t => t.LookUpEmployee).Include(t => t.LookUpManagementType);
            return View(tblManagementDetails.ToList());
        }

        // GET: TblManagementDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblManagementDetail tblManagementDetail = db.TblManagementDetails.Find(id);
            if (tblManagementDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblManagementDetail);
        }

        // GET: TblManagementDetails/Create
        public ActionResult Create()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            ViewBag.ManagementTypeId = new SelectList(db.LookUpManagementTypes, "ManagementTypeId", "ManagementType");
            return View();
        }

        // POST: TblManagementDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagementDetailId,SortingSerialNo,ManagementTypeId,EmpId")] TblManagementDetail tblManagementDetail)
        {
            if (ModelState.IsValid)
            {
                db.TblManagementDetails.Add(tblManagementDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblManagementDetail.EmpId);
            ViewBag.ManagementTypeId = new SelectList(db.LookUpManagementTypes, "ManagementTypeId", "ManagementType", tblManagementDetail.ManagementTypeId);
            return View(tblManagementDetail);
        }

        // GET: TblManagementDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblManagementDetail tblManagementDetail = db.TblManagementDetails.Find(id);
            if (tblManagementDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblManagementDetail.EmpId);
            ViewBag.ManagementTypeId = new SelectList(db.LookUpManagementTypes, "ManagementTypeId", "ManagementType", tblManagementDetail.ManagementTypeId);
            return View(tblManagementDetail);
        }

        // POST: TblManagementDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagementDetailId,SortingSerialNo,ManagementTypeId,EmpId")] TblManagementDetail tblManagementDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblManagementDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblManagementDetail.EmpId);
            ViewBag.ManagementTypeId = new SelectList(db.LookUpManagementTypes, "ManagementTypeId", "ManagementType", tblManagementDetail.ManagementTypeId);
            return View(tblManagementDetail);
        }

        // GET: TblManagementDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblManagementDetail tblManagementDetail = db.TblManagementDetails.Find(id);
            if (tblManagementDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblManagementDetail);
        }

        // POST: TblManagementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblManagementDetail tblManagementDetail = db.TblManagementDetails.Find(id);
            db.TblManagementDetails.Remove(tblManagementDetail);
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
