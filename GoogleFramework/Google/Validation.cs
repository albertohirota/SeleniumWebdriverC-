using System;
using System.Collections.ObjectModel;
using System.Reflection;
using OpenQA.Selenium;
using static GoogleFramework.Driver;

namespace GoogleFramework
{
    public class Validation : CommonFunctions
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);

        public static bool IsElementVisible(By by)
        {
            IWebElement element = WaitElementPresent(by);
            bool isVisible = element.Displayed;
            logger.Info(String.Format("Is element visible: " + isVisible.ToString() + ". Element is: "+element.TagName.ToString() ));
            return isVisible;
        }

        public static bool IsTextElementValid(By by, string text)
        {
            bool isValid = false;
            ReadOnlyCollection<IWebElement> elements = FindElements(by);
            if (elements!= null)
            {
                foreach(IWebElement element in elements)
                {
                    if (element.Text.ToString().Contains(text))
                    {
                        isValid = true; break;
                    }
                    else
                    {
                        ReadOnlyCollection<IWebElement> child = element.FindElements(By.XPath(".//*"));
                        foreach (IWebElement childElem in child)
                        {
                            if (childElem.Text.ToString().Contains(text))
                            {
                                isValid = true; break;
                            }
                        }
                    }
                }
            }
            logger.Info(String.Format("Is text valid: " + isValid.ToString() + ". Text is: " + text));
            return isValid;
        }
    }
}
