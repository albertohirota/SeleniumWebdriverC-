using GoogleFramework;
using Login;
using OpenQA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class SendNewEmail : TestBaseClass
    {
        [TestMethod, TestCategory("Gmail"), TestCategory("Chrome")]
        public void TC001_ValidateSendButtonIsDisplayedChrome()
        {
            Driver.Initialize(Driver.Browsers.Chrome);
            TC001_ValidateSendButtonIsDisplayed();
        }

        [TestMethod, TestCategory("Gmail"), TestCategory("Firefox")]
        public void TC001_ValidateSendButtonIsDisplayedFireFox()
        {
            Driver.Initialize(Driver.Browsers.Firefox);
            TC001_ValidateSendButtonIsDisplayed();
        }

        [TestMethod, TestCategory("Gmail"), TestCategory("Edge")]
        public void TC001_ValidateSendButtonIsDisplayedEdge()
        {
            Driver.Initialize(Driver.Browsers.Edge);
            TC001_ValidateSendButtonIsDisplayed();
        }

        public static void TC001_ValidateSendButtonIsDisplayed()
        {
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonSend), "Send button should be found");
        }

        [TestMethod, TestCategory("Gmail"), TestCategory("Chrome")]
        public void TC002_ValidateNewMessageTextChrome()
        {
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.TitleEmail,"New Message"), "Email title should be valid");


            CommonFunctions.TakeScreenshot("Test");
            CommonFunctions.Delay(1000);
        }
    }
}