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
            Click_FileInDrive(file);
            Click_DeleteIconFromGoogleDrive();
        }

        public static List<string> GetFileList(bool share = false)
        {
            var fileList = new List<string>();
            FileList driveList = GetDriveJson(share);
            foreach(var file in driveList.Items)
            {
                fileList.Add(file.Title);
            }

            return fileList;
        }

        private static FileList GetDriveJson(bool share)
        {
            DriveService service = GoogleApi.LoadDriveApi(share);

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
