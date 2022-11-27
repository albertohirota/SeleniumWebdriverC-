using GoogleFramework;
using Login;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;

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
            CommonFunctions.LogInfo("--------Test Initializer--------");
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
            CommonFunctions.LogInfo("--------Running Gmail Cleanup--------"); 
            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            RunGmailCleanUpFolder();

            Driver.InstanceClose();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void classcleanUp()
        {
            Driver.CloseBrowser();
        }

        public static void RunGmailCleanUpFolder()
        {
            GmailPage.GoToInbox();
            CommonFunctions.Delay(2000);
            GmailPage.Click_CheckBoxSelectAll();
            CommonFunctions.Delay(2000);
            GmailPage.Click_ButtonDelete(); 
            CommonFunctions.Delay(2000);
        }

    }
}
