using GoogleFramework;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class ChromeReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            TestCases.TC006Setup();
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            TestCases.TC006();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            TestCases.TC007();
        }
    }

    [TestClass]
    public class EdgeReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            TestCases.TC006Setup();
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            TestCases.TC006();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            TestCases.TC007();
        }
    }

    [TestClass]
    public class FirefoxReplyAndForwardEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            TestCases.TC006Setup();
        }

        [TestMethod]
        public void TC006_ValidateReplyPage()
        {
            TestCases.TC006();
        }

        [TestMethod]
        public void TC007_ValidateForwardAndSendEmail()
        {
            TestCases.TC007();
        }
    }
}
