using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GoogleFramework
{
    public class CommonFunctions    {
        private static readonly log4net.ILog logger = log4net.LogManager.
            GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);
        internal static readonly CommonFunctions commonFunctions = new();
        public CommonFunctions common = commonFunctions;

        public TestContext? TestContext { get; set; }

        public static void LogInfo(string message)
        {
            logger.Info(string.Format(message));
        }

        public static void Delay(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
            logger.Info(string.Format("Delay miliseconds: " + miliSeconds.ToString()));
        }

        public static void Login(GoogleLogin.Sites site)
        {
            GoogleLogin.Go(site, Driver.Instance!);
        }

        public static IWebElement FindElement(By by)
        {
            IWebElement? findElement = null;
            try
            {
                findElement = Driver.Instance!.FindElement(by);
                logger.Info(string.Format("Element found, XPath: " + by.ToString()));

            }
            catch (Exception e)
            {
                logger.Error(string.Format("Error to find element: " + e.Message.ToString()));
            }
            Delay(500);

            return findElement!;
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            ReadOnlyCollection<IWebElement>? findElements = null;
            try
            {
                findElements = Driver.Instance!.FindElements(by);
                logger.Info(string.Format("Elements found: " + findElements!.Count.ToString()));

            }
            catch (Exception e)
            {
                logger.Error(string.Format("Error: " + e.Message.ToString()));
            }
            Delay(500);

            return findElements!;
        }

        public static void TakeScreenshot(string testName)
        {
            ITakesScreenshot? instance = Driver.Instance as ITakesScreenshot;
            var screenshot = instance!.GetScreenshot();
            var fileName = $"{"sShot-"}{testName}{"_"}{DateTime.Now.Ticks}{".jpg"}";
            var path = "C:\\Temp\\" + fileName;
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
            logger.Info(string.Format("ScreenShot taken: " + path));
        }

        public static void SendKey(By by, string text)
        {
            IWebElement element = WaitElementPresent(by);
            element.SendKeys(text);
            Delay(1000);
            logger.Info(string.Format("Sendkey: " + text + " to element: " + element.TagName.ToString()));
        }

        public static IWebElement WaitElementPresent(By by, int iTimeout = 5)
        {
            IWebElement? findElement = null;
            logger.Info(string.Format("Waiting element be present, XPath: " + by.ToString()));
            for (int i = 0; i < iTimeout; i++)
            {
                findElement = FindElement(by);
                if (findElement == null)
                {
                    Delay(1000);
                    findElement = FindElement(by);
                }
                else break;
            }
            return findElement!;
        }

        public static void Click(By by)
        {
            try
            {
                IWebElement findElement = FindElement(by);
                logger.Info(string.Format("Element found, XPath: " + by!.ToString()));
                Actions builder = new(Driver.Instance);
                builder.MoveToElement(Driver.Instance!.FindElement(by)).Click().Perform();
                logger.Info(string.Format("Click element: " + by.ToString()));
            }
            catch (Exception e)
            {
                logger.Error(string.Format("Error: " + e.Message.ToString()));
            }
            Delay(700);
        }
    }
}
