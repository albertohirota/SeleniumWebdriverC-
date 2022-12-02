using GoogleFramework;
using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriverCSharp.Docs
{
    [TestClass]
    public class ChromeDocs: TestBaseClass
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

        [TestMethod]
        public void TC301_ValidateFileExist()
        {
            TestCases.TC301();
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
    }
}
