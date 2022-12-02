using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Linq;

namespace GoogleFramework
{
    public class GOfficePage : CommonFunctions
    {

        public static readonly By DocumentTitle = By.XPath("//input[@class='docs-title-input']");
        public static readonly By DocumentBlank = By.XPath("//img[contains(@src,'blank')]");
        public static readonly By ButtonGoogle = By.XPath
            ("//*[@data-tooltip='Docs home'] | //*[@data-tooltip='Sheets home'] | //*[@data-tooltip='Slides home']");
        public static readonly By ButtonDeleteFromGoogleDocs = By.XPath
            ("//div[@class='docs-homescreen-overflowmenuitem-text'][contains(text(),'Remove')]");
        public static readonly By ButtonMoveToTrash = By.XPath("//button[@name='moveToTrash']");

        public static void Click_DocumentNameTitle() => Click(DocumentTitle);
        public static void Click_ButtonGoogle() => Click(ButtonGoogle);
        public static void Click_DocumentBlank() => Click(DocumentBlank);
        public static void Click_OpenFile(string file) => Click(By.XPath
            ("//div[@class='docs-homescreen-list-item-title-value'][contains(text(),'" + file + "')]"));
        public static void RightClick_File(string fileName) => RightClick(By.XPath("//div[@title='" + fileName + "']"));

        public static void RenameDocumentName(string documentName)
        {
            Click_DocumentNameTitle();
            Clear_TextElement(DocumentTitle);
            SendKeyAndEnter(DocumentTitle,documentName);
            Delay(2000);
        }

        public static void DeleteFile(string fileName)
        {
            RightClick_File(fileName);
            Click(ButtonDeleteFromGoogleDocs);
            Click(ButtonMoveToTrash);
        }


    }
}
