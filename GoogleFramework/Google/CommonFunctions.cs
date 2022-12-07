using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.IO;

namespace GoogleFramework
{
    public class CommonFunctions    {
        private static readonly log4net.ILog logger = log4net.LogManager.
            GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);
        internal static readonly CommonFunctions commonFunctions = new();
        public CommonFunctions common = commonFunctions;

        public TestContext? TestContext { get; set; }

        public static void Login(GoogleLogin.Sites site) => GoogleLogin.Go(site, Driver.Instance!);

        public static void LogInfo(string message) => logger.Info(string.Format(message));


        public static void LogWarn(string message) => logger.Warn(string.Format(message));


        public static void LogError(string message) => logger.Error(string.Format(message));

        public static void Delay(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
            logger.Info(string.Format("Delay miliseconds: " + miliSeconds.ToString()));
        }

        public static IWebElement FindElement(By by)
        {
            LogInfo("Finding element");
            IWebElement? findElement = null;
            try
            {
                findElement = Driver.Instance!.FindElement(by);
                LogInfo("Element found, Tag: " + findElement.TagName+ " Element: "+findElement.Text);

            }
            catch (Exception e)
            {
                LogError("Error to find element: " + e.Message.ToString());
            }
            Delay(500);

            return findElement!;
        }

        public static bool DoesElementExist(By element)
        {
            bool exists;
            exists = WaitElementPresent(element);
            LogInfo("Element exist: " + exists.ToString() + " and Element is: " + element.ToString());
            return exists;
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LogInfo("Finding elements");
            ReadOnlyCollection<IWebElement>? findElements = null;
            try
            {
                findElements = Driver.Instance!.FindElements(by);
                LogInfo("Elements found: " + findElements!.Count.ToString());

            }
            catch (Exception e)
            {
                LogError("Error: " + e.Message.ToString());
            }
            Delay(500);

            return findElements!;
        }

        public static void TakeScreenshot(string testName)
        {
            LogInfo("Taking screenshot");
            ITakesScreenshot? instance = Driver.Instance as ITakesScreenshot;
            var screenshot = instance!.GetScreenshot();
            var fileName = $"{"sShot-"}{testName}{"_"}{DateTime.Now.Ticks}{".jpg"}";
            var path = "C:\\Temp\\" + fileName;
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            LogInfo("ScreenShot taken: " + path);
        }

        public static void SendKey(By by, string text)
        {
            LogInfo("Sending key");
            bool elementExists = WaitElementPresent(by);
            if (elementExists)
            {
                IWebElement element = FindElement(by);
                element.SendKeys(text);
                Delay(1000);
                LogInfo("Sendkey: " + text + " to element: " + element.Text.ToString());
            }
        }

        public static void SendKeyActionBuilder(By by, string text)
        {
            LogInfo("Sending Key using Action builder");
            bool elementExists = WaitElementPresent(by);
            if (elementExists)
            {
                Actions builder = new(Driver.Instance);
                builder.MoveToElement(Driver.Instance!.FindElement(by)).Click().SendKeys(text).Perform();
                LogInfo("Sendkey: " + text + " to element: " + by.ToString());
            }
        }

        public static void SendKeyAndEnter(By by, string text)
        {
            LogInfo("Sending Key and pressing Enter");
            bool elementExists = WaitElementPresent(by);
            if (elementExists)
            {
                IWebElement element = FindElement(by);
                element.SendKeys(text);
                Delay(1000);
                element.SendKeys(Keys.Return);
                Delay(1000);
                LogInfo("Sendkey: " + text + " to element: " + element.Text.ToString());
            }
        }

        public static bool WaitElementPresent(By by, int iTimeout = 10)
        {
            LogInfo("Waiting element be present");
            bool elementExists = false;
            WebDriverWait wait = new(Driver.Instance, TimeSpan.FromSeconds(iTimeout));
            ReadOnlyCollection<IWebElement> elements = wait.Until(drv => (drv.FindElements(by)));
            elementExists = (elements.Count > 0);
            LogInfo("Wait element. Seconds waited: "+iTimeout.ToString()+". - Elements found: " + elements.Count.ToString());
            Driver.Instance!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            return elementExists;
        }

        public static void WaitElementNotBePresent(By by, int iTimeout = 10)
        {
            LogInfo("Waiting element not be present");
            Driver.Instance!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            int i = 0;
            while(i < iTimeout)
            {
                try
                {
                    if (Driver.Instance!.FindElement(by).Displayed)
                        i++;
                }
                catch (NoSuchElementException)
                {
                    LogInfo("Element should be no longer present.");
                    break;
                }
            }
            Driver.Instance!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void Click(By by)
        {
            LogInfo("Clicking element");
            try
            {
                int elem = 1;
                ReadOnlyCollection<IWebElement> findElements = Driver.Instance!.FindElements(by);
                foreach (IWebElement element in findElements)
                {
                    LogInfo("Total element: "+ findElements.Count.ToString()+" of "+elem.ToString());
                    if (element.Displayed)
                    {
                        LogInfo("Element found, Tag: " + element.TagName + " Element: " + element.Text);
                        Actions builder = new(Driver.Instance);
                        LogInfo("Click element: " + element.Text);
                        builder.MoveToElement(Driver.Instance!.FindElement(by)).Click().Perform();
                    }
                    elem++;
                }
            }
            catch (Exception e)
            {
                LogError("Error: " + e.Message.ToString());
            }
            Delay(700);
        }


        public static void RightClick(By by)
        {
            LogInfo("Right-Click element");
            Actions builder = new(Driver.Instance);
            builder.MoveToElement(Driver.Instance!.FindElement(by)).ContextClick().Perform();
            LogInfo("Right-Click element: " + Driver.Instance.FindElement(by).Text);
            Delay(2000);
        }
        public static void Click_Parent(By by)
        {
            LogInfo("Clicking parent element");
            IWebElement myElement = Driver.Instance!.FindElement(by);
            IWebElement parent = myElement.FindElement(By.XPath("./.."));
            Actions builder = new(Driver.Instance);
            LogInfo("Click Parent element: " + parent.Text);
            builder.MoveToElement(parent).Click().Perform();
        }

        public static void Clear_TextElement(By by)
        {
            LogInfo("Clear element, send text and press Enter");
            WaitElementPresent(by);
            IWebElement element = FindElement(by);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            Delay(2000);
        }

        public static void SwitchFrame(By by)
        {
            LogInfo("Switching frame");
            Driver.Instance!.SwitchTo().ParentFrame();
            Driver.Instance!.SwitchTo().DefaultContent();
            Driver.Instance.SwitchTo().Frame(Driver.Instance.FindElement(by));
        }

        public static string GetTextFromElement(By by)
        {
            string text = Driver.Instance!.FindElement(by).Text;
            LogInfo("Getting text from element: "+ text);
            LogInfo("Text found: " + text);
            return text;
        }

        public static void RefreshPage()
        {
            Driver.Instance!.Navigate().Refresh();
            LogInfo("Reloading browser");
        }

        public static void GoToPage(string url)
        {
            Driver.Instance!.Navigate().GoToUrl(url);
            LogInfo("Going to Webpage: "+ url);
        }

        public static void CloseTab(int tab)
        {
            Driver.Instance?.SwitchTo().Window(Driver.Instance.WindowHandles[tab]).Close();
            LogInfo("Closing browser tab: " +tab.ToString());
        }

        public static void GoToTab(int tab) 
        {
            Driver.Instance?.SwitchTo().Window(Driver.Instance.WindowHandles[tab]);
            LogInfo("Going to tab: " + tab.ToString());
        }    
    }
}
