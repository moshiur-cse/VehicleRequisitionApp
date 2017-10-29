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
        public ActionResult Index(int? id)
        {           
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            if (Convert.ToInt32(Session["UserGroupId"])== 1)
            {
                var findRequest = db.TblRequisitionDetails.Where(i => i.EmpId == id).ToList();
                if (findRequest.Count > 0)
                {
                    var tblRequisitionDetail =
                        db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                            .Include(t => t.LookupProject)
                            .Include(t => t.LookupRequisitionCategorys)
                            .Where(i => i.EmpId == id && i.RequiredToDate>DateTime.Now)//DateTime.Now.AddDays(1) && 
                              .ToList();
                    return View(tblRequisitionDetail);

                }
                if (id != null)
                {
                    var tblRequisitionDetail =
                        db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                            .Include(t => t.LookupProject)
                            .Include(t => t.LookupRequisitionCategorys)
                            .Where(i => i.EmpId == id && i.RequiredToDate <DateTime.Now.Date) //Date
                            .ToList();
                    return View(tblRequisitionDetail);
                }
            }

            var tblRequisitionDetails = db.TblRequisitionDetails.Include(t => t.LookUpEmployee).Include(t => t.LookupProject).Include(t => t.LookupRequisitionCategorys).ToList();
            return View(tblRequisitionDetails);                                   
        }

        // GET: TblRequisitionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
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
        public ActionResult Create(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            id = Convert.ToInt32(Session["EmpId"]);
            ViewBag.Error = "";
            ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i=>i.EmpId==id), "EmpId", "EmpFullName");
            ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == id), "EmpId", "EmpDesignation");
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory");
            return View();
        }

        // POST: TblRequisitionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,ActuallyUsedFromDate,UsedFromKM,UsedToKM,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId")] TblRequisitionDetail tblRequisitionDetail)
        {
            string message = "";

            if (tblRequisitionDetail.RequiredFromDate< DateTime.Now) 
            {
                message = "* Request Time Must be greater then Cureent Time";
                ViewBag.Error = message;
                ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId", "EmpFullName");
                ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId", "EmpDesignation");
                ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
                ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory");
                return View();
            }

            if (tblRequisitionDetail.RequiredToDate<tblRequisitionDetail.RequiredFromDate)
            {
                message = "* Required To Time Must be greater then Required From Time";
                ViewBag.Error = message;
                ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId", "EmpFullName");
                ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId", "EmpDesignation");
                ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
                ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory");
                return View();
            }
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }


            if (ModelState.IsValid)
            {
                db.TblRequisitionDetails.Add(tblRequisitionDetail);
                db.SaveChanges();
                if (Session["UserName"].ToString() == "RMO")
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index","TblRequisitionDetails",new {id=Session["EmpId"]});
            }

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees, "EmpId", "EmpDesignation", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);

            return View(tblRequisitionDetail);
        }
        // GET: TblRequisitionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(id);
            if (tblRequisitionDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.DriverId = db.LookUpEmployees.Where(u => u.EmpDesignation == "Driver").
                                Select(i => new
                                {
                                    DriverName = i.EmpFullName,
                                    DriverId = i.EmpId
                                }).ToList();

            //ViewBag.DriverId = new SelectList(db.tbl, "DriverId", "DriverName");
            ViewBag.VehicleId = db.LookupVehicles.ToList();


            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }

        // POST: TblRequisitionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,UsedFromKM,UsedToKM,ActuallyUsedFromDate,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId")] TblRequisitionDetail tblRequisitionDetail)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            if (ModelState.IsValid)
            {
                db.Entry(tblRequisitionDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode", tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId", "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }
        // GET: TblRequisitionDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
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
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
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
