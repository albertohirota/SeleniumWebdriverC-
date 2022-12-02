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
            ("//*[@data-tooltip='Docs home' or @data-tooltip='Sheets home'or @data-tooltip='Slides home']");
        public static readonly By ButtonDeleteFromGoogleDocs = By.XPath
            ("//div[@class='docs-homescreen-overflowmenuitem-text'][contains(text(),'Remove')]");
        public static readonly By ButtonMoveToTrash = By.XPath("//button[@name='moveToTrash']");
        public static readonly By ButtonShare = By.XPath("//*[@id='docs-titlebar-share-client-button']");
        public static readonly By ButtonSharingSend = By.XPath("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ nCP5yc AjY5Oe DuMIQc LQeN7 ftJYz']");
        public static readonly By ButtonSharing = By.XPath("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ nCP5yc AjY5Oe DuMIQc LQeN7 xFWpbf oWBWHf ftJYz CZCFtc-bMElCd sj692e RCmsv jbArdc']");
        public static readonly By IframeSharing = By.XPath("//iframe[@class='share-client-content-iframe']");
        public static readonly By TextBoxSharing = By.XPath("//input[@class='zeumMd d2j1H']");
        public static readonly By CheckBoxNotify = By.XPath("//input[@name='notify']");

        public static void Click_DocumentNameTitle() => Click(DocumentTitle);
        public static void Click_ButtonGoogle() => Click(ButtonGoogle);
        public static void Click_ButtonShare() => Click(ButtonShare);
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

        public static void SharedPublic(string user)
        {
            Click_ButtonShare();
            AddSharedUser(user);
            Click_SendInSharingWindow();
        }

        public static void AddSharedUser(string user)
        {
            SwitchFrame(IframeSharing);
            SendKeyAndEnter(TextBoxSharing,user);
        }

        public static void Click_SendInSharingWindow()
        {
            Delay(3000);
            NotifyPeopleInSharing(false);
            Click_ButtonShareSend();
            Delay(1000);
            RefreshPage();
            Delay(1000);
            Driver.Instance!.SwitchTo().DefaultContent();
            Delay(1000);
        }

        public static void NotifyPeopleInSharing(bool notify)
        {
            bool selected = Driver.Instance.FindElement(CheckBoxNotify).Selected;
            if (selected != notify)
                SendKey(CheckBoxNotify, Keys.Space);
        }

        public static void Click_ButtonShareSend()
        {
            bool exist = DoesElementExist(ButtonSharingSend);
            if (exist)
                Click(ButtonSharingSend);
            else
                Click(ButtonSharing);
        }
    }
}
