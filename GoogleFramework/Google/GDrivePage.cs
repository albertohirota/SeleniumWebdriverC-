using OpenQA.Selenium;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GoogleFramework
{
    public class GDrivePage: CommonFunctions
    {
        public static readonly By ButtonNewFile = By.XPath("//span[contains(text(),'New')]");
        public static readonly By MenuGoogleDocs = By.XPath("//div[@class='a-v-T'][contains(text(),'Google Docs')]");
        public static readonly By DeleteButtonFromGoogleDrive = By.XPath("//div[@aria-label='Remove']");

        public static void Click_NewFile() => Click(ButtonNewFile);
        public static void Click_GoogleDocs() => Click(MenuGoogleDocs);
        public static void Click_DeleteIconFromGoogleDrive() => Click(DeleteButtonFromGoogleDrive);
        public static void Click_DriveMenuFolder(string folder) => 
            Click(By.XPath("//span[@class='a-s-T'][contains(text(),'"+folder+"')]"));
        public static void Click_FileInDrive(string file) => Click(By.XPath
            ("//div[contains(@aria-label,'"+file+"')][contains(text(),'"+file+"')]"));
        
        /// <summary>
        /// Description: Delete a file in GoogleDrive
        /// </summary>
        /// <param name="file">Need the File name to be delete</param>
        public static void DeleteFileInDrive(string file)
        {
            LogInfo("Deleting File in Drive, file: "+ file);
            Click_FileInDrive(file);
            Click_DeleteIconFromGoogleDrive();
        }

        /// <summary>
        /// Method to return a List of string of fileList from Google Drive
        /// </summary>
        /// <param name="share"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>List of string with file names</returns>
        public static List<string> GetFileList(bool share = false)
        {
            LogInfo("Getting Google Drive File List through API");
            var fileList = new List<string>();
            FileList driveList = GetDriveJson(share);
            foreach(var file in driveList.Items)
            {
                fileList.Add(file.Title);
            }

            return fileList;
        }

        /// <summary>
        /// Method to get the FileList from GoogleDrive
        /// </summary>
        /// <param name="share"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns> Return the FileList from GoogleDrive</returns>
        /// <exception cref="ArgumentNullException">In case sender, certificate, or chain is null</exception>
        private static FileList GetDriveJson(bool share)
        {
            LogInfo("Getting Google Drive Object");
            DriveService service = GoogleApi.GetDriveService(share);
            FilesResource.ListRequest request = service.Files.List();
            request.Q = "";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPoliocyErros)
            {
                if (sender is null)
                    throw new ArgumentNullException(nameof(sender));

                if (certificate is null)
                    throw new ArgumentNullException(nameof(certificate));

                if (chain is null)
                    throw new ArgumentNullException(nameof(chain));

                return true;
            };
            FileList drive = request.Execute();
            return drive;
        }
    }
}
