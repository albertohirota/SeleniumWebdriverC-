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
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC201");
            CalendarPage.Click_ButtonSaveSummaryPage();
            Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File name should exist");
        }
    }
}
