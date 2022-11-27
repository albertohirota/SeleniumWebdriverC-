using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class ChromeReceivingEmail: TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com","Test Receiving","Test body receiving email","alberto.hirota@gmail.com","eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        [TestMethod]
        public void TC004_ValidateBcc()
        {
            Assert.IsTrue(Validation.DoesObjectExist("eitihirota@gmail.com","email", "span"), "Bcc email should be visible");
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonReplyAll), "Button should be available");
        }
    }

    [TestClass]
    public class EdgeReceivingEmail : TestBaseClass
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
        public void TC004_ValidateBcc()
        {
            Assert.IsTrue(Validation.DoesObjectExist("eitihirota@gmail.com", "email", "span"), "Bcc email should be visible");
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonReplyAll), "Button should be available");
        }
    }

    [TestClass]
    public class FirefoxReceivingEmail : TestBaseClass
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
            CommonFunctions.Delay(2000);
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        [TestMethod]
        public void TC004_ValidateBcc()
        {
            Assert.IsTrue(Validation.DoesObjectExist("eitihirota@gmail.com", "email", "span"), "Bcc email should be visible");
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonReplyAll), "Button should be available");
        }
    }
}
