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
            TestCases.TC001();
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            TestCases.TC002();
        }

        [TestMethod]
        public void TC003_CancelEmailAndValidateNewEmailDoesNotExist()
        {
            TestCases.TC003();
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
            TestCases.TC001();
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            TestCases.TC002();
        }

        [TestMethod]
        public void TC003_CancelEmailAndValidateNewEmailDoesNotExist()
        {
            TestCases.TC003();
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
            CommonFunctions.Delay(3000);
            CommonFunctions.WaitElementPresent(GmailPage.ButtonCompose);
            GmailPage.Click_NewEmail();
        }

        [TestMethod]
        public void TC001_ValidateSendButtonIsDisplayed()
        {
            TestCases.TC001();
        }

        [TestMethod]
        public void TC002_ValidateNewMessageText()
        {
            TestCases.TC002();
        }

        [TestMethod]
        public void TC003_CancelEmailAndValidateNewEmailDoesNotExist()
        {
            TestCases.TC003();
        }
    }
}