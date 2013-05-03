using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using plUploadHandler.Properties;

namespace MyUpload.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        //Variables that should be queried with the request
        public string TenantUsername = "Jack";
        public string RequestID = "5";

        public ActionResult Index()
        {
            var imageStoragePath = Server.MapPath("~/UploadedImages");
            var tenantPath = Server.MapPath("~/Tenant");
            var directory = @"\" + TenantUsername + @"\" + "Requests" + @"\" + RequestID + @"\";

            var path = imageStoragePath + directory;

            var uploadDirectory = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                if (uploadDirectory.GetFiles().Count() != 0)
                {
                    var files = uploadDirectory.GetFiles();

                    //Create Directory if it doesn't exist
                    var newdirectory = tenantPath + directory;
                    CreateDirectoryIfNotExist(newdirectory);

                    foreach (var f in files)
                    {

                        //This is what you need.
                        var destinationFile = newdirectory + @"\" + f.Name;

                        if (!System.IO.File.Exists(destinationFile))
                        {
                            System.IO.File.Move(f.FullName, destinationFile);
                        }
                        if (System.IO.File.Exists(f.Name))
                            System.IO.File.Delete(f.Name);

                    }
                    DeleteDirectoryIfExist(path);
                }
            }


            return View();
        }



        //public void CopyFiles(string uploadedFilename)
        //{
        //    var directory = "/" + Request.Params["UserName"] + "/" + "Requests" + "/" + Request.Params["requestid"];
        //    string sourceFile = Server.MapPath("Data.txt");
        //    string destinationFile = Server.MapPath("Datamove.txt");
        //    System.IO.File.Move(sourceFile, destinationFile);

        //}







        /// <summary>
        /// Addition of Helper function to create and/or delete directory
        /// </summary>
        /// <param name="newDirectory"></param>
        private void CreateDirectoryIfNotExist(string newDirectory)
        {
            try
            {
                // Checking the existence of directory
                if (!Directory.Exists(newDirectory))
                {

                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(newDirectory);
                }
            }
            catch (IOException err)
            {
                Response.Write(err.Message);
            }
        }
        private void DeleteDirectoryIfExist(string newDirectory)
        {
            try
            {
                // Checking the existence of directory
                if (Directory.Exists(newDirectory))
                {

                    string[] files = Directory.GetFiles(newDirectory);
                    foreach (string file in files)
                        System.IO.File.Delete(file);

                    Directory.Delete(newDirectory);
                }
            }
            catch (IOException err)
            {
                Response.Write(err.Message);
            }
        }
    }
}
