using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionApp.Models;

namespace VehicleRequisitionApp.Controllers
{
    public class LookUpFileUploadsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: LookUpFileUploads
        public ActionResult Index()
        {
            return View(db.LookUpFileUploads.ToList());
        }


        //[HttpGet]
        //public ActionResult UploadFile()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return View();
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}

        // GET: LookUpFileUploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpFileUpload lookUpFileUpload = db.LookUpFileUploads.Find(id);
            if (lookUpFileUpload == null)
            {
                return HttpNotFound();
            }
            return View(lookUpFileUpload);
        }

        // GET: LookUpFileUploads/Create
        public ActionResult Create(int? id)
        {
            LookUpFileUpload imageDetails = db.LookUpFileUploads.Where(i => i.EmpId == id).SingleOrDefault();
            //    Select(k=>new
            //{
            //    fileName=k.FileName
            //}).ToList();

            ViewBag.Message = "";
            return View(imageDetails);
        }

        // POST: LookUpFileUploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FileId,FileName,EmpId")] LookUpFileUpload lookUpFileUpload)
        public ActionResult Create(HttpPostedFileBase file,[Bind(Include = "FileId,FileName,EmpId")] LookUpFileUpload lookUpFileUpload,int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }

            LookUpFileUpload fileUp = db.LookUpFileUploads.Find(id);            
            db.LookUpFileUploads.Remove(fileUp);
            db.SaveChanges();
            string fullPath = Request.MapPath("~/UploadedFiles/" + fileUp.FileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            try
                {
                    if (file.ContentLength > 0)
                    {
                        string fileNames = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileNames);
                        file.SaveAs(path);
                        lookUpFileUpload.FileName = fileNames;
                    }

                    ViewBag.Message = "File Uploaded Successfully!!";                    
                    db.LookUpFileUploads.Add(lookUpFileUpload);
                    db.SaveChanges();
                    return RedirectToAction("Dashboard","TblUsers",new {id=Convert.ToInt32(Session["EmpId"])});
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }
            

            //return View(lookUpFileUpload);           
        }

        // GET: LookUpFileUploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpFileUpload lookUpFileUpload = db.LookUpFileUploads.Find(id);
            if (lookUpFileUpload == null)
            {
                return HttpNotFound();
            }

            return View(lookUpFileUpload);
        }

        // POST: LookUpFileUploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,FileName,EmpId")] LookUpFileUpload lookUpFileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lookUpFileUpload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lookUpFileUpload);
        }

        // GET: LookUpFileUploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpFileUpload lookUpFileUpload = db.LookUpFileUploads.Find(id);
            if (lookUpFileUpload == null)
            {
                return HttpNotFound();
            }
            return View(lookUpFileUpload);
        }

        // POST: LookUpFileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LookUpFileUpload lookUpFileUpload = db.LookUpFileUploads.Find(id);
            db.LookUpFileUploads.Remove(lookUpFileUpload);
            db.SaveChanges();

            string fullPath = Request.MapPath("~/UploadedFiles/" + lookUpFileUpload.FileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
