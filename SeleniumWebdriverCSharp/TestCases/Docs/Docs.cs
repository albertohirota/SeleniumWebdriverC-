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
        public void TC301_ValidateNewFileCreated()
        {
            //Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File event should exist");
        }
    }
}
