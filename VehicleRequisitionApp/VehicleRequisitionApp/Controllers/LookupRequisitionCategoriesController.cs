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
    public class LookupRequisitionCategoriesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookupRequisitionCategories
        public ActionResult Index()
        {
            return View(db.LookupRequisitionCategorys.ToList());
        }

        // GET: LookupRequisitionCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupRequisitionCategory lookupRequisitionCategory = db.LookupRequisitionCategorys.Find(id);
            if (lookupRequisitionCategory == null)
            {
                return HttpNotFound();
            }
            return View(lookupRequisitionCategory);
        }

        // GET: LookupRequisitionCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LookupRequisitionCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequisitionCategoryId,SortingSerialNo,RequisitionCategory")] LookupRequisitionCategory lookupRequisitionCategory)
        {
            if (ModelState.IsValid)
            {
                db.LookupRequisitionCategorys.Add(lookupRequisitionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lookupRequisitionCategory);
        }

        // GET: LookupRequisitionCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupRequisitionCategory lookupRequisitionCategory = db.LookupRequisitionCategorys.Find(id);
            if (lookupRequisitionCategory == null)
            {
                return HttpNotFound();
            }
            return View(lookupRequisitionCategory);
        }

        // POST: LookupRequisitionCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequisitionCategoryId,SortingSerialNo,RequisitionCategory")] LookupRequisitionCategory lookupRequisitionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookupRequisitionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookupRequisitionCategory);
        }

        // GET: LookupRequisitionCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookupRequisitionCategory lookupRequisitionCategory = db.LookupRequisitionCategorys.Find(id);
            if (lookupRequisitionCategory == null)
            {
                return HttpNotFound();
            }
            return View(lookupRequisitionCategory);
        }

        // POST: LookupRequisitionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookupRequisitionCategory lookupRequisitionCategory = db.LookupRequisitionCategorys.Find(id);
            db.LookupRequisitionCategorys.Remove(lookupRequisitionCategory);
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
