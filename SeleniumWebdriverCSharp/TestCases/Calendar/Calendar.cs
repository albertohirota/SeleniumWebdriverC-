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
            Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File event should exist");
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC202");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_TextCalendarBody("This is TC202");
            CalendarPage.Click_ButtonSave();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC202");
            Assert.IsTrue(Validation.DoesCalendarTextMessageBodyExist("This is TC202"), "Text body calendar event should exist");
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC203");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_Guest("alberto.hirota@gmail.com");
            CalendarPage.Add_TextCalendarBody("This is TC203");
            CommonFunctions.Delay(2000);
            CalendarPage.Click_ButtonSave();
            CalendarPage.Click_ButtonSend();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC203");
            Assert.IsTrue(Validation.DoesGuestExist("alberto.hirota@gmail.com"), "Guest exist");
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
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC201");
            CalendarPage.Click_ButtonSaveSummaryPage();
            Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File event should exist");
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC202");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_TextCalendarBody("This is TC202");
            CalendarPage.Click_ButtonSave();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC202");
            Assert.IsTrue(Validation.DoesCalendarTextMessageBodyExist("This is TC202"), "Text body calendar event should exist");
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC203");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_Guest("alberto.hirota@gmail.com");
            CalendarPage.Add_TextCalendarBody("This is TC203");
            CommonFunctions.Delay(2000);
            CalendarPage.Click_ButtonSave();
            CalendarPage.Click_ButtonSend();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC203");
            Assert.IsTrue(Validation.DoesGuestExist("alberto.hirota@gmail.com"), "Guest exist");
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
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC201");
            CalendarPage.Click_ButtonSaveSummaryPage();
            Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File event should exist");
        }

        [TestMethod]
        public void TC202_ValidateCalendarTextBody()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC202");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_TextCalendarBody("This is TC202");
            CalendarPage.Click_ButtonSave();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC202");
            Assert.IsTrue(Validation.DoesCalendarTextMessageBodyExist("This is TC202"), "Text body calendar event should exist");
        }

        [TestMethod]
        public void TC203_ValidateUserGuest()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC203");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_Guest("alberto.hirota@gmail.com");
            CalendarPage.Add_TextCalendarBody("This is TC203");
            CommonFunctions.Delay(2000);
            CalendarPage.Click_ButtonSave();
            CalendarPage.Click_ButtonSend();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC203");
            Assert.IsTrue(Validation.DoesGuestExist("alberto.hirota@gmail.com"), "Guest exist");
        }
    }
}
