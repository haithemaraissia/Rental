using System.IO;
using System.Web;

namespace RentalMobile.Helpers
{
    public static class UploadHelper
    {
        /// <summary>
        /// Addition of Helper function to create and/or delete directory
        /// </summary>
        /// <param name="newDirectory"></param>
        public static void CreateDirectoryIfNotExist(string newDirectory)
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
                HttpContext.Current.Response.Write(err.Message);
            }
        }

        public static void DeleteDirectoryIfExist(string newDirectory)
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
                HttpContext.Current.Response.Write(err.Message);
            }
        }
    }
}