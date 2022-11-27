using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Reflection;

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
    }
}
