using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Rotativa;
using Rotativa.Options;
using VehicleRequisitionApp.Models;

namespace VehicleRequisitionApp.Controllers
{
    public class TblRequisitionDetailsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblRequisitionDetails
        public ActionResult Index(int? id, int? divisionId) //, int? isLay=1
        {

            //ViewBag.isLay = isLay;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            if (Convert.ToInt32(Session["UserGroupId"]) == 1)
            {
                var findRequest = db.TblRequisitionDetails.Where(i => i.EmpId == id).ToList();
                if (findRequest.Count > 0)
                {

                    ViewBag.ApproveRequisition = db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.EmpId == id && i.StateId == 6 && i.RequiredToDate > DateTime.Now)
                        //DateTime.Now.AddDays(1) && 
                        .ToList();

                    ViewBag.NotApproveRequisition = db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.EmpId == id && i.StateId != 6 && i.RequiredToDate > DateTime.Now)
                        //DateTime.Now.AddDays(1) && 
                        .ToList();

                    return View();
                }

                if (id != null)
                {
                    ViewBag.ApproveRequisition = db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.EmpId == id) //DateTime.Now.AddDays(1) && 
                        .ToList();
                    return View();
                }
            }

            if (Convert.ToInt32(Session["UserGroupId"]) == 2)
            {

                string employeeInitial = Session["UserName"].ToString();

                var findProjectId = db.LookupProjects.Where(i => i.ProjectPl == employeeInitial).ToList();

                List<int> projectIdList = new List<int>();

                foreach (LookupProject item in findProjectId)
                {
                    projectIdList.Add(item.ProjectId);
                }

            
                if (findProjectId.Count > 0)
                {
                    ViewBag.ApproveRequisition =
                        db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                            .Include(t => t.LookupProject)
                            .Include(t => t.LookupRequisitionCategorys)
                            .Where(
                                i =>
                                    (projectIdList.Contains(i.ProjectId) || i.EmpId == id) && i.StateId != 1 &&
                                    i.RequiredToDate > DateTime.Now)
                            .ToList();

                    ViewBag.NotApproveRequisition =
                        db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                            .Include(t => t.LookupProject)
                            .Include(t => t.LookupRequisitionCategorys)
                            .Where(
                                i =>
                                    (projectIdList.Contains(i.ProjectId) || i.EmpId == id) && i.StateId == 1 &&
                                    i.RequiredToDate > DateTime.Now)
                            //DateTime.Now.AddDays(1) && 
                            .ToList();
                    //ViewBag.IsProjectLeader = 1;
                }
                
                   
                
                return View();
            }


            if (Convert.ToInt32(Session["UserGroupId"]) == 3)
            {
               
                var divId = divisionId == null ? Convert.ToInt32(Session["DivisionId"]) : divisionId;

                ViewBag.ApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId > 2 && i.LookUpEmployee.EmpDivisionId == divId && i.RequiredToDate > DateTime.Now) //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.NotApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId >= 1 && i.StateId <= 2 && i.LookUpEmployee.EmpDivisionId == divId && i.RequiredToDate > DateTime.Now)
                        //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.DivisionList = db.LookUpDivisions.ToList();
                return View();
            }

            if (Convert.ToInt32(Session["UserGroupId"]) == 4)
            {

                var divId = divisionId == null ? Convert.ToInt32(Session["DivisionId"]) : divisionId;

                ViewBag.ApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId > 3 && i.RequiredToDate > DateTime.Now) //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.NotApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId == 3 && i.RequiredToDate > DateTime.Now)
                        //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.DivisionList = db.LookUpDivisions.ToList();
                return View();
            }

            if (Convert.ToInt32(Session["UserGroupId"]) == 5)
            {

                var divId = divisionId == null ? Convert.ToInt32(Session["DivisionId"]) : divisionId;

                ViewBag.ApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId > 4  && i.RequiredToDate > DateTime.Now) //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.NotApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId >=3 && i.StateId <= 4 && i.RequiredToDate > DateTime.Now)
                        //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.DivisionList = db.LookUpDivisions.ToList();
                return View();
            }

            if (Convert.ToInt32(Session["UserGroupId"]) == 6)
            {
                ViewBag.ApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId != 5 && i.RequiredToDate > DateTime.Now && i.AssignId == 1) //DateTime.Now.AddDays(1) && 
                        .ToList();
                ViewBag.NotApproveRequisition =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.StateId == 5 && i.RequiredToDate > DateTime.Now && i.AssignId == 1) //DateTime.Now.AddDays(1) && 
                        .ToList();


                ViewBag.ConbinedRequisition = db.TblRequisitionDetails.Where(i => i.AssignedDriverEmpId != null && i.RequiredToDate > DateTime.Now && i.AssignId!= 1 && i.AssignId != 0)
              .GroupBy(k => new { k.AssignId, k.LookUpDriverEmployee.EmpFullName, k.LookUpDriverEmployee.EmpMobile, k.LookupVehicles.VehicleNo })
              .Select(k => new
              {
                  AssignIds = k.Key.AssignId,
                  RequiredFromDate = k.Min(l => l.RequiredFromDate),
                  RequiredToDate = k.Max(l => l.RequiredToDate),
                  ProjectCode = k.Select(l => l.LookupProject.ProjectCode),
                  Place = k.Select(l => l.Place),
                  EmpFullName = k.Select(l => l.LookUpEmployee.EmpFullName),
                  EmpMobile = k.Select(l => l.LookUpEmployee.EmpMobile),
                  DriverName = k.Key.EmpFullName,
                  DriverPhoneNo = k.Key.EmpMobile,
                  Vehicle = k.Key.VehicleNo
              }).ToList();
                List<CombinedRequisition> aList = new List<CombinedRequisition>();
                CombinedRequisition aCombinedRequisition;
                for (int r = 0; r < ViewBag.ConbinedRequisition.Count; r++)
                {
                    aCombinedRequisition = new CombinedRequisition();
                    aCombinedRequisition.AssignId = ViewBag.ConbinedRequisition[r].AssignIds;
                    aCombinedRequisition.RequiredFromDate = ViewBag.ConbinedRequisition[r].RequiredFromDate;
                    aCombinedRequisition.RequiredToDate = ViewBag.ConbinedRequisition[r].RequiredToDate;
                    aCombinedRequisition.Place = string.Join(", ", ViewBag.ConbinedRequisition[r].Place);
                    aCombinedRequisition.EmpFullName = string.Join(", ", ViewBag.ConbinedRequisition[r].EmpFullName);
                    aCombinedRequisition.EmpMobile = string.Join(", ", ViewBag.ConbinedRequisition[r].EmpMobile);
                    aCombinedRequisition.ProjectCode = string.Join(", ", ViewBag.ConbinedRequisition[r].ProjectCode);
                    aCombinedRequisition.DriverName = ViewBag.ConbinedRequisition[r].DriverName;
                    aCombinedRequisition.DriverPhoneNo = ViewBag.ConbinedRequisition[r].DriverPhoneNo;
                    aCombinedRequisition.VehicleNo = ViewBag.ConbinedRequisition[r].Vehicle;
                    aList.Add(aCombinedRequisition);
                }
                ViewBag.Combined = aList;
                return View();
            }
            ViewBag.NotApproveRequisition = db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                    .Include(t => t.LookupProject)
                    .Include(t => t.LookupRequisitionCategorys)
                    .Where(i => i.StateId != 6)
                    .ToList();

            ViewBag.ApproveRequisition =
                db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                    .Include(t => t.LookupProject)
                    .Include(t => t.LookupRequisitionCategorys)
                    .Where(i => i.StateId == 6)
                    .ToList();

            return View();
        }

        public ActionResult PreviousRequisition()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            ViewBag.ApproveRequisition =
               db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                   .Include(t => t.LookupProject)
                   .Include(t => t.LookupRequisitionCategorys)
                   .ToList();
            return View();

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

            ViewBag.ApprovalStatusDetails = db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id).ToList();
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
            ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == id), "EmpId", "EmpFullName");
            ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == id), "EmpId",
                "EmpDesignation");
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                "RequisitionCategory");
            return View();
        }

        // POST: TblRequisitionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,ActuallyUsedFromDate,UsedFromKM,UsedToKM,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId"
                )] TblRequisitionDetail tblRequisitionDetail)
        {
            tblRequisitionDetail.AssignId = 0;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            string message = "";
            if (tblRequisitionDetail.RequiredFromDate < DateTime.Now)
            {
                message = "* Request Time Must be greater then Current Time";
                ViewBag.Error = message;
                ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId),
                    "EmpId", "EmpFullName");
                ViewBag.EmpDesignation =
                    new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId",
                        "EmpDesignation");
                ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
                ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                    "RequisitionCategory");
                return View();
            }

            if (tblRequisitionDetail.RequiredToDate < tblRequisitionDetail.RequiredFromDate)
            {
                message = "* Required To Time Must be greater then Required From Time";
                ViewBag.Error = message;
                ViewBag.EmpId = new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId),
                    "EmpId", "EmpFullName");
                ViewBag.EmpDesignation =
                    new SelectList(db.LookUpEmployees.Where(i => i.EmpId == tblRequisitionDetail.EmpId), "EmpId",
                        "EmpDesignation");
                ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode");
                ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                    "RequisitionCategory");
                return View();
            }
            var plInitial =
                    db.LookupProjects.Where(i => i.ProjectId == tblRequisitionDetail.ProjectId)
                        .Select(i => i.ProjectPl)
                        .SingleOrDefault();

            if (ModelState.IsValid)
            {                
                var plEmails = db.LookUpEmployees.Where(i => i.EmpInitial == plInitial).Select(i => i.EmpEmail).SingleOrDefault();
                if (Convert.ToInt32(Session["UserGroupId"]) == 3)
                {

                    if (tblRequisitionDetail.RequisitionCategoryId == 3)
                    {
                        tblRequisitionDetail.StateId = 3;
                    }
                    else if (tblRequisitionDetail.RequisitionCategoryId == 2)
                    {
                        tblRequisitionDetail.StateId = 3;
                    }
                    else
                    {
                        tblRequisitionDetail.StateId = 5;
                    }

                    db.TblRequisitionDetails.Add(tblRequisitionDetail);
                    db.SaveChanges();
                    var requesterName =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookUpEmployee.EmpFullName).SingleOrDefault();

                    var projectCode =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookupProject.ProjectCode).SingleOrDefault();


                    var adminTransportEmail = "moshiur.mgmh@gmail.com";


                    ApprovalMethod(tblRequisitionDetail.RequisitionId, 5, "Approved By Director", adminTransportEmail,requesterName,projectCode);
                }
                else if (Convert.ToInt32(Session["UserGroupId"]) == 2)
                {
                   

                    if (tblRequisitionDetail.RequisitionCategoryId == 3)
                    {
                        tblRequisitionDetail.StateId = 3;
                    }
                    else if (tblRequisitionDetail.RequisitionCategoryId == 2)
                    {
                        tblRequisitionDetail.StateId = 2;
                    }
                    else
                    {
                        if (Session["UserName"].ToString() == plInitial)
                        {
                            tblRequisitionDetail.StateId = 2;
                        }
                        else
                        {
                            tblRequisitionDetail.StateId = 1;
                        }
                       
                    }












                    db.TblRequisitionDetails.Add(tblRequisitionDetail);
                    db.SaveChanges();

                    var requesterDivId =
                db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                    .Select(i => i.LookUpEmployee.EmpDivisionId)
                    .SingleOrDefault();

                    var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                        .Select(i => i.LookUpEmployee.EmpFullName)
                        .SingleOrDefault();

                    var directorEmail =
                        db.LookUpEmployees.Where(i => i.EmpDivisionId == requesterDivId && i.EmpDesignation.Contains("Director"))
                            .Select(i => i.EmpEmail)
                            .SingleOrDefault();

                    var projectCode =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookupProject.ProjectCode).SingleOrDefault();
                    if (Session["UserName"].ToString() == plInitial)
                    {
                        ApprovalMethod(tblRequisitionDetail.RequisitionId, 2, "Recommended By PL", directorEmail,
                            requesterName, projectCode);
                    }
                    else { 
                    EmailTemplate(plEmails, requesterName, projectCode);
                    }
                }
                else
                {
                    var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                                        .Select(i => i.LookUpEmployee.EmpFullName)
                                        .SingleOrDefault();

                    var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                        .Select(i => i.LookupProject.ProjectCode)
                        .SingleOrDefault();

                    if (tblRequisitionDetail.RequisitionCategoryId == 3)
                    {
                        tblRequisitionDetail.StateId = 3;
                    }
                    else if (tblRequisitionDetail.RequisitionCategoryId == 2)
                    {
                        tblRequisitionDetail.StateId = 2;
                    }
                    else
                    {
                        tblRequisitionDetail.StateId = 1;
                    }

                    db.TblRequisitionDetails.Add(tblRequisitionDetail);
                    db.SaveChanges();

                    EmailTemplate(plEmails, requesterName, projectCode);    
                    
                                   
                    //DirectorApprovalMethod(tblRequisitionDetail.RequisitionId, 1, "Recommended By PL");
                }
                return RedirectToAction("Index", "TblRequisitionDetails", new { id = Session["EmpId"] });
            }


            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.EmpDesignation = new SelectList(db.LookUpEmployees, "EmpId", "EmpDesignation",
                tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);

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
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
            return View(tblRequisitionDetail);
        }

        // POST: TblRequisitionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "RequisitionId,RequisitionCategoryId,EmpId,ProjectId,RequestSubmissionDate,RequiredFromDate,RequiredToDate,Place,Reason,UsedFromKM,UsedToKM,ActuallyUsedFromDate,ActuallyUsedToDate,AssignedDriverEmpId,AssignedVehicleId,StateId"
                )] TblRequisitionDetail tblRequisitionDetail)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tblRequisitionDetail).State = EntityState.Modified;
                db.SaveChanges();
                if (Convert.ToInt32(Session["UserGroupId"]) != 1)
                {
                    var findRequisition =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .ToList();

                    var requesterName =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookUpEmployee.EmpFullName)
                            .SingleOrDefault();
                    var requesterEmail =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookUpEmployee.EmpEmail)
                            .SingleOrDefault();

                    var projectCode =
                        db.TblRequisitionDetails.Where(i => i.RequisitionId == tblRequisitionDetail.RequisitionId)
                            .Select(i => i.LookupProject.ProjectCode)
                            .SingleOrDefault();

                    foreach (var item in findRequisition)
                    {
                        item.StateId = 6;
                        item.AssignId = 1;
                    }

                    EmailTemplateApproval(requesterEmail, requesterName, projectCode);
                    
                    db.SaveChanges();
                        
                    return RedirectToAction("Index");
                }

            }
            //return RedirectToAction("Index");

            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);
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

        public ActionResult PlApprove(int requisitionId)
        {
            var requesterDivId =
                db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                    .Select(i => i.LookUpEmployee.EmpDivisionId)
                    .SingleOrDefault();

            var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpFullName)
                .SingleOrDefault();

            var directorEmail =
                db.LookUpEmployees.Where(i => i.EmpDivisionId == requesterDivId && i.EmpDesignation == "Director")
                    .Select(i => i.EmpEmail)
                    .SingleOrDefault();
            var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookupProject.ProjectCode)
                .SingleOrDefault();
            ApprovalMethod(requisitionId, 2, "Recommended By PL", directorEmail, requesterName, projectCode);

            return RedirectToAction("Index", "TblRequisitionDetails", new { id=Convert.ToInt32(Session["EmpId"]) });

        }

        public ActionResult DirectorApprove(int requisitionId)
        {
            var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpFullName)
                .SingleOrDefault();
            var adminTransportEmail = "moshiur.mgmh@gmail.com";
            var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookupProject.ProjectCode)
                .SingleOrDefault();
            ApprovalMethod(requisitionId, 5, "Approved By Director", adminTransportEmail, requesterName,projectCode);
            return RedirectToAction("Index");
        }
        public ActionResult DEDApprove(int requisitionId)
        {
            var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpFullName)
                .SingleOrDefault();
            var edMailAddress = "moshiur.mgmh@gmail.com";
            var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookupProject.ProjectCode)
                .SingleOrDefault();
            ApprovalMethod(requisitionId, 4, "Approved By DED", edMailAddress, requesterName, projectCode);
            return RedirectToAction("Index");
        }
        public ActionResult EDApprove(int requisitionId)
        {
            var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpFullName)
                .SingleOrDefault();

            var adminTransportEmail = "moshiur.mgmh@gmail.com";

            var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookupProject.ProjectCode)
                .SingleOrDefault();
            ApprovalMethod(requisitionId, 5, "Approved By ED", adminTransportEmail, requesterName, projectCode);
            return RedirectToAction("Index");
        }

        public ActionResult AdminTransportApprove(int requisitionId)
        {
            var requesterName = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpFullName)
                .SingleOrDefault();
            var requesterEmail = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookUpEmployee.EmpEmail)
                .SingleOrDefault();

            var projectCode = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId)
                .Select(i => i.LookupProject.ProjectCode)
                .SingleOrDefault();

            ApprovalMethodAdminTransport(requisitionId, 6, "Vehicle Assigned By Admin Transport", requesterEmail, requesterName, projectCode);
            return RedirectToAction("Index");
        }
        public bool ApprovalMethodAdminTransport(int requisitionId, int stateId, string comments, string email, string requesterName, string projectCode)
        {
            TblRequestApprovalDetail aApproval = new TblRequestApprovalDetail();
            aApproval.RequisitionId = requisitionId;
            aApproval.ApprovalAuthorityId = Convert.ToInt32(Session["EmpId"]);
            aApproval.ApprovalStatus = true;
            aApproval.Comments = comments; //;
            db.TblRequestApprovalDetails.Add(aApproval);
            db.SaveChanges();
            var findRequisition = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId).ToList();

            foreach (TblRequisitionDetail item in findRequisition)
            {
                item.StateId = stateId;
                if (stateId == 5)
                {
                    item.AssignId = 1;
                }
            }



            EmailTemplateApproval(email, requesterName, projectCode);
            db.SaveChanges();


            return true;
        }

        public bool ApprovalMethod(int requisitionId, int stateId, string comments, string email, string requesterName, string projectCode)
        {
            TblRequestApprovalDetail aApproval = new TblRequestApprovalDetail();
            aApproval.RequisitionId = requisitionId;
            aApproval.ApprovalAuthorityId = Convert.ToInt32(Session["EmpId"]);
            aApproval.ApprovalStatus = true;
            aApproval.Comments = comments; //;
            db.TblRequestApprovalDetails.Add(aApproval);
            db.SaveChanges();
            var findRequisition = db.TblRequisitionDetails.Where(i => i.RequisitionId == requisitionId).ToList();
           
            foreach (TblRequisitionDetail item in findRequisition)
            {
                item.StateId = stateId;
                if (stateId == 5)
                {
                    item.AssignId = 1;
                }
            }

            

            EmailTemplate(email, requesterName, projectCode);
            db.SaveChanges();

               
            return true;
        }

        public ActionResult Prints(int id, string name)
        {
            var report = new ActionAsPdf("Print", new { id = id, name = name })
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                //PageMargins = new Margins(1/5, 1, 1/2, 1/5),
                //PageWidth = 210,
                //PageHeight = 297
            };
            return report;
        }


        public ActionResult CombinedRequisition(List<string> combinedRequisitionList) //IEnumerable<int?> productCategoryIds = null
        {
            List<int> projectIdList = new List<int>();

            int rId = 0;

            for (int i = 0; i < combinedRequisitionList.Count; i++)
            {
                if (combinedRequisitionList[i] == "")
                {
                    projectIdList.Add(0);
                }
                else
                {
                    projectIdList.Add(Convert.ToInt32(combinedRequisitionList[i]));
                    rId = Convert.ToInt32(combinedRequisitionList[i]);
                }
            }
            ViewBag.Conbined =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => projectIdList.Contains(i.RequisitionId)).ToList();


            string employee = "";
            string project = "";
            string place = "";
            string reson = "";
            string designation = "";

            foreach (TblRequisitionDetail aEmpoyeeList in ViewBag.Conbined)
            {
                employee += employee == "" ? aEmpoyeeList.LookUpEmployee.EmpFullName : ", " + aEmpoyeeList.LookUpEmployee.EmpFullName;
            
                project += project == "" ? aEmpoyeeList.LookupProject.ProjectCode : ", " + aEmpoyeeList.LookupProject.ProjectCode;
            
                place += place == "" ? aEmpoyeeList.Place : ", " + aEmpoyeeList.Place;
            
                reson += reson == "" ? aEmpoyeeList.Reason : ", " + aEmpoyeeList.Reason;
                designation+= designation==""?aEmpoyeeList.LookUpEmployee.EmpDesignation:", "+ aEmpoyeeList.LookUpEmployee.EmpDesignation;
            }


            ViewBag.EmpoyeeList = employee;
            ViewBag.ProjectList = project;
            ViewBag.PlaceList = place;
            ViewBag.ResonList = reson;
            ViewBag.Designation = designation;
            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(rId);

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

            ViewBag.VehicleId = db.LookupVehicles.ToList();
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);

            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
               "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);

            TempData["RequisitionListId"] = projectIdList;
            return View(tblRequisitionDetail);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CombinedRequisition(double? UsedFromKM, double? UsedToKM, DateTime? ActuallyUsedFromDate, DateTime? ActuallyUsedToDate, int AssignedDriverEmpId, int AssignedVehicleId, List<int> requisitionListId)
        {
            List<int> rIdList = (List<int>)TempData["RequisitionListId"];

            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            int assignId = Convert.ToInt32(db.TblRequisitionDetails.Max(i => i.AssignId).Value) + 1;

            for (int j = 0; j < rIdList.Count; j++)
            {
                TblRequisitionDetail aTblRequisitionDetail = db.TblRequisitionDetails.Find(rIdList[j]);

                aTblRequisitionDetail.StateId = 6;
                aTblRequisitionDetail.AssignedDriverEmpId = AssignedDriverEmpId;
                aTblRequisitionDetail.AssignedVehicleId = AssignedVehicleId;
                aTblRequisitionDetail.AssignId = assignId;
                EmailTemplateApproval(aTblRequisitionDetail.LookUpEmployee.EmpEmail,aTblRequisitionDetail.LookUpEmployee.EmpFullName, aTblRequisitionDetail.LookupProject.ProjectCode);
                db.SaveChanges();
               
            }
            return RedirectToAction("Index");
            
        }

        public ActionResult EditCombinedRequisition(int? assignId) //IEnumerable<int?> productCategoryIds = null
        {
            ViewBag.Conbined =
                    db.TblRequisitionDetails.Include(t => t.LookUpEmployee)
                        .Include(t => t.LookupProject)
                        .Include(t => t.LookupRequisitionCategorys)
                        .Where(i => i.AssignId == assignId).ToList();
            string employee = "";
            string project = "";
            string place = "";
            string reson = "";
            List<int> rId = new List<int>();
            foreach (TblRequisitionDetail aEmpoyeeList in ViewBag.Conbined)
            {
                employee += employee == "" ? aEmpoyeeList.LookUpEmployee.EmpFullName : ", " + aEmpoyeeList.LookUpEmployee.EmpFullName;
            
                project += project == "" ? aEmpoyeeList.LookupProject.ProjectCode : ", " + aEmpoyeeList.LookupProject.ProjectCode;
            
                place += place == "" ? aEmpoyeeList.Place : ", " + aEmpoyeeList.Place;
                reson += reson == "" ? aEmpoyeeList.Reason : ", " + aEmpoyeeList.Reason;
                rId.Add(aEmpoyeeList.RequisitionId);
            }
            ViewBag.EmpoyeeList = employee;
            ViewBag.ProjectList = project;
            ViewBag.PlaceList = place;
            ViewBag.ResonList = reson;

            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(rId[0]);

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

            ViewBag.VehicleId = db.LookupVehicles.ToList();
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpFullName", tblRequisitionDetail.EmpId);
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);

            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
               "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);

            return View(tblRequisitionDetail);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCombinedRequisition(double? UsedFromKM, double? UsedToKM, DateTime? ActuallyUsedFromDate, DateTime? ActuallyUsedToDate, int AssignedDriverEmpId, int AssignedVehicleId, int AssignId)
        {


            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            foreach (TblRequisitionDetail aTblRequisitionDetail in db.TblRequisitionDetails.Where(i => i.AssignId == AssignId).ToList())
            {
                aTblRequisitionDetail.UsedFromKM = UsedFromKM;
                aTblRequisitionDetail.UsedToKM = UsedToKM;
                aTblRequisitionDetail.ActuallyUsedFromDate = ActuallyUsedFromDate;
                aTblRequisitionDetail.ActuallyUsedToDate = ActuallyUsedToDate;

                aTblRequisitionDetail.AssignedDriverEmpId = AssignedDriverEmpId;
                aTblRequisitionDetail.AssignedVehicleId = AssignedVehicleId;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }


        public ActionResult Print(int id, string name)
        {
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
            ViewBag.ProjectId = new SelectList(db.LookupProjects, "ProjectId", "ProjectCode",
                tblRequisitionDetail.ProjectId);
            ViewBag.RequisitionCategoryId = new SelectList(db.LookupRequisitionCategorys, "RequisitionCategoryId",
                "RequisitionCategory", tblRequisitionDetail.RequisitionCategoryId);


            ViewBag.ApprovalStatusDetails = db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id).ToList();
            ViewBag.AdminTransport = name;

            ViewBag.PL =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id && i.Comments == "Recommended By PL")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();

            ViewBag.Director =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id && i.Comments == "Approved By Director")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();


            ViewBag.DED =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id && i.Comments == "Approved By DED")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();


            ViewBag.ED =
    db.TblRequestApprovalDetails.Where(i => i.RequisitionId == id && i.Comments == "Approved By ED")
        .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
        .SingleOrDefault();


            return View(tblRequisitionDetail);

        }
        public ActionResult CombinedPrints(int id, string name)
        {
            var report = new ActionAsPdf("CombinedPrint", new { id = id, name = name })
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                //PageMargins = new Margins(1/5, 1, 1/2, 1/5),
                //PageWidth = 210,
                //PageHeight = 297
            };
            return report;
        }
        public ActionResult CombinedPrint(int id, string name)
        {
            List<int> ids = new List<int>();

            string names="", designatinon="", project="", place="", reason="";
            foreach (TblRequisitionDetail item in db.TblRequisitionDetails.Where(i=>i.AssignId==id).ToList())
            {
                names+=(names==""?item.LookUpEmployee.EmpFullName: ", "+item.LookUpEmployee.EmpFullName);
                designatinon += (designatinon==""?item.LookUpEmployee.EmpDesignation:", "+ item.LookUpEmployee.EmpDesignation);
                project+= (project==""?item.LookupProject.ProjectCode:", "+ item.LookupProject.ProjectCode);
                place+= (place==""?item.Place: ", "+item.Place);
                reason+= (reason==""?item.Reason:", "+ item.Reason);
                ids.Add(item.RequisitionId);
            }
            ViewBag.empName = names;
            ViewBag.empDesignation = designatinon;
            ViewBag.projectCode = project;
            ViewBag.place = place;
            ViewBag.reason = reason;

            int aId = Convert.ToInt32(ids[0]);

            TblRequisitionDetail tblRequisitionDetail = db.TblRequisitionDetails.Find(aId);
            if (tblRequisitionDetail == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.AdminTransport = name;

            ViewBag.PL =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == aId && i.Comments == "Recommended By PL")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();

            ViewBag.Director =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == aId && i.Comments == "Approved By Director")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();


            ViewBag.DED =
                db.TblRequestApprovalDetails.Where(i => i.RequisitionId == aId && i.Comments == "Approved By DED")
                    .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
                    .SingleOrDefault();


            ViewBag.ED =
    db.TblRequestApprovalDetails.Where(i => i.RequisitionId == aId && i.Comments == "Approved By ED")
        .Select(i => i.LookUpEmployeeAuthority.EmpFullName)
        .SingleOrDefault();


            return View(tblRequisitionDetail);

        }

        public bool EmailTemplate(string email, string requesterName, string projectCode)
        {
            try
            {
                WebMail.SmtpServer = "130.180.3.10"; //gmail.com
                WebMail.SmtpPort = 25; //587
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = false;
                WebMail.UserName = "vehicleadmin";
                WebMail.Password = "cegis@2017";
                WebMail.From = "vehicleadmin@cegisbd.com";
                WebMail.Send(to: email, subject: "Approval for Vehicle Requisition ",
                    body: "<h3>Dear, Sir</h3>" +
                          "<p>I would like to inform that a vehicle requisition form has been submitted by <b>" + requesterName + "</b> under the project <b>" + projectCode + "</b> through the Vehicle Requisition System of CEGIS.Please visit http://www.cegisbd.com/vr  to approve the requisition. </ p > " +
                          "<p></p>" +
                          "<p>With kind regards.</p>" +

                          "<p></p>" +
                          "<p>Administrator</p>" +
                          "<p>Vehicle Requisition System of CEGIS</p>",
                    cc: "", bcc: "", isBodyHtml: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
            return true;
        }
        public bool EmailTemplateApproval(string email, string requesterName, string projectCode)
        {
            try
            {
                WebMail.SmtpServer = "130.180.3.10"; //gmail.com
                WebMail.SmtpPort = 25; //587
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = false;
                WebMail.UserName = "vehicleadmin";
                WebMail.Password = "cegis@2017";
                WebMail.From = "vehicleadmin@cegisbd.com";
                WebMail.Send(to: email, subject: "Approval status for Vehicle Requisition ",
                    body: "<h3>Dear, Sir</h3>" +
                          "<p>I would like to inform that a vehicle requisition form has been approved by <b>"+ Session["FullName"] + "</b> under the project<b> " + projectCode + "</b> through the Vehicle Requisition System of CEGIS.Please visit http://www.cegisbd.com/vr  to check approval status. </ p > " +
                          "<p></p>" +
                          "<p>With kind regards.</p>" +

                          "<p></p>" +
                          "<p>Administrator</p>" +
                          "<p>Vehicle Requisition System of CEGIS</p>",
                    cc: "", bcc: "", isBodyHtml: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                // Provide for exceptions.
            }
            return true;
        }
    }
}
