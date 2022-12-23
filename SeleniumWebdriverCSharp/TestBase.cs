using GoogleFramework;
using Login;

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
            string? testMethodName = TestContext!.TestName;
            CommonFunctions.LogInfo("--------Starting Test Case: "+ testMethodName + "--------");
        }

        [TestCleanup]
        public void CleanUp()
        {  
            if (TestContext!.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string? testMethodName = TestContext!.TestName;
                CommonFunctions.LogError("TEST CASE FAILURE: "+testMethodName+" - Adding Screenshot...");
                CommonFunctions.TakeScreenshot(testMethodName!);
            }
            CommonFunctions.LogInfo("--------End of the Test Case--------");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            CommonFunctions.LogInfo("--------Running Assembly Cleanup--------");
            CommonFunctions.Delay(5000);
            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            RunGmailCleanUpFolder();
            RunCalendarCleanUpFolder();
            RunGoogleDriveCleanUp();
            RunDocsCleanUp();
            RunSheetsCleanUp();

            Driver.InstanceClose();
        }

        [ClassCleanup()] //InheritanceBehavior.BeforeEachDerivedClass
        public static void ClassCleanUp()
        {
            CommonFunctions.LogInfo("--------Running Class Cleanup--------");
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
                while (Validation.DoesCalendarEventExist(ev))
                {
                    CalendarPage.DeleteEvent(ev);
                    CalendarPage.Click_ButtonSend();
                    CommonFunctions.Delay(1500);
                }
            }
            CommonFunctions.Delay(3000);
        }

        public static void RunGoogleDriveCleanUp()
        {
            CommonFunctions.LogInfo("--------GoogleDrive Cleanup--------");
            CommonFunctions.GoToPage(GoogleLogin.DriveUrl);
            CommonFunctions.Delay(3000);
            string[] files = { "Untitled document", "TC103","TC302","TC305","TC402", "Untitled spreadsheet", "TC404" };
            foreach (string file in files)
            {
                while (Validation.DoesFileInGDriveExists(file))
                    GDrivePage.DeleteFileInDrive(file);
            }
        }

        public static void RunDocsCleanUp()
        {
            CommonFunctions.LogInfo("--------Google Docs Cleanup--------");
            CommonFunctions.GoToPage(GoogleLogin.DocUrl);
            CommonFunctions.Delay(3000);
            string[] files = { "TC302","TC305" };
            foreach (string file in files)
            {
                while (Validation.DoesFileExistDocsSheetsSlides(file))
                    GOfficePage.DeleteFile(file);
            }
        }

        public static void RunSheetsCleanUp()
        {
            CommonFunctions.LogInfo("--------Google Sheets Cleanup--------");
            CommonFunctions.GoToPage(GoogleLogin.SheetlUrl);
            CommonFunctions.Delay(3000);
            string[] files = { "TC402", "TC404" };
            foreach (string file in files)
            {
                while (Validation.DoesFileExistDocsSheetsSlides(file))
                    GOfficePage.DeleteFile(file);
            }
        }
    }
}
