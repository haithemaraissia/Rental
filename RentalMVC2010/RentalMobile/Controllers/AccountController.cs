using System;
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

                    if (model.Role == "Tenant")
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
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
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
                var changeEmailSucceeded = true;
                try
                {

                    //Membership
                    var u = Membership.GetUser(User.Identity.Name);
                    if (u != null)
                    {
                        u.Email = model.Email;
                        Membership.UpdateUser(u);
                    }


                    if (User.IsInRole("Tenant"))
                    {
                        //Tenant
                        var tenant = _db.Tenants.Find(UserHelper.GetTenantID());
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
                ModelState.AddModelError("", "The email address is incorrect.");
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
            var newtenant = new Tenant { EmailAddress = model.Email };
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newtenant.GUID = (Guid)providerUserKey;
                newtenant.FirstName = model.UserName;
                newtenant.Photo = "./../images/dotimages/avatar-placeholder.png";
            }

            _db.Tenants.Add(newtenant);
            _db.SaveChanges();

        }













        //UPLOAD PRIMARY PHOTO REGARDLESS OF THE ROLE; ONLY 1 PHOTO
        //IT will requires another uploader.ashx

        //public string Username = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name).ToString();

        //Helpers
        public string GetUserPhotoPath()
        {
            var role = GetCurrentRole();
            if (role != null)
            {
                return "~/Photo/" + role + "/Profile";
            }
            return null;
        }

        public string GetVirtualUserPhotoPath()
        {
            var role = GetCurrentRole();
            if (role != null)
            {
                return @"~\Photo\" + role;
            }
            return null;
        }

        public string GetCurrentRole()
        {
            var user = System.Web.HttpContext.Current.User;
            if (user.IsInRole("Tenant"))
            {
                return "Tenant";
            }
            if (user.IsInRole("Owner"))
            {
                return "Landlord";
            }
            return user.IsInRole("Specialist") ? "Specialist" : null;
        }













        //        TempData["TenantUsername"] = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
        //TempData["RequestID"] = maintenanceorder.MaintenanceID;

        public ActionResult Upload(int id)
        {
            var role = GetCurrentRole();
            if (role == "Tenant")
            {
                ViewBag.Id = UserHelper.GetTenantID();
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                ViewBag.Type = "Profile";
                TempData["UserID"] = UserHelper.GetTenantID();
            }

            //RequestID = TempData["RequestID"].ToString();
            //ViewBag.TenantUserName = TenantUsername;
            //ViewBag.RequestID = TempData["RequestID"].ToString();
            //TempData["UserID"] = RequestID;
            return View();
        }


        public ActionResult UpdateProfilePhoto(int id)
        {
            SavePictures(id);
            var user = System.Web.HttpContext.Current.User;
            if (user.IsInRole("Tenant")) { return RedirectToAction("Index", "Tenant"); }
            if (user.IsInRole("Owner")) { return RedirectToAction("Index", "Landlord"); }
            return user.IsInRole("Specialist") ? RedirectToAction("Index", "Specialist") : null;
        }

        public void SavePictures(int id)
        {
            var imageStoragePath = Server.MapPath("~/UploadedImages");
            var photoPath = Server.MapPath(GetUserPhotoPath());

            //Maybe we need to pass the role//
            var directory = @"\" + System.Web.HttpContext.Current.User.Identity.Name + @"\" + "Profile" + @"\" + id + @"\";
            var desinationdirectory = @"\" + System.Web.HttpContext.Current.User.Identity.Name + @"\" + id + @"\";

            var path = imageStoragePath + directory;

            var uploadDirectory = new DirectoryInfo(path);
            var newdirectory = photoPath + desinationdirectory;


            if (Directory.Exists(newdirectory))
            {
                UploadHelper.CreateDirectoryIfNotExist(newdirectory);
            }



            var latestFile = (from f in uploadDirectory.GetFiles()
                              orderby f.LastWriteTime descending
                              select f).First();
            if (latestFile != null)
                try
                {


                    var destinationFile = newdirectory + @"\" + latestFile.Name;
                    var virtualdestinationFile = GetVirtualUserPhotoPath() + @"\" + "Profile" + @"\" + System.Web.HttpContext.Current.User.Identity.Name + @"\" + id + @"\" + latestFile.Name;


                    if (!System.IO.File.Exists(destinationFile))
                    {

                        //Delete all files in the destination folder 
                        var desintationDirectoryFolder = new DirectoryInfo(newdirectory);
                        if (desintationDirectoryFolder.Exists)
                        {

                            var files = desintationDirectoryFolder.GetFiles();
                            foreach (var f in files)
                            {
                                System.IO.File.Delete(f.Name);
                            }
                        }
                        else
                        {
                            UploadHelper.CreateDirectoryIfNotExist(newdirectory);
                            
                        }
                        System.IO.File.Move(latestFile.FullName, destinationFile);
                        AddPicture(virtualdestinationFile);
                    }


                    //Delete all Files from UploaderDirectory
                    var files2 = uploadDirectory.GetFiles();
                    foreach (var f in files2)
                    {
                        System.IO.File.Delete(f.Name);
                    }

                }





                catch (Exception e)
                {

                    Response.Write(string.Format("Error occurs in uploading profile picture! {0}", e.Message));
                }

            UploadHelper.DeleteDirectoryIfExist(path);
        }

        public void AddPicture(string photoPath)
        {
            var role = GetCurrentRole();
            if (role == "Tenant")
            {
                AddTenantPicture(photoPath);
            }
            //if (role == "Landloard")
            //{
            //    AddLandlordPicture();
            //}
            //if (role == "Specialist")
            //{
            //    AddSpecialistPicture();
            //}
        }









        public void AddTenantPicture(string photoPath)
        {
            var tenant = _db.Tenants.Find(UserHelper.GetTenantID());
            if (!ModelState.IsValid) return;
            tenant.Photo = photoPath.Replace(@"~\Photo", @"../../Photo").Replace("\\", "/");
            _db.SaveChanges();
        }

        //Future

        //public void AddLandlordPicture( string photoPath)
        //{
        //    var tenant = _db.Tenants.Find(UserHelper.GetTenantID());
        //    if (!ModelState.IsValid) return;
        //    tenant.Photo = photoPath;
        //    _db.SaveChanges();
        //}

        //public void AddSpecialistPicture(string photoPath)
        //{
        //    var tenant = _db.Tenants.Find(UserHelper.GetTenantID());
        //    if (!ModelState.IsValid) return;
        //    tenant.Photo = photoPath;
        //    _db.SaveChanges();
        //}









    }
}
