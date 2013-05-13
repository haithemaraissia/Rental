﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using RentalMobile.Helpers;
using RentalMobile.Models;

namespace RentalMobile.Controllers
{
    public class AccountController : Controller
    {
        private readonly DB_33736_rentalEntities _db = new DB_33736_rentalEntities();

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", model.Role);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null,
                                      out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    Roles.AddUserToRole(model.UserName, model.Role);

                    if (model.Role =="Tenant")
                    {
                        RegisterTenant(model);
                    }
                    //Add User to the Databases
                    return RedirectToAction("Index", model.Role);
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion


















        //
        // GET: /Account/ChangeEmail

        [Authorize]
        public ActionResult ChangeEmail()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmail model)
        {
            if (ModelState.IsValid)
            {

                // Change will throw an exception rather
                // than return false in certain failure scenarios.
                bool changeEmailSucceeded = true;
                try
                {

                    //Membership
                    MembershipUser u = Membership.GetUser(User.Identity.Name);
                    u.Email = model.Email;
                    Membership.UpdateUser(u);


                    if (User.IsInRole("Tenant"))
                    {
                        //Tenant
                        Tenant tenant = _db.Tenants.Find(UserHelper.GetTenantID());
                        {
                            tenant.EmailAddress = model.Email;
                        }
                        _db.SaveChanges();
                    }


                }
                catch (Exception)
                {
                    changeEmailSucceeded = false;
                }

                if (changeEmailSucceeded)
                {
                    return RedirectToAction("ChangeEmailSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The email address is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangeEmailSuccess

        public ActionResult ChangeEmailSuccess()
        {
            return View();
        }




        [Authorize]
        public void RegisterTenant(RegisterModel model)
        {
            var newtenant = new Tenant {EmailAddress = model.Email};
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newtenant.GUID = (Guid)providerUserKey;
            }
            _db.Tenants.Add(newtenant);
            _db.SaveChanges();

        }













        //UPLOAD PRIMARY PHOTO REGARDLESS OF THE ROLE; ONLY 1 PHOTO
        //IT will requires another uploader.ashx

        public string Username = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).ToString();

        //Depending on the role
        public string UserPhotoPath = "~/Photo/Tenant/Requests";
        public string UserID;

        public ActionResult Upload()
        {
            //RequestID = TempData["RequestID"].ToString();
            //ViewBag.TenantUserName = TenantUsername;
            //ViewBag.RequestID = TempData["RequestID"].ToString();
            //TempData["UserID"] = RequestID;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            SavePictures();
            return RedirectToAction("Index", "MaintenanceOrder");
        }

        public void SavePictures()
        {
            var imageStoragePath = Server.MapPath("~/UploadedImages");
            var photoPath = Server.MapPath(UserPhotoPath);
            var directory = @"\" + Username + @"\" + "Requests" + @"\" + TempData["RequestID"] + @"\";
            var path = imageStoragePath + directory;
            var uploadDirectory = new DirectoryInfo(path);
            var newdirectory = photoPath + directory;
            if (Directory.Exists(path))
            {
                UploadHelper.CreateDirectoryIfNotExist(newdirectory);
            }
            var files = uploadDirectory.GetFiles();

            foreach (var f in files)
            {
                var destinationFile = newdirectory + @"\" + f.Name;
                var virtualdestinationFile = @"~\Photo\Tenant\Requests" + directory + f.Name;
                if (!System.IO.File.Exists(destinationFile))
                {
                    System.IO.File.Move(f.FullName, destinationFile);
                    AddPicture(Convert.ToInt32(TempData["RequestID"]), virtualdestinationFile);
                }
                if (System.IO.File.Exists(f.Name))
                    System.IO.File.Delete(f.Name);
            }
            UploadHelper.DeleteDirectoryIfExist(path);
        }

        public void AddPicture(int maintenanceId, string photoPath)
        {
            var maintenancephoto = new MaintenancePhoto { MaintenanceID = maintenanceId, PhotoPath = photoPath };
            if (!ModelState.IsValid) return;
            _db.MaintenancePhotoes.Add(maintenancephoto);
            _db.SaveChanges();
        }

    }
}
