using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GoogleFramework
{
    public class GOfficePage : CommonFunctions
    {

        public static readonly By DocumentTitle = By.XPath("//input[@class='docs-title-input']");

        public static void Click_DocumentNameTitle() => Click(DocumentTitle);

        public static void RenameDocumentName(string documentName)
        {
            Click_DocumentNameTitle();
            Clear_TextElement(DocumentTitle);
            SendKeyAndEnter(DocumentTitle,documentName);
        }
    }
}
