using OpenQA.Selenium;

namespace GoogleFramework
{
    public class GOfficePage : CommonFunctions
    {

        public static readonly By DocumentTitle = By.XPath("//input[@class='docs-title-input']");
        public static readonly By DocumentStatus = By.XPath("//div[@aria-label='Document status: Saved to Drive.']");
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

        /// <summary>
        /// Method to rename the document
        /// </summary>
        /// <param name="documentName">Enter the document name</param>
        public static void RenameDocumentName(string documentName)
        {
            LogInfo("Renaming Document to: "+ documentName);
            Click_DocumentNameTitle();
            Clear_TextElement(DocumentTitle);
            SendKeyAndEnter(DocumentTitle,documentName);
            WaitDocumentBeingSaved();
        }

        /// <summary>
        /// Method to wait the document being saved
        /// </summary>
        public static void WaitDocumentBeingSaved()
        {
            LogInfo("Waiting document being saved");
            bool fileSaved = WaitElementPresent(DocumentStatus);
            while (!fileSaved)
                fileSaved = WaitElementPresent(DocumentStatus);
        }

        /// <summary>
        /// Method to delete the file in the main Docs/Sheets/Slides page
        /// </summary>
        /// <param name="fileName">File name</param>
        public static void DeleteFile(string fileName)
        {
            LogInfo("Deleting file: "+ fileName);
            RightClick_File(fileName);
            Click(ButtonDeleteFromGoogleDocs);
            Click(ButtonMoveToTrash);
        }

        /// <summary>
        /// Method to share document with a user.
        /// </summary>
        /// <param name="user">Enter the email user</param>
        public static void SharePublic(string user)
        {
            LogInfo("Sharing document with: "+ user);
            WaitElementPresent(ButtonShare);
            Click_ButtonShare();
            AddSharedUser(user);
            Click_SendInSharingWindow();
        }

        /// <summary>
        /// Add the user in the iFrame AddUser
        /// </summary>
        /// <param name="user">Enter email user</param>
        public static void AddSharedUser(string user)
        {
            LogInfo("Adding shared user: "+ user);
            SwitchFrame(IframeSharing);
            SendKeyAndEnter(TextBoxSharing,user);
        }

        /// <summary>
        /// Click Send in Sharing Window
        /// </summary>
        public static void Click_SendInSharingWindow()
        {
            LogInfo("Sending in Sharing Window");
            Delay(3000);
            NotifyPeopleInSharing(false);
            Click_ButtonShareSend();
            Delay(2000);
            WaitElementNotBePresent(ButtonSharingSend);
            WaitElementNotBePresent(ButtonSharing);
            Delay(1000);
            RefreshPage();
            Delay(2000);
            Driver.Instance!.SwitchTo().DefaultContent();
            Delay(3000);
        }

        /// <summary>
        /// Method to check or uncheck Notify people.
        /// </summary>
        /// <param name="notify">Enter true if you wish the checkbox to be check or false to be unchecked</param>
        public static void NotifyPeopleInSharing(bool notify)
        {
            LogInfo("Notify people: "+notify.ToString());
            bool selected = Driver.Instance!.FindElement(CheckBoxNotify).Selected;
            if (selected != notify)
                SendKey(CheckBoxNotify, Keys.Space);
        }

        /// <summary>
        /// Click Button share
        /// </summary>
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
