using GoogleFramework;
using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CommonFunctions.Login(GoogleLogin.Sites.Docs);
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
        public void TC302_ValidateNewFileCreated()
        {
            TestCases.TC302();
        }

        [TestMethod]
        public void TC303_ValidateDocBody()
        {
            TestCases.TC303();
        }

        [TestMethod]
        public void TC304_ValidateDocHeaders()
        {
            TestCases.TC304();
        }

        [TestMethod]
        public void TC305_ValidateDocBodyNewText()
        {
            TestCases.TC305();
        }
    }
}
