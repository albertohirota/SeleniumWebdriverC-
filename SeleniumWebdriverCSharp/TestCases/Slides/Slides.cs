using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp.Slides
{
    [TestClass]
    public class ChromeSlides : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Slides);
            CommonFunctions.Delay(5000);
        }

        [TestInitialize()]
        public void InitializeTestCases()
        {
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(3000);
        }

        [TestMethod]
        public void TC501_ValidateFileExist()
        {
            TestCases.TC501();
        }

        [TestMethod]
        public void TC502_ValidateNewFileCreated()
        {
            TestCases.TC502();
        }

        [TestMethod]
        public void TC503_ValidateSpreadsheetBodyInCellB1()
        {
            TestCases.TC503();
        }

        [TestMethod]
        public void TC404_ValidateSheetcBodyTextInNewFile()
        {
            TestCases.TC404();
        }
    }
}
