using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GoogleFramework
{
    public class GmailPage : CommonFunctions
    {
        private static readonly log4net.ILog logger = log4net.LogManager.
            GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);

        public static readonly By ButtonCompose = By.XPath("//div[@class='T-I T-I-KE L3']");
        public static readonly By ButtonSend = By.XPath("//*[@role='button' and text()='Send']");
        public static readonly By TitleEmail = By.XPath("//*[@class='aYF']");
        public static readonly By ButtonDiscard = By.XPath("//*[@aria-label='Discard draft ‪(Ctrl-Shift-D)‬']");
        public static readonly By CcBoxLabel = By.XPath("//span[@class='aB gQ pE']");
        public static readonly By BccBoxLabel = By.XPath("//span[@class='aB  gQ pB']");
        public static readonly By To = By.XPath("//*[@name='to']//*[@name='to' or @class='agP aFw']");
        public static readonly By CcBox = By.XPath("//*[@name='cc']//*[@name='cc' or @class='agP aFw']");
        public static readonly By BccBox = By.XPath("//*[@name='bcc']//*[@name='bcc' or @class='agP aFw']");
        public static readonly By Subject = By.XPath("//*[@name='subjectbox']");
        public static readonly By MessageBody = By.XPath("//*[contains(@class,'Am Al editable LW-avf')]");
        public static readonly By InboxListNewEmail = By.XPath("//tr[@class='zA zE']");
        public static readonly By ReadViewEmailBody = By.XPath("//div[@class,'a3s aiL']");
        public static readonly By ButtonReply = By.XPath("//span[@class='ams bkH']"); //[contains(Text(),'Reply')]
        public static readonly By ButtonReplyAll = By.XPath("//span[text()='Reply all' and @role='link']");
        public static readonly By ButtonForward = By.XPath("//span[text()='Forward' and @role='link']");
        public static readonly By FolderInbox = By.XPath("//div[@data-tooltip='Inbox']");
        public static readonly By CheckBoxSelectAll = By.XPath("//span[@class='T-Jo J-J5-Ji']");
        public static readonly By ButtonDelete = By.XPath("//div[@data-tooltip='Delete']");

        public static void Click_NewEmail() => Click(ButtonCompose);
        public static void Click_ButtonDiscard() => Click(ButtonDiscard);
        public static void Click_SendEmail() => Click(ButtonSend);
        public static void Click_ButtonReply() => Click(ButtonReply);
        public static void Click_ButtonForward() => Click(ButtonForward);
        public static void GoToInbox() => Click(FolderInbox);
        public static void Click_CheckBoxSelectAll() => Click(CheckBoxSelectAll);
        public static void Click_ButtonDelete() => Click(ButtonDelete);

        /// <summary>
        /// Function to populate a new email in Gmail
        /// </summary>
        /// <param name="email">Email recipient</param>
        /// <param name="subject">Email subject</param>
        /// <param name="messageBody">Email body info</param>
        /// <param name="cc">optional CC</param>
        /// <param name="bcc">Optional BCC</param>
        public static void PopulateEmail(string email, string subject, string messageBody, string cc=null!, string bcc=null!)
        {
            logger.Info(String.Format("Populating email..."));
            SendKeyAndEnter(To, email);

            if (cc != null)
            {
                Click(CcBoxLabel);
                SendKeyAndEnter(CcBox, cc);
            }
            if (bcc != null)
            {
                Click(BccBoxLabel);
                SendKeyAndEnter(BccBox, bcc);
            }

            SendKey(Subject, subject);
            SendKey(MessageBody, messageBody);
        }

        /// <summary>
        /// Function to wait and open a received email, required e-mail subject
        /// </summary>
        /// <param name="subject">Email subject parameter</param>
        public static void WaitAndOpenReceivedEmail(string subject)
        {
            ReadOnlyCollection<IWebElement> emails = FindElements(InboxListNewEmail);
            foreach (IWebElement newEmail in emails)
            {
                if (newEmail.Text.Contains(subject))
                {
                    logger.Info(String.Format("New email found: "+ newEmail.Text.ToString()));
                    try
                    {
                        newEmail.Click();
                        CommonFunctions.Delay(2000);
                    }
                    catch
                    {
                        ReadOnlyCollection<IWebElement> child = newEmail.FindElements(By.XPath(".//*"));
                        foreach (IWebElement childElem in child)
                        {
                            if (childElem.Text.ToString().Contains(subject))
                            {
                                childElem.Click();
                            }
                        }
                    }
                    break;
                }
            }
        }
    }
}
