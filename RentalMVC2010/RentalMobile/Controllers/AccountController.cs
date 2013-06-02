using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

        public ActionResult LogOn()
        {
            return View();
        }

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

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

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

                    if (model.Role == "Owner")
                    {
                        RegisterOwner(model);
                    }

                    if (model.Role == "Agent")
                    {
                        RegisterAgent(model);
                    }

                    if (model.Role == "Specialist")
                    {
                        RegisterSpecialist(model);
                    }

                    if (model.Role == "Provider")
                    {
                        RegisterProvider(model);
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

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

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

        [Authorize]
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

        [Authorize]
        public ActionResult ChangeEmail()
        {
            return View();
        }

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
                        var Tenant = _db.Tenants.Find(UserHelper.GetTenantID());
                        {
                            Tenant.EmailAddress = model.Email;
                        }
                        _db.SaveChanges();
                    }

                    if (User.IsInRole("Owner"))
                    {
                        //Owner
                        var owner = _db.Owners.Find(UserHelper.GetOwnerID());
                        {
                            owner.EmailAddress = model.Email;
                        }
                        _db.SaveChanges();
                    }

                    if (User.IsInRole("Agent"))
                    {
                        //Agent
                        var agent = _db.Agents.Find(UserHelper.GetAgentID());
                        {
                            agent.EmailAddress = model.Email;
                        }
                        _db.SaveChanges();
                    }

                    if (User.IsInRole("Specialist"))
                    {
                        //Specialist
                        var specialist = _db.Specialists.Find(UserHelper.GetSpecialistID());
                        {
                            specialist.EmailAddress = model.Email;
                        }
                        _db.SaveChanges();
                    }
                    if (User.IsInRole("Provider"))
                    {
                        //Provider
                        var Provider = _db.MaintenanceProviders.Find(UserHelper.GetProviderID());
                        {
                            Provider.EmailAddress = model.Email;
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

        [Authorize]
        public ActionResult ChangeEmailSuccess()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //Get username from provided email address;
                var userName = Membership.GetUserNameByEmail(model.Email);

                //if username exist get the membership user to reset the password
                if (userName != null)
                {
                    var currentUser = Membership.GetUser(userName);

                    if (currentUser != null && model.Email.ToLower() == currentUser.Email.ToLower())
                    {
                        SendResetEmail(currentUser);

                        return RedirectToAction("ResetPasswordSuccess", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The email address entered does not exist.");
                }

            }
            return View();
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }
        
        public void SendResetEmail(MembershipUser user)
        {
            //The URL to login
            if (HttpContext.Request.Url == null) return;
            var domain = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) +
                            HttpRuntime.AppDomainAppVirtualPath;

            //link to send
            var link = domain + "/Account/Logon";

            var body = "<p> Dear " + user.UserName + ",</p>";
            body += "<p> Your Orion password has been reset, Click " + link + " to login with details below</p>";
            body += "<p> UserName: " + user.UserName + "</p>";
            body += "<p> Password: " + user.ResetPassword() + "</p>";
            body += "<p>It is recomended that you change it after logon.</p>";
            body += "<p>If you did not request a password reset you do not need to take any action.</p>";

            var plainView = AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(body, @"<(.|\n)*?>", string.Empty), null, "text/plain");

            var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            var message = new MailMessage("haithem-araissia.com", user.Email)
                {
                    Subject = "Password Reset",
                    BodyEncoding = System.Text.Encoding.GetEncoding("utf-8"),
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                };

            message.AlternateViews.Add(plainView);
            message.AlternateViews.Add(htmlView);

            var smtpMail = new SmtpClient();
            var basicAuthenticationInfo = new System.Net.NetworkCredential("postmaster@haithem-araissia.com",
                                                                           "haithem759163");
            smtpMail.Host = "mail.haithem-araissia.com";
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = basicAuthenticationInfo;
            try
            {
                smtpMail.Send(message);
            }
            catch (Exception)
            {
                RedirectToAction("Index", "Home");
                throw;
            }
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
                newtenant.GoogleMap = "USA";
            }

            _db.Tenants.Add(newtenant);
            _db.SaveChanges();

        }

        [Authorize]
        public void RegisterOwner(RegisterModel model)
        {
            var newowner = new  Owner { EmailAddress = model.Email };
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newowner.GUID = (Guid)providerUserKey;
                newowner.FirstName = model.UserName;
                newowner.Photo = "./../images/dotimages/avatar-placeholder.png";
                newowner.GoogleMap = "USA";
            }

            _db.Owners.Add(newowner);
            _db.SaveChanges();

        }

        [Authorize]
        public void RegisterAgent(RegisterModel model)
        {
            var newagent = new Agent { EmailAddress = model.Email };
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newagent.GUID = (Guid)providerUserKey;
                newagent.FirstName = model.UserName;
                newagent.Photo = "./../images/dotimages/avatar-placeholder.png";
                newagent.GoogleMap = "USA";
            }

            _db.Agents.Add(newagent);
            _db.SaveChanges();

        }

        [Authorize]
        public void RegisterSpecialist(RegisterModel model)
        {
            var newspecialist = new Specialist { EmailAddress = model.Email };
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newspecialist.GUID = (Guid)providerUserKey;
                newspecialist.FirstName = model.UserName;
                newspecialist.Photo = "./../images/dotimages/avatar-placeholder.png";
                newspecialist.GoogleMap = "USA";
            }

            _db.Specialists.Add(newspecialist);
            _db.SaveChanges();

        }

        [Authorize]
        public void RegisterProvider(RegisterModel model)
        {
            var newprovider = new MaintenanceProvider { EmailAddress = model.Email };
            var user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                var providerUserKey = user.ProviderUserKey;
                if (providerUserKey != null)
                    newprovider.GUID = (Guid)providerUserKey;
                newprovider.FirstName = model.UserName;
                newprovider.Photo = "./../images/dotimages/avatar-placeholder.png";
                newprovider.GoogleMap = "USA";
            }

            _db.MaintenanceProviders.Add(newprovider);
            _db.SaveChanges();

        }

        //HELPERS
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
                return "Owner";
            }
            if (user.IsInRole("Agent"))
            {
                return "Agent";
            }
            if (user.IsInRole("Provider"))
            {
                return "Provider";
            }
            return user.IsInRole("Specialist") ? "Specialist" : null;
        }

        [Authorize]
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

            if (role == "Owner")
            {
                ViewBag.Id = UserHelper.GetOwnerID();
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                ViewBag.Type = "Profile";
                TempData["UserID"] = UserHelper.GetOwnerID();
            }

            if (role == "Agent")
            {
                ViewBag.Id = UserHelper.GetAgentID();
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                ViewBag.Type = "Profile";
                TempData["UserID"] = UserHelper.GetAgentID();
            }

            if (role == "Specialist")
            {
                ViewBag.Id = UserHelper.GetSpecialistID();
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                ViewBag.Type = "Profile";
                TempData["UserID"] = UserHelper.GetSpecialistID();
            }
            if (role == "Provider")
            {
                ViewBag.Id = UserHelper.GetProviderID();
                ViewBag.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                ViewBag.Type = "Profile";
                TempData["UserID"] = UserHelper.GetProviderID();
            }

            return View();
        }

        [Authorize]
        public ActionResult UpdateProfilePhoto(int id)
        {
            SavePictures(id);
            var user = System.Web.HttpContext.Current.User;
            if (user.IsInRole("Agent")) { return RedirectToAction("Index", "Tenant"); }
            if (user.IsInRole("Owner")) { return RedirectToAction("Index", "Owner"); }
            if (user.IsInRole("Agent")) { return RedirectToAction("Index", "Agent"); }
            if (user.IsInRole("Agent")) { return RedirectToAction("Index", "Provider"); }
            return user.IsInRole("Specialist") ? RedirectToAction("Index", "Specialist") : null;
        }

        public void SavePictures(int id)
        {
            var imageStoragePath = Server.MapPath("~/UploadedImages");
            var photoPath = Server.MapPath(GetUserPhotoPath());
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
                    var uploadfiles = uploadDirectory.GetFiles();
                    foreach (var f in uploadfiles)
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
            if (role == "Agent")
            {
                AddAgentPicture(photoPath);
            }
            if (role == "Owner")
            {
                AddOwnerPicture(photoPath);
            }
            if (role == "Agent")
            {
                AddAgentPicture(photoPath);
            }
            if (role == "Specialist")
            {
                AddSpecialistPicture(photoPath);
            }
            if (role == "Provider")
            {
                AddProviderPicture(photoPath);
            }
        }

        public void AddAgentPicture(string photoPath)
        {
            var Agent = _db.Agents.Find(UserHelper.GetAgentID());
            if (!ModelState.IsValid) return;
            Agent.Photo = CleanUpPhotoPath(photoPath);
            _db.SaveChanges();
        }

        public void AddOwnerPicture(string photoPath)
        {
            var owner = _db.Owners.Find(UserHelper.GetOwnerID());
            if (!ModelState.IsValid) return;
            owner.Photo = CleanUpPhotoPath(photoPath);
            _db.SaveChanges();
        }

        public void AddSpecialistPicture(string photoPath)
        {
            var specialist = _db.Specialists.Find(UserHelper.GetSpecialistID());
            if (!ModelState.IsValid) return;
            specialist.Photo = CleanUpPhotoPath(photoPath);
            _db.SaveChanges();
        }

        public void AddProviderPicture(string photoPath)
        {
            var provider = _db.MaintenanceProviders.Find(UserHelper.GetProviderID());
            if (!ModelState.IsValid) return;
            provider.Photo = CleanUpPhotoPath(photoPath);
            _db.SaveChanges();
        }

        public string CleanUpPhotoPath(string photoPath)
        {
            return photoPath.Replace(@"~\Photo", @"../../Photo").Replace("\\", "/");
        }

    }
}
