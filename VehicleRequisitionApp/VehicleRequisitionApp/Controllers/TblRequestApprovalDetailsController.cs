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
    public class TblRequestApprovalDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblRequestApprovalDetails
        public ActionResult Index()
        {
            return View(db.TblRequestApprovalDetails.ToList());
        }

        // GET: TblRequestApprovalDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequestApprovalDetail tblRequestApprovalDetail = db.TblRequestApprovalDetails.Find(id);
            if (tblRequestApprovalDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblRequestApprovalDetail);
        }

        // GET: TblRequestApprovalDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblRequestApprovalDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestApprovalDetailId,RequisitionId,ApprovalAuthorityId,ApprovalStatus,Comments")] TblRequestApprovalDetail tblRequestApprovalDetail)
        {
            if (ModelState.IsValid)
            {
                db.TblRequestApprovalDetails.Add(tblRequestApprovalDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblRequestApprovalDetail);
        }

        // GET: TblRequestApprovalDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequestApprovalDetail tblRequestApprovalDetail = db.TblRequestApprovalDetails.Find(id);
            if (tblRequestApprovalDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblRequestApprovalDetail);
        }

        // POST: TblRequestApprovalDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestApprovalDetailId,RequisitionId,ApprovalAuthorityId,ApprovalStatus,Comments")] TblRequestApprovalDetail tblRequestApprovalDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRequestApprovalDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblRequestApprovalDetail);
        }

        // GET: TblRequestApprovalDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequestApprovalDetail tblRequestApprovalDetail = db.TblRequestApprovalDetails.Find(id);
            if (tblRequestApprovalDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblRequestApprovalDetail);
        }

        // POST: TblRequestApprovalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblRequestApprovalDetail tblRequestApprovalDetail = db.TblRequestApprovalDetails.Find(id);
            db.TblRequestApprovalDetails.Remove(tblRequestApprovalDetail);
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
