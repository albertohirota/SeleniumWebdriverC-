using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using static GoogleFramework.Driver;

namespace GoogleFramework
{
    public class CommonFunctions
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(type: MethodBase.GetCurrentMethod().DeclaringType);

        public TestContext? TestContext { get; set; }

        public static void Delay(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
            logger.Info(String.Format("Delay miliseconds: " + miliSeconds.ToString()));
        }

        public IWebElement FindElement(By by)
        {
            IWebElement? findElement = null;
            try { 
                findElement = Driver.Instance.FindElement(by);
                logger.Info(String.Format("Element found: " + findElement.ToString()));
            } catch (Exception e)
            {
                logger.Error(String.Format("Error to find element: " + e.Message.ToString()));
            }
            Delay(500);
#pragma warning disable CS8603 // Dereference of a possibly null reference.
            return findElement;
        }

        public static void TakeScreenshot(string testName)
        {
            ITakesScreenshot? instance = Driver.Instance as ITakesScreenshot;
            var screenshot = instance!.GetScreenshot();
            var fileName = $"{testName}{"_ss_"}{DateTime.Now.Ticks}{".jpg"}";
            var path = "C:\\Temp\\"+fileName;
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
            logger.Info(String.Format("ScreenShot taken: " + path));
        }

    }
}
