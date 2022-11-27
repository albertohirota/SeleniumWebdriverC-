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
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC101"), "File name should exist");
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            GDrivePage.Click_DriveMenuFolder("Shared with me");
            CommonFunctions.Delay(5000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("logo Lambton.PNG"), "File name should exist");
            GDrivePage.Click_DriveMenuFolder("My Drive");
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            GDrivePage.Click_NewFile();
            GDrivePage.Click_GoogleDocs();
            CommonFunctions.GoToTab(1);
            CommonFunctions.Delay(5000);
            GOfficePage.RenameDocumentName("TC103");
            CommonFunctions.Delay(3000);
            CommonFunctions.CloseTab(1);
            CommonFunctions.GoToTab(0);
            CommonFunctions.Delay(7000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC103"), "File name should exist");
            GDrivePage.DeleteFileInDrive("TC103");
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
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC101"), "File name should exist");
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            GDrivePage.Click_DriveMenuFolder("Shared with me");
            CommonFunctions.Delay(5000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("logo Lambton.PNG"), "File name should exist");
            GDrivePage.Click_DriveMenuFolder("My Drive");
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            GDrivePage.Click_NewFile();
            GDrivePage.Click_GoogleDocs();
            CommonFunctions.GoToTab(1);
            CommonFunctions.Delay(5000);
            GOfficePage.RenameDocumentName("TC103");
            CommonFunctions.Delay(3000);
            CommonFunctions.CloseTab(1);
            CommonFunctions.GoToTab(0);
            CommonFunctions.Delay(7000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC103"), "File name should exist");
            GDrivePage.DeleteFileInDrive("TC103");
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
        }

        [TestMethod]
        public void TC101_ValidateFileExists()
        {
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC101"), "File name should exist");
        }

        [TestMethod]
        public void TC102_ValidateFileExistInShareWithMeFolder()
        {
            GDrivePage.Click_DriveMenuFolder("Shared with me");
            CommonFunctions.Delay(5000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("logo Lambton.PNG"), "File name should exist");
            GDrivePage.Click_DriveMenuFolder("My Drive");
        }

        [TestMethod]
        public void TC103_ValidateCreatingOfNewFile()
        {
            GDrivePage.Click_NewFile();
            GDrivePage.Click_GoogleDocs();
            CommonFunctions.GoToTab(1);
            CommonFunctions.Delay(5000);
            GOfficePage.RenameDocumentName("TC103");
            CommonFunctions.Delay(3000);
            CommonFunctions.CloseTab(1);
            CommonFunctions.GoToTab(0);
            CommonFunctions.Delay(7000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC103"), "File name should exist");
            GDrivePage.DeleteFileInDrive("TC103");
        }
    }
}
