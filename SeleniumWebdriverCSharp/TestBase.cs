using GoogleFramework;
using Login;
using System.Security.Policy;

namespace SeleniumWebdriverCSharp
{
    [TestClass]
    public class TestBaseClass
    {
        public TestContext? TestContext { get; set; }
        private static readonly string temporaryDirectory = "C:\\Temp";

        [AssemblyInitializeAttribute]
        public static void TestInitialize(TestContext context)
        {
            
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Directory.CreateDirectory(temporaryDirectory);
            DirectoryInfo directory = new(temporaryDirectory);
            foreach (FileInfo file in directory.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                    CommonFunctions.LogWarn("File could not be deleted: "+file);
                }
            }
            CommonFunctions.LogInfo("--------Test Initializer--------");
        }

        [TestInitialize]
        public void TestSetup()
        {
            string testMethodName = TestContext!.TestName;
            CommonFunctions.LogInfo("--------Starting Test Case: "+ testMethodName + "--------");
        }

        [TestCleanup]
        public void CleanUp()
        {  
            if (TestContext!.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string testMethodName = TestContext!.TestName;
                CommonFunctions.LogError("TEST CASE FAILURE: "+testMethodName+" - Adding Screenshot...");
                CommonFunctions.TakeScreenshot(testMethodName);
            }
            CommonFunctions.LogInfo("--------End of the Test Case--------");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            CommonFunctions.LogInfo("--------Running Cleanup--------"); 
            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            RunGmailCleanUpFolder();
            RunCalendarCleanUpFolder();
            RunGoogleDriveCleanUp();
            RunDocsCleanUp();

            Driver.InstanceClose();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void classcleanUp()
        {
            Driver.CloseBrowser();
        }

        public static void RunGmailCleanUpFolder()
        {
            CommonFunctions.LogInfo("--------Gmail Cleanup--------");
            GmailPage.GoToInbox();
            CommonFunctions.Delay(2000);
            GmailPage.Click_CheckBoxSelectAll();
            CommonFunctions.Delay(2000);
            GmailPage.Click_ButtonDelete(); 
            CommonFunctions.Delay(2000);
        }

        public static void RunCalendarCleanUpFolder()
        {
            CommonFunctions.LogInfo("--------Calendar Cleanup--------");
            CommonFunctions.GoToPage(GoogleLogin.CalendarUrl);
            CommonFunctions.Delay(3000);
            string[] events = { "TC201" ,"TC202", "TC203"};
            foreach (string ev in events)
            {
                if (Validation.DoesCalendarEventExist(ev))
                {
                    CalendarPage.DeleteEvent(ev);
                    CalendarPage.Click_ButtonSend();
                }
            }
        }

        public static void RunGoogleDriveCleanUp()
        {
            CommonFunctions.LogInfo("--------Calendar GoogleDrive--------");
            CommonFunctions.GoToPage(GoogleLogin.DriveUrl);
            CommonFunctions.Delay(3000);
            string[] files = { "TC103"};
            foreach (string file in files)
            {
                if (Validation.DoesFileInGDriveExists(file))
                    GDrivePage.DeleteFileInDrive(file);
            }
        }

        public static void RunDocsCleanUp()
        {
            CommonFunctions.LogInfo("--------Calendar GoogleDocs--------");
            CommonFunctions.GoToPage(GoogleLogin.DocUrl);
            CommonFunctions.Delay(3000);
        }

    }
}
