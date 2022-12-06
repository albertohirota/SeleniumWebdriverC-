using GoogleFramework;

namespace SeleniumWebdriverCSharp.Gmail
{
    [TestClass]
    public class ChromeReceivingEmail: TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            TestCases.TC004Setup();
        }

        [TestMethod]
        public void TC004_ValidateBcc()
        {
            TestCases.TC004();
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            TestCases.TC005();
        }
    }

    [TestClass]
    public class EdgeReceivingEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            TestCases.TC004Setup();
        }

        [TestMethod]
        public void TC004_ValidateBcc()
        {
            TestCases.TC004();
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            TestCases.TC005();
        }
    }

    [TestClass]
    public class FirefoxReceivingEmail : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            TestCases.TC004Setup();
        }

        [TestMethod]
        public void TC004_ValidateBcc()
        {
            TestCases.TC004();
        }

        [TestMethod]
        public void TC005_ValidateReplyAllButtonIsDisplayed()
        {
            TestCases.TC005();
        }
    }
}
