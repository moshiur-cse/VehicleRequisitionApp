using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VehicleRequisitionApp.Models;

namespace VehicleRequisitionApp.Controllers
{
    public class UsersController: Controller
    {
        private MyDbContext db = new MyDbContext();
        public ActionResult LogIn()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(TblUser user)
        {
            string initial="", name="";
            int userId=0, userGroupId=0;
            
            string hashPass = PassWordHash(user.Password);
           
            var isUser = db.TblUsers.Where(u => u.EmpId == user.EmpId && u.Password == hashPass);

            var userDetails = db.TblUsers.Join(db.LookUpEmployees, u => u.EmpId, eu => eu.EmpId, (u, eu) => new {U=u,EU = eu}).
                                Where(u => u.U.EmpId == user.EmpId).
                                Select(u => new
                                {
                                    FullName = u.EU.EmpFullName,
                                    Initial = u.EU.EmpInitial,
                                    aUserId=u.U.UserId
                                }).ToList();


            foreach (var item in userDetails)
            {
                initial = item.Initial;
                name = item.FullName;
                userId = item.aUserId;

            }

            userGroupId = db.TblUserGroupDistributions.Where(i => i.UserId == userId).Select(i => i.UserGroupsId).SingleOrDefault();

            if (isUser.Count()>0)
            {
                Session["UserName"] = initial;
                Session["FullName"] = name;
                Session["UserId"] = userId;
                Session["EmpId"] = user.EmpId;
                Session["UserGroupId"] = userGroupId;
            }            
            else
            {
                TempData["Registration"] = "* Invalid Initial or Password";
                ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
                return View();
            }
         
            return RedirectToAction("Dashboard", "Users",new {id=Session["EmpId"]});
        }      
        public ActionResult Register()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(TblUser user)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }
            TblUser aTblUser = new TblUser();

            StringBuilder hash = new StringBuilder();

            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(user.Password));

            for (int i = 0; i < bytes.Length; i++)
            {
               hash.Append(bytes[i].ToString("x2"));
            }

            if (ModelState.IsValid)
            {                
                    aTblUser.EmpId = user.EmpId;
                    aTblUser.Password = hash.ToString();
                    db.TblUsers.Add(aTblUser);
                    db.SaveChanges();
                
                TempData["Registration"] = "Registration Successfully";
                return RedirectToAction("LogIn","Users");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("LogIn", "Users");
        }

        public ActionResult Dashboard(int? id)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpEmployee lookUpEmployee = db.LookUpEmployees.Find(id);
            if (lookUpEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Image = db.LookUpFileUploads.Where(i => i.EmpId == id).Select(i => i.FileName).SingleOrDefault();


            return View(lookUpEmployee);
            //return View();
        }
        public ActionResult ChangePassWord()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }
            TempData["Error"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassWord(ChangePassWord user)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }
            string oldPass=PassWordHash(user.OldPassword);

            var isUser = db.TblUsers.Where(u => u.UserId == user.UserId && u.Password == oldPass);

            if (isUser.Count() > 0)
            {
                var result = (from u in db.TblUsers where u.UserId == user.UserId select u);
               
                string hashPass = PassWordHash(user.NewPassword);

                foreach (TblUser item in result)
                {
                    item.Password = hashPass;                   
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // Provide for exceptions.
                }

            }
            else
            {
                TempData["Error"] = "Please Ender a Correct Old Password";
                return View(user);
            }
            TempData["Error"] = "Password Change Successfully";
            return View(user);
        }

        public string PassWordHash(string pass)
        {           
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(pass));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}