using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp.Sheets
{
    [TestClass]
    public class ChromeSheets : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Sheets);
            CommonFunctions.Delay(5000);
        }

        [TestInitialize()]
        public void InitializeTestCases()
        {
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(3000);
        }

        [TestMethod]
        public void TC401_ValidateFileExist()
        {
            TestCases.TC401();
        }

        [TestMethod]
        public void TC402_ValidateNewFileCreated()
        {
            TestCases.TC402();
        }

        [TestMethod]
        public void TC403_ValidateSpreadsheetBodyInCellB1()
        {
            TestCases.TC403();
        }

        [TestMethod]
        public void TC404_ValidateSheetcBodyTextInNewFile()
        {
            TestCases.TC404();
        }
    }

    [TestClass]
    public class EdgeSheets : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            CommonFunctions.Login(GoogleLogin.Sites.Sheets);
            CommonFunctions.Delay(5000);
        }

        [TestInitialize()]
        public void InitializeTestCases()
        {
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(3000);
        }

        [TestMethod]
        public void TC401_ValidateFileExist()
        {
            TestCases.TC401();
        }

        [TestMethod]
        public void TC402_ValidateNewFileCreated()
        {
            TestCases.TC402();
        }

        [TestMethod]
        public void TC403_ValidateSpreadsheetBodyInCellB1()
        {
            TestCases.TC403();
        }

        [TestMethod]
        public void TC404_ValidateSheetcBodyTextInNewFile()
        {
            TestCases.TC404();
        }
    }

    [TestClass]
    public class FirefoxSheets : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            CommonFunctions.Login(GoogleLogin.Sites.Sheets);
            CommonFunctions.Delay(5000);
        }

        [TestInitialize()]
        public void InitializeTestCases()
        {
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(3000);
        }

        [TestMethod]
        public void TC401_ValidateFileExist()
        {
            TestCases.TC401();
        }

        [TestMethod]
        public void TC402_ValidateNewFileCreated()
        {
            TestCases.TC402();
        }

        [TestMethod]
        public void TC403_ValidateSpreadsheetBodyInCellB1()
        {
            TestCases.TC403();
        }

        [TestMethod]
        public void TC404_ValidateSheetcBodyTextInNewFile()
        {
            TestCases.TC404();
        }
    }
}
