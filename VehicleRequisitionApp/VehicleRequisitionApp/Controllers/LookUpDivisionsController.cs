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
    public class LookUpDivisionsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookUpDivisions
        public ActionResult Index()
        {
            return View(db.LookUpDivisions.ToList());
        }

        // GET: LookUpDivisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpDivision lookUpDivision = db.LookUpDivisions.Find(id);
            if (lookUpDivision == null)
            {
                return HttpNotFound();
            }
            return View(lookUpDivision);
        }

        // GET: LookUpDivisions/Create
        public ActionResult Create()
        {
            ViewBag.HtmlStr = "";

            return View();
        }

        // POST: LookUpDivisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DivisionId,DivShortName,DivFullName")] LookUpDivision lookUpDivision)
        {
            if (ModelState.IsValid)
            {
                db.LookUpDivisions.Add(lookUpDivision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookUpDivision);
        }

        // GET: LookUpDivisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpDivision lookUpDivision = db.LookUpDivisions.Find(id);
            if (lookUpDivision == null)
            {
                return HttpNotFound();
            }
            return View(lookUpDivision);
        }

        // POST: LookUpDivisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DivisionId,DivShortName,DivFullName")] LookUpDivision lookUpDivision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookUpDivision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookUpDivision);
        }

        // GET: LookUpDivisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpDivision lookUpDivision = db.LookUpDivisions.Find(id);
            if (lookUpDivision == null)
            {
                return HttpNotFound();
            }
            return View(lookUpDivision);
        }

        // POST: LookUpDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookUpDivision lookUpDivision = db.LookUpDivisions.Find(id);
            db.LookUpDivisions.Remove(lookUpDivision);
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
