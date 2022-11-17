using GoogleFramework;
using System.IO;

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

        [TestCleanup]
        public void CleanUp()
        {  
            if (TestContext!.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                CommonFunctions.TakeScreenshot(TestContext.TestName.ToString());
            }
        }
    }
}
