using System.Collections.ObjectModel;
using System.Reflection;
using Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace GoogleFramework
{
    public class CommonFunctions
    {
        private static readonly log4net.ILog logger = log4net.LogManager.
            GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);
        internal static readonly CommonFunctions commonFunctions = new();
        public CommonFunctions common = commonFunctions;

        /// <summary>
        /// Login into the website
        /// </summary>
        /// <param name="site">Need the website Enum</param>
        public static void Login(GoogleLogin.Sites site) => GoogleLogin.Go(site, Driver.Instance!);

        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message">Enter the message</param>
        public static void LogInfo(string message) => logger.Info(string.Format(message));

        /// <summary>
        /// Enter the Warn
        /// </summary>
        /// <param name="message">Enter the message</param>
        public static void LogWarn(string message) => logger.Warn(string.Format(message));

        /// <summary>
        /// Enter the Error message
        /// </summary>
        /// <param name="message">Enter the message</param>
        public static void LogError(string message) => logger.Error(string.Format(message));

        /// <summary>
        /// Delay method
        /// </summary>
        /// <param name="miliSeconds">Enter the milisconds you want to wait</param>
        public static void Delay(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
            logger.Info(string.Format("Delay miliseconds: " + miliSeconds.ToString()));
        }

        /// <summary>
        /// Find element method
        /// </summary>
        /// <param name="by">Need the XPath to look the element</param>
        /// <returns>return the IWebElement of the element</returns>
        public static IWebElement FindElement(By by)
        {
            LogInfo("Finding element: "+by+" ...");
            IWebElement? findElement = null;
            try
            {
                findElement = Driver.Instance!.FindElement(by);
                LogInfo("Element found, Tag: " + findElement.TagName + " Element: " + findElement.Text);

            }
            catch (Exception e)
            {
                LogError("Error to find element: " + e.Message.ToString());
            }
            Delay(500);

            return findElement!;
        }

        /// <summary>
        /// Check if the element exist
        /// </summary>
        /// <param name="element">Enter the XPath element</param>
        /// <returns>Return boolean</returns>
        public static bool DoesElementExist(By element)
        {
            LogInfo("Checking if the element exists...");
            bool exists;
            WaitElementBePresent(element);
            ReadOnlyCollection<IWebElement> elements = FindElements(element);
            exists = (elements.Count >0);
            LogInfo("Element exist: " + exists.ToString() + " and Element is: " + element.ToString());
            return exists;
        }

        /// <summary>
        /// Find all elements according to the XPath argument
        /// </summary>
        /// <param name="by">Need the XPath argument</param>
        /// <returns>Return a collection of IWebelements</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LogInfo("Finding elements...");
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

        /// <summary>
        /// Take screenshot of the browser
        /// </summary>
        /// <param name="testName">Enter the TestName of file name you wish</param>
        public static void TakeScreenshot(string testName)
        {
            LogInfo("Taking screenshot...");
            ITakesScreenshot? instance = Driver.Instance as ITakesScreenshot;
            var screenshot = instance!.GetScreenshot();
            var fileName = $"{"sShot-"}{testName}{"_"}{DateTime.Now.Ticks}{".png"}";
            var path = "C:\\Temp\\" + fileName;
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            LogInfo("ScreenShot taken: " + path);
        }

        /// <summary>
        /// Send keyboard keys
        /// </summary>
        /// <param name="by">Enter the XPath of the element</param>
        /// <param name="text">Enter the text or Key</param>
        public static void SendKey(By by, string text)
        {
            LogInfo("Sending key...");
            WaitElementBePresent(by);
            bool elementExists = DoesElementExist(by);
            if (elementExists)
            {
                IWebElement element = FindElement(by);
                element.SendKeys(text);
                Delay(1000);
                LogInfo("Sendkey: " + text + " to element: " + element.Text.ToString());
            } else
            {
                LogError("Element does NOT exist.");
            }
        }

        /// <summary>
        /// Send the keyboard keys to the element. This method uses the Action class to send keys
        /// </summary>
        /// <param name="by">XPath element</param>
        /// <param name="text">enter the text</param>
        public static void SendKeyActionBuilder(By by, string text)
        {
            LogInfo("Sending Key using Action builder...");
            WaitElementBePresent(by);
            bool elementExists = DoesElementExist(by);
            if (elementExists)
            {
                Actions builder = new(Driver.Instance);
                builder.MoveToElement(Driver.Instance!.FindElement(by)).Click().SendKeys(text).Perform();
                LogInfo("Sendkey: " + text + " to element: " + by.ToString());
            }
        }

        /// <summary>
        /// Send keyboard key + Enter
        /// </summary>
        /// <param name="by">Enter the XPath of the element</param>
        /// <param name="text">Enter the text or Key</param>
        public static void SendKeyAndEnter(By by, string text)
        {
            LogInfo("Sending Key and pressing Enter...");
            WaitElementBePresent(by);
            bool elementExists = DoesElementExist(by);
            if (elementExists)
            {
                IWebElement element = FindElement(by);
                element.SendKeys(text);
                Delay(1000);
                element.SendKeys(Keys.Return);
                Delay(1000);
                LogInfo("Sendkey: " + text + " to element: " + element.Text.ToString());
            } else
            {
                LogError("Element NOT found");
            }
        }

        /// <summary>
        /// Wait element be present, the default time is 10 seconds.
        /// </summary>
        /// <param name="by">Enter XPath of the element</param>
        /// <param name="iTimeout">Enter seconds you wish to wait</param>
        /// <returns>Return boolean if it was successful</returns>
        public static void WaitElementBePresent(By by, int iTimeout = 10)
        {
            LogInfo("Waiting element be present...");
            WebDriverWait wait = new(Driver.Instance, TimeSpan.FromSeconds(iTimeout));
            ReadOnlyCollection<IWebElement> elements = wait.Until(drv => drv.FindElements(by));
            LogInfo("Wait element. Seconds waited: " + iTimeout.ToString() + ". - Elements found: " + elements.Count.ToString());
            Driver.Instance!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        /// <summary>
        /// Wait element not be present in the browser
        /// </summary>
        /// <param name="by">Enter XPath of the element</param>
        /// <param name="iTimeout">Enter seconds you wish to wait</param>
        public static void WaitElementNotBePresent(By by, int iTimeout = 10)
        {
            LogInfo("Waiting element not be present...");
            Driver.Instance!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            int i = 0;
            while (i < iTimeout)
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

        /// <summary>
        /// Click method
        /// </summary>
        /// <param name="by">Enter XPath element</param>
        public static void Click(By by)
        {
            LogInfo("Clicking element...");
            WaitElementBePresent(by);
            try
            {
                int elem = 1;
                ReadOnlyCollection<IWebElement> findElements = Driver.Instance!.FindElements(by);
                foreach (IWebElement element in findElements)
                {
                    LogInfo("Total element: " + findElements.Count.ToString() + " of " + elem.ToString());
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

        /// <summary>
        /// Right Click Method
        /// </summary>
        /// <param name="by">Enter the XPath of the element</param>
        public static void RightClick(By by)
        {
            LogInfo("Right-Click element...");
            WaitElementBePresent(by);
            Actions builder = new(Driver.Instance);
            builder.MoveToElement(Driver.Instance!.FindElement(by)).ContextClick().Perform();
            LogInfo("Right-Click element: " + Driver.Instance.FindElement(by).Text);
            Delay(2000);
        }

        /// <summary>
        /// Click Parent element. Sometimes a element is not clickable
        /// </summary>
        /// <param name="by">Enter the XPath of the element</param>
        public static void Click_Parent(By by)
        {
            LogInfo("Clicking parent element...");
            WaitElementBePresent(by);
            IWebElement myElement = Driver.Instance!.FindElement(by);
            IWebElement parent = myElement.FindElement(By.XPath("./.."));
            Actions builder = new(Driver.Instance);
            LogInfo("Click Parent element: " + parent.Text);
            builder.MoveToElement(parent).Click().Perform();
        }

        /// <summary>
        /// Clear the text of the element
        /// </summary>
        /// <param name="by">Enter the XPath of the element</param>
        public static void Clear_TextElement(By by)
        {
            LogInfo("Clear element, send text and press Enter...");
            WaitElementBePresent(by);
            IWebElement element = FindElement(by);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            Delay(2000);
            LogInfo("Element cleared: "+element.Text);
        }

        /// <summary>
        /// Switch frames
        /// </summary>
        /// <param name="by">Enter the XPath iFrame</param>
        public static void SwitchFrame(By by)
        {
            LogInfo("Switching frame...");
            Driver.Instance!.SwitchTo().ParentFrame();
            Driver.Instance!.SwitchTo().DefaultContent();
            Driver.Instance.SwitchTo().Frame(Driver.Instance.FindElement(by));
        }

        /// <summary>
        /// Get text from the element
        /// </summary>
        /// <param name="by">Enter XPath element to get the text</param>
        /// <returns>Return the text of the element</returns>
        public static string GetTextFromElement(By by)
        {
            WaitElementBePresent(by);
            string text = Driver.Instance!.FindElement(by).Text;
            LogInfo("Getting text from element: " + text+" ...");
            LogInfo("Text found: " + text);
            return text;
        }

        /// <summary>
        /// Refresh browser page
        /// </summary>
        public static void RefreshPage()
        {
            Driver.Instance!.Navigate().Refresh();
            LogInfo("Reloading browser");
        }

        /// <summary>
        /// Go to Page
        /// </summary>
        /// <param name="url">Enter the URL page</param>
        public static void GoToPage(string url)
        {
            Driver.Instance!.Navigate().GoToUrl(url);
            LogInfo("Going to Webpage: " + url);
        }

        /// <summary>
        /// Close the tab. Browser tabs start from 0, like an array
        /// </summary>
        /// <param name="tab">Enter the tab number</param>
        public static void CloseTab(int tab)
        {
            LogInfo("Closing Tab: "+ tab.ToString()+" ...");
            Driver.Instance?.SwitchTo().Window(Driver.Instance.WindowHandles[tab]).Close();
            LogInfo("Closing browser tab: " + tab.ToString());
        }

        /// <summary>
        /// Go to the browser Tab
        /// </summary>
        /// <param name="tab">Enter Tab number</param>
        public static void GoToTab(int tab)
        {
            LogInfo("Going to Tab: " + tab.ToString() + " ...");
            Driver.Instance?.SwitchTo().Window(Driver.Instance.WindowHandles[tab]);
            LogInfo("Going to tab: " + tab.ToString());
        }

        /// <summary>
        /// Return if element is visible
        /// </summary>
        /// <param name="by">Enter XPath element</param>
        /// <returns>return boolean</returns>
        public static bool IsVisible(By by)
        {
            bool isVisible = false;
            try
            {
                ReadOnlyCollection<IWebElement> elements = FindElements(by);
                foreach (IWebElement element in elements)
                {
                    if (element.Displayed)
                    {
                        isVisible = true;
                        LogInfo("Is element visible: " + isVisible.ToString() + ". Element is: " + element.Text);
                    }
                }
            }
            catch
            {
                LogError("Element not found...");
            }
            return isVisible;
        }
    }
}
