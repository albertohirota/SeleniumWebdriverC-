using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp.GDrive
{
    [TestClass]
    public class ChromeGDrive : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Chrome);
            CommonFunctions.Login(GoogleLogin.Sites.Drive);
        }

        [TestMethod]
        public void TC101_ValidateFileExists()
        {
            TestCases.TC101();
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            TestCases.TC102();
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            TestCases.TC103();
        }
    }

    [TestClass]
    public class EdgeGDrive : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Edge);
            CommonFunctions.Login(GoogleLogin.Sites.Drive);
        }

        [TestMethod]
        public void TC101_ValidateFileExists()
        {
            TestCases.TC101();
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            TestCases.TC102();
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            TestCases.TC103();
        }
    }

    [TestClass]
    public class FirefoxGDrive : TestBaseClass
    {
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeTest(TestContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Driver.Initialize(Driver.Browsers.Firefox);
            CommonFunctions.Login(GoogleLogin.Sites.Drive);
            CommonFunctions.Delay(2000);
        }

        [TestMethod]
        public void TC101_ValidateFileExists()
        {
            TestCases.TC101();
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            TestCases.TC102();
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            TestCases.TC103();
        }
    }
}
