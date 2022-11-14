using GoogleFramework;

namespace SeleniumWebdriverCSharp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("Gmail")]
        public void TestMethod1()
        {
            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.TakeScreenshot("Test");
            CommonFunctions.Delay(1000);
        }
    }
}