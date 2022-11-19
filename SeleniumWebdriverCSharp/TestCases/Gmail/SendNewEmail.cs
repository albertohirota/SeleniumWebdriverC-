using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class ChromeSendNewEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
        }

        [TestMethod]
        public void TC001_ValidateSendButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonSend), "Send button should be found");
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.TitleEmail, "New Message"), "Email title should be valid");


            CommonFunctions.TakeScreenshot("Test");
            CommonFunctions.Delay(1000);
        }
    }

    [TestClass]
    public class EdgeSendNewEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
        }

        [TestMethod]
        public void TC001_ValidateSendButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonSend), "Send button should be found");
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.TitleEmail, "New Message"), "Email title should be valid");
        }
    }

    [TestClass]
    public class FireFoxSendNewEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
        }

        [TestMethod]
        public void TC001_ValidateSendButtonIsDisplayed()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonSend), "Send button should be found");
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.TitleEmail, "New Message"), "Email title should be valid");
        }
    }
}