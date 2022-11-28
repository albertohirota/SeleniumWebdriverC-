using GoogleFramework;
using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriverCSharp.Calendar
{
    [TestClass]
    public class ChromeCalendar: TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Calendar);
            CommonFunctions.Delay(5000);
        }

        [TestMethod]
        public void TC201_ValidateNewCalendarEvent()
        {
            TestCases.TC201();
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            TestCases.TC202();
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            TestCases.TC203();
        }
    }

    [TestClass]
    public class EdgeCalendar : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            CommonFunctions.Login(GoogleLogin.Sites.Calendar);
            CommonFunctions.Delay(5000);
        }

        [TestMethod]
        public void TC201_ValidateNewCalendarEvent()
        {
            TestCases.TC201();
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            TestCases.TC202();
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            TestCases.TC203();
        }
    }

    [TestClass]
    public class FirefoxCalendar : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            CommonFunctions.Login(GoogleLogin.Sites.Calendar);
            CommonFunctions.Delay(5000);
        }

        [TestMethod]
        public void TC201_ValidateNewCalendarEvent()
        {
            TestCases.TC201();
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            TestCases.TC202();
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            TestCases.TC203();
        }
    }
}
