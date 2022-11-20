using GoogleFramework;
using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class ChromeReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com", "Test Receiving", "Test body receiving email", "alberto.hirota@gmail.com", "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            GmailPage.Click_ButtonReply();
            Assert.IsTrue(Validation.DoesObjectExist("albertohirota@gmail.com", "email", "span"), "Email should be visible");
            GmailPage.Click_ButtonDiscard();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            GmailPage.Click_ButtonForward();
            GmailPage.SendKeyAndEnter(GmailPage.To,"eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            CommonFunctions.Delay(3000);
            GmailPage.GoToInbox();
            GmailPage.GoToInbox();
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.InboxListNewEmail,"Forwarded message"), "Email should be valid");
        }
    }

    [TestClass]
    public class EdgeReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com", "Test Receiving", "Test body receiving email", "alberto.hirota@gmail.com", "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            GmailPage.Click_ButtonReply();
            Assert.IsTrue(Validation.DoesObjectExist("albertohirota@gmail.com", "email", "span"), "Email should be visible");
            GmailPage.Click_ButtonDiscard();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            GmailPage.Click_ButtonForward();
            GmailPage.SendKeyAndEnter(GmailPage.To, "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            CommonFunctions.Delay(3000);
            GmailPage.GoToInbox();
            GmailPage.GoToInbox();
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.InboxListNewEmail, "Forwarded message"), "Email should be valid");
        }
    }

    [TestClass]
    public class FirefoxReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com", "Test Receiving", "Test body receiving email", "alberto.hirota@gmail.com", "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            GmailPage.Click_ButtonReply();
            Assert.IsTrue(Validation.DoesObjectExist("albertohirota@gmail.com", "email", "span"), "Email should be visible");
            GmailPage.Click_ButtonDiscard();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            GmailPage.Click_ButtonForward();
            GmailPage.SendKeyAndEnter(GmailPage.To, "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            CommonFunctions.Delay(3000);
            GmailPage.GoToInbox();
            GmailPage.GoToInbox();
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.InboxListNewEmail, "Forwarded message"), "Email should be valid");
        }
    }
}
