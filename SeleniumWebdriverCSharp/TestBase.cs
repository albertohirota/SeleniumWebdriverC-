using GoogleFramework;
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
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Directory.CreateDirectory(temporaryDirectory);
            DirectoryInfo directory = new(temporaryDirectory);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        [TestInitialize]
        public void TestSetup()
        {
            string testMethodName = TestContext!.TestName;
            CommonFunctions.LogInfo("--------Starting Test Case: "+ testMethodName + "--------Test: ");
        }

        [TestCleanup]
        public void CleanUp()
        {  
            if (TestContext!.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                CommonFunctions.TakeScreenshot(TestContext.TestName.ToString());
            }
            CommonFunctions.LogInfo("--------End of the Test Case--------");
        }
    }
}
