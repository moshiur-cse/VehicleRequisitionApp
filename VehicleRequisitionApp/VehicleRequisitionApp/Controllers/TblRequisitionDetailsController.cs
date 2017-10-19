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
    public class TblRequisitionDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblRequisitionDetails
        public ActionResult Index()
        {
            var tblRequisitionDetails = db.TblRequisitionDetails.Include(t => t.LookUpEmployee).Include(t => t.LookupProject).Include(t => t.LookupRequisitionCategorys);
            return View(tblRequisitionDetails.ToList());
        }

        // GET: TblRequisitionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(id);
            if (tblRequisitionDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblRequisitionDetail);
        }

        // GET: TblRequisitionDetails/Create
        public ActionResult Create()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory");
            return View();
        }

        // POST: TblRequisitionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,ActuallyUsedFromDate,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId")] TblRequisitionDetail tblRequisitionDetail)
        {
            if (ModelState.IsValid)
            {
                db.TblRequisitionDetails.Add(tblRequisitionDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }

        // GET: TblRequisitionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(id);
            if (tblRequisitionDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }

        // POST: TblRequisitionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,ActuallyUsedFromDate,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId")] TblRequisitionDetail tblRequisitionDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRequisitionDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }

        // GET: TblRequisitionDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(id);
            if (tblRequisitionDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblRequisitionDetail);
        }

        // POST: TblRequisitionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(id);
            db.TblRequisitionDetails.Remove(tblRequisitionDetail);
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
