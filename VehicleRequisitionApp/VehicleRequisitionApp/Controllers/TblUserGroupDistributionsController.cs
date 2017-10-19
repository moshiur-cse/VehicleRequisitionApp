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
    public class TblUserGroupDistributionsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TblUserGroupDistributions
        public ActionResult Index()
        {
            var tblUserGroupDistributions = db.TblUserGroupDistributions.Include(t => t.LookupUserGroup).Include(t => t.TblUser);
            return View(tblUserGroupDistributions.ToList());
        }

        // GET: TblUserGroupDistributions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUserGroupDistribution tblUserGroupDistribution = db.TblUserGroupDistributions.Find(id);
            if (tblUserGroupDistribution == null)
            {
                return HttpNotFound();
            }
            return View(tblUserGroupDistribution);
        }

        // GET: TblUserGroupDistributions/Create
        public ActionResult Create()
        {
            ViewBag.UserGroupsId = new SelectList(db.LookupUserGroups, "UserGroupsId", "UserGroupName");

            //ViewBag.UserId = new SelectList(db.TblUsers, "UserId", "UserId");

            var userDetails =db.TblUsers.Join(db.LookUpEmployees, u => u.EmpId, uir => uir.EmpId, (u, uir) => new {u, uir}).ToList();
            ViewBag.UserId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");




            //         var UserInRole = db.UserProfiles.
            // Join(db.UsersInRoles, u => u.UserId, uir => uir.UserId,
            // (u, uir) => new { u, uir }).
            // Join(db.Roles, r => r.uir.RoleId, ro => ro.RoleId, (r, ro) => new { r, ro })
            //{
            //   .Select(m => new AddUserToRole
            //    UserName = m.r.u.UserName,
            //     RoleName = m.ro.RoleName
            // });
            return View();
        }

        // POST: TblUserGroupDistributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGroupDistributionId,SortingSerialNo,UserGroupsId,UserId")] TblUserGroupDistribution tblUserGroupDistribution)
        {
            if (ModelState.IsValid)
            {
                db.TblUserGroupDistributions.Add(tblUserGroupDistribution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserGroupsId = new SelectList(db.LookupUserGroups, "UserGroupsId", "UserGroupName", tblUserGroupDistribution.UserGroupsId);
            ViewBag.UserId = new SelectList(db.TblUsers, "UserId", "Password", tblUserGroupDistribution.UserId);
            return View(tblUserGroupDistribution);
        }

        // GET: TblUserGroupDistributions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUserGroupDistribution tblUserGroupDistribution = db.TblUserGroupDistributions.Find(id);
            if (tblUserGroupDistribution == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserGroupsId = new SelectList(db.LookupUserGroups, "UserGroupsId", "UserGroupName", tblUserGroupDistribution.UserGroupsId);
            ViewBag.UserId = new SelectList(db.TblUsers, "UserId", "Password", tblUserGroupDistribution.UserId);
            return View(tblUserGroupDistribution);
        }

        // POST: TblUserGroupDistributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserGroupDistributionId,SortingSerialNo,UserGroupsId,UserId")] TblUserGroupDistribution tblUserGroupDistribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUserGroupDistribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserGroupsId = new SelectList(db.LookupUserGroups, "UserGroupsId", "UserGroupName", tblUserGroupDistribution.UserGroupsId);
            ViewBag.UserId = new SelectList(db.TblUsers, "UserId", "Password", tblUserGroupDistribution.UserId);
            return View(tblUserGroupDistribution);
        }

        // GET: TblUserGroupDistributions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUserGroupDistribution tblUserGroupDistribution = db.TblUserGroupDistributions.Find(id);
            if (tblUserGroupDistribution == null)
            {
                return HttpNotFound();
            }
            return View(tblUserGroupDistribution);
        }

        // POST: TblUserGroupDistributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblUserGroupDistribution tblUserGroupDistribution = db.TblUserGroupDistributions.Find(id);
            db.TblUserGroupDistributions.Remove(tblUserGroupDistribution);
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
