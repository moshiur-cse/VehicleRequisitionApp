﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using VehicleRequisitionApp.Models;
using WebGrease.Css.Extensions;

//https://github.com/moshiur-cse/VehicleRequisitionApp.git
//DBCC CHECKIDENT (TableName, RESEED, 0);
namespace VehicleRequisitionApp.Controllers
{
    public class TblUsersController : Controller
    {
        private MyDbContext db = new MyDbContext();
        public ActionResult LogIn()
        {
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(TblUser user,string initials)
        {
            string initial = "", name = "", degination = "";
            int userId = 0, userGroupId = 0, divId = 0, empId=0;
            var controller = "";
            var action = "";

            string hashPass = PassWordHash(user.Password);

            int findEmpId = db.LookUpEmployees.Where(i => i.EmpInitial == initials).Select(i=>i.EmpId).SingleOrDefault();
            if (findEmpId==0)
            {
                TempData["Registration"] = "* Invalid Initial";
                ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
                return View();

            }          
            var isUser = db.TblUsers.Where(u => u.EmpId == findEmpId && u.Password == hashPass);

            var userDetails = db.TblUsers.Join(db.LookUpEmployees, u => u.EmpId, eu => eu.EmpId, (u, eu) => new { U = u, EU = eu }).
                                Where(u => u.U.EmpId == findEmpId).
                                Select(u => new
                                {
                                    FullName = u.EU.EmpFullName,
                                    Initial = u.EU.EmpInitial,
                                    Degination = u.EU.EmpDesignation,
                                    aUserId = u.U.UserId,
                                    //DivisionFullName=u.EU.LookUpDivision.DivFullName,
                                    //DivisionShortName = u.EU.LookUpDivision.DivShortName,
                                    aDivisionId = u.EU.LookUpDivision.DivisionId

                                }).ToList();


            foreach (var item in userDetails)
            {
                initial = item.Initial;
                name = item.FullName;
                userId = item.aUserId;
                degination = item.Degination;
                divId = item.aDivisionId;

            }

            userGroupId = db.TblUserGroupDistributions.Where(i => i.UserId == userId).Select(i => i.UserGroupsId).SingleOrDefault();

            if (isUser.Count() > 0)
            {
                Session["UserName"] = initial;
                Session["FullName"] = name;
                Session["Degination"] = degination;
                Session["UserId"] = userId;
                Session["EmpId"] = findEmpId;
                Session["UserGroupId"] = userGroupId;
                Session["DivisionId"] = divId;

            }
            else
            {
                TempData["Registration"] = "* Invalid Password";
                ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
                return View();
            }

            controller = db.LoopUpRedirectPages.Where(i => i.UserGroupsId == userGroupId).Select(i => i.ControllerName).SingleOrDefault();
            action = db.LoopUpRedirectPages.Where(i => i.UserGroupsId == userGroupId).Select(i => i.ActionName).SingleOrDefault();

            return RedirectToAction(action, controller, new { id = Session["EmpId"] });
        }
        public ActionResult Register()
        {
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("LogIn", "TblUsers");
            //}
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel user,string email, string phoneNo)
        {
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("LogIn", "TblUsers");
            //}
            TblUser aTblUser = new TblUser();
            TblUserGroupDistribution aGroupUser = new TblUserGroupDistribution();
            LookUpFileUpload aFile = new LookUpFileUpload();

            string hashPass = PassWordHash(user.Password);

            var status = db.TblUsers.Where(i => i.EmpId == user.EmpId).ToList();

            if (status.Count() > 0)
            {
                TempData["Registration"] = "Your are already Registered. Please Sing in here...";
                return RedirectToAction("LogIn", "TblUsers");
            }
            if (status.Count()==0) { 
                aTblUser.EmpId = user.EmpId;
                aTblUser.Password = hashPass;
                db.TblUsers.Add(aTblUser);

                aGroupUser.UserId = aTblUser.UserId;
                aGroupUser.UserGroupsId = 1;
                db.TblUserGroupDistributions.Add(aGroupUser);

                aFile.FileName = "";
                aFile.EmpId = user.EmpId;
                db.LookUpFileUploads.Add(aFile);

                var updateEmailAndPhoneNo = db.LookUpEmployees.Where(i => i.EmpId == user.EmpId).ToList();
                foreach (LookUpEmployee item in updateEmailAndPhoneNo)
                {
                    item.EmpEmail = email;
                    item.EmpMobile = phoneNo;

                }


                db.SaveChanges();


                TempData["Registration"] = "Registration Successfully Completed";
                return RedirectToAction("LogIn", "TblUsers");
            }
            ViewBag.EmpId = new SelectList(db.LookUpEmployees, "EmpId", "EmpInitial");
            return View();
        }
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("LogIn", "TblUsers");
        }

        public ActionResult Dashboard(int? id)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
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

            ViewBag.CarStatus = db.TblRequisitionDetails.Where(i => i.AssignedDriverEmpId != null && i.RequiredToDate > DateTime.Now && i.AssignId==1).ToList();

            ViewBag.ConbinedRequisition = db.TblRequisitionDetails.Where(i => i.AssignedDriverEmpId != null && i.RequiredToDate > DateTime.Now && i.AssignId!= 1 && i.AssignId != 0)
               .GroupBy(k => new { k.AssignId, k.LookUpDriverEmployee.EmpFullName,k.LookUpDriverEmployee.EmpMobile, k.LookupVehicles.VehicleNo} )
               .Select(k=> new
               {
                AssignIds = k.Key.AssignId,
                RequiredFromDate = k.Min(l=>l.RequiredFromDate),                             
                Place = k.Select(l => l.Place),
                EmpFullName = k.Select(l => l.LookUpEmployee.EmpFullName),
                EmpMobile = k.Select(l => l.LookUpEmployee.EmpMobile),                
                DriverName = k.Key.EmpFullName,
                DriverPhoneNo = k.Key.EmpMobile,
                Vehicle = k.Key.VehicleNo
               }).ToList();
            List<CombinedRequisition> aList=new List<CombinedRequisition>();
            CombinedRequisition aCombinedRequisition;
            for (int r = 0; r < ViewBag.ConbinedRequisition.Count; r++)
            {
                aCombinedRequisition = new CombinedRequisition();
                aCombinedRequisition.AssignId = ViewBag.ConbinedRequisition[r].AssignIds;
                aCombinedRequisition.RequiredFromDate=ViewBag.ConbinedRequisition[r].RequiredFromDate;              
                aCombinedRequisition.Place= string.Join(", ", ViewBag.ConbinedRequisition[r].Place);
                aCombinedRequisition.EmpFullName=string.Join(", ", ViewBag.ConbinedRequisition[r].EmpFullName);
                aCombinedRequisition.EmpMobile=string.Join(", ", ViewBag.ConbinedRequisition[r].EmpMobile);
                aCombinedRequisition.DriverName= ViewBag.ConbinedRequisition[r].DriverName;
                aCombinedRequisition.DriverPhoneNo=ViewBag.ConbinedRequisition[r].DriverPhoneNo;
                aCombinedRequisition.VehicleNo =  ViewBag.ConbinedRequisition[r].Vehicle;
                aList.Add(aCombinedRequisition);
            }
            return View(aList);
            //return View();
        }

        public ActionResult Profile(int? id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
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
        }

        public ActionResult ChangePassWord()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            TempData["Error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassWord(ChangePassWord user)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LogIn", "TblUsers");
            }
            if (user.Password!=user.ConfirmPassword)
            {
                TempData["Error"] = "The password and confirmation password do not match.";
                return View(user);
            }
            string oldPass = PassWordHash(user.OldPassword);

            var isUser = db.TblUsers.Where(u => u.UserId == user.UserId && u.Password == oldPass);

            if (isUser.Count() > 0)
            {
                var result = (from u in db.TblUsers where u.UserId == user.UserId select u);

                string hashPass = PassWordHash(user.Password);

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
        public JsonResult EmployeeDetails(int empId)
        {
            var empInfo = db.LookUpEmployees.Where(i => i.EmpId == empId).Select
                        (u => new
                        {
                            Id = u.EmpId,
                            Name = u.EmpFullName,
                            Email = u.EmpEmail,
                            PhoneNo = u.EmpMobile
                        }).ToList();

            return Json(empInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string PinNo, string Email)
        {
            int empId = db.LookUpEmployees.Where(i => i.EmpPinNo == PinNo).Select(i => i.EmpId).SingleOrDefault();
            string email = db.LookUpEmployees.Where(i => i.EmpEmail == Email).Select(i=>i.EmpEmail).SingleOrDefault();

            if (empId != 0)
            {
                TblUser aUsr = db.TblUsers.Where(i=>i.EmpId==empId).SingleOrDefault();
                aUsr.Password = PassWordHash("CegiS"+ PinNo);
               

                try
                {
                    WebMail.SmtpServer = "130.180.3.10"; //gmail.com
                    WebMail.SmtpPort = 25; //587
                    WebMail.SmtpUseDefaultCredentials = true;
                    WebMail.EnableSsl = false;
                    WebMail.UserName = "vehicleadmin";
                    WebMail.Password = "cegis@2017";
                    WebMail.From = "vehicleadmin@cegisbd.com";
                    WebMail.Send(to: email, subject: "New User Password for Vehicle Requisition Sytem",
                        body: "<h3> User Initial : " + aUsr.LookUpEmployee.EmpInitial + "</h3>" +
                              "<p> New Password : " + "CegiS" + PinNo + "</p>",
                        cc: "", bcc: "", isBodyHtml: true);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-error", Title = "Error!", Message = "Operation Failed." };
                    // Provide for exceptions.
                }
                TempData["Error"] = "*New Password Send to your email Adrees";
                return View();
            }
            else                        
            TempData["Error"] = "*Invalid Email or Pin No";
            return View();
            
        }
        //public JsonResult EmployeeDetails(string empId)
        //{
        //    var empInfo = db.LookUpEmployees.Where(i => i.EmpInitial == empId).Select
        //                (u => new
        //                {
        //                    Id = u.EmpId,
        //                    Name =u.EmpFullName,
        //                    Email=u.EmpEmail,
        //                    PhoneNo=u.EmpMobile                                                        
        //                }).ToList();

        //    return Json(empInfo, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult AutoCompleteInitial(string initial)
        {
            //Searching records from list using LINQ query  
            var empInitial = db.LookUpEmployees.Where(i => i.EmpInitial.StartsWith(initial)).Select
                       (u => new{
                           Id = u.EmpId,
                           Initial = u.EmpInitial,                          
                       }).ToList();
            return Json(empInitial, JsonRequestBehavior.AllowGet);
        }
    }
}