using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionApp.Models;

namespace VehicleRequisitionApp.Controllers
{
    public class LookUpEmployeesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookUpEmployees
        public ActionResult Index()
        {
            var lookUpEmployees = db.LookUpEmployees.Include(l => l.LookUpDivision).Include(l => l.LookUpEmployeeType);
            return View(lookUpEmployees.ToList());
        }

        // GET: LookUpEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployee lookUpEmployee = db.LookUpEmployees.Find(id);
            if (lookUpEmployee == null)
            {
                return HttpNotFound();
            }
            return View(lookUpEmployee);
        }

        // GET: LookUpEmployees/Create
        public ActionResult Create()
        {
            ViewBag.EmpDivisionId = new SelectList(db.LookUpDivisions, "DivisionId", "DivShortName");
            ViewBag.EmpTypeId = new SelectList(db.LookUpEmployeeTypes, "EmpTypeId", "EmpType");
            return View();
        }

        // POST: LookUpEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpId,SortingSerialNo,EmpTypeId,EmpInitial,EmpFullName,EmpDivisionId,EmpDesignation,EmpLevel,EmpEmail,EmpMobile,EmpRoomNo,EmpPresentAddress,EmpNid,EmpPaasportNo,EmpBloodGroup,EmpHighestDegree,EmpHighestDegreeMajorSubject,EmpCareerSummary")] LookUpEmployee lookUpEmployee)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(lookUpEmployee.EmpInitial));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            string hashPass = hash.ToString();
            TblUser aUser=new TblUser();

            if (ModelState.IsValid)
            {
                db.LookUpEmployees.Add(lookUpEmployee);
                db.SaveChanges();

                aUser.EmpId = lookUpEmployee.EmpId;
                aUser.Password = hashPass;
                db.TblUsers.Add(aUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpDivisionId = new SelectList(db.LookUpDivisions, "DivisionId", "DivShortName", lookUpEmployee.EmpDivisionId);
            ViewBag.EmpTypeId = new SelectList(db.LookUpEmployeeTypes, "EmpTypeId", "EmpType", lookUpEmployee.EmpTypeId);
            return View(lookUpEmployee);
        }

        // GET: LookUpEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployee lookUpEmployee = db.LookUpEmployees.Find(id);
            if (lookUpEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpDivisionId = new SelectList(db.LookUpDivisions, "DivisionId", "DivShortName", lookUpEmployee.EmpDivisionId);
            ViewBag.EmpTypeId = new SelectList(db.LookUpEmployeeTypes, "EmpTypeId", "EmpType", lookUpEmployee.EmpTypeId);
            return View(lookUpEmployee);
        }

        // POST: LookUpEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpId,SortingSerialNo,EmpTypeId,EmpInitial,EmpFullName,EmpDivisionId,EmpDesignation,EmpLevel,EmpEmail,EmpMobile,EmpRoomNo,EmpPresentAddress,EmpNid,EmpPaasportNo,EmpBloodGroup,EmpHighestDegree,EmpHighestDegreeMajorSubject,EmpCareerSummary")] LookUpEmployee lookUpEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookUpEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpDivisionId = new SelectList(db.LookUpDivisions, "DivisionId", "DivShortName", lookUpEmployee.EmpDivisionId);
            ViewBag.EmpTypeId = new SelectList(db.LookUpEmployeeTypes, "EmpTypeId", "EmpType", lookUpEmployee.EmpTypeId);
            return View(lookUpEmployee);
        }

        // GET: LookUpEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployee lookUpEmployee = db.LookUpEmployees.Find(id);
            if (lookUpEmployee == null)
            {
                return HttpNotFound();
            }
            return View(lookUpEmployee);
        }

        // POST: LookUpEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookUpEmployee lookUpEmployee = db.LookUpEmployees.Find(id);
            db.LookUpEmployees.Remove(lookUpEmployee);
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
