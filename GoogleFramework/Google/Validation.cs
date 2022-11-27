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
            bool isVisible = false;
            try {
                ReadOnlyCollection<IWebElement> elements = FindElements(by);
                foreach (IWebElement element in elements)
                {
                    if (element.Displayed)
                    {
                        isVisible= true;
                        logger.Info(String.Format("Is element visible: " + isVisible.ToString() + ". Element is: " + element.Text));
                    }
                }
            }
            catch {
                logger.Error(String.Format("Element not found..."));
            }
            return isVisible;
        }

        public static bool IsElementNotVisible(By by)
        {
            bool isVisible = true;
            try
            {
                ReadOnlyCollection<IWebElement> elements = FindElements(by);
                isVisible = elements.Count > 0? true: false ;
                logger.Info(String.Format("Is element visible: " + isVisible.ToString() + ". Element is, XPath: " + by.ToString()));
            } catch
            {
                logger.Error(String.Format("Element error..."));
            }
            
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

        public static bool DoesObjectExist(string text, string objectName, string type)
        {
            bool exists = false;
            By element = By.XPath("//"+type+ "[@"+objectName+"='"+text+"']");
            try
            {
                ReadOnlyCollection<IWebElement> elements = FindElements(element);
                exists = elements.Count > 0 ? true : false;
                logger.Info(String.Format("Does the object exist: " + exists.ToString() + ". Elements found: " + elements.Count.ToString()));
            }
            catch
            {
                logger.Error(String.Format("Element invalid: " + element.ToString()));
            }
            
            return exists;
        }

        public static bool DoesFileInGDriveExists(string fileName)
        {
            bool exists = false;
            By element = By.XPath("//div[@class='KL4NAf '][contains(text(),'" + fileName + "')]");
            try
            {
                ReadOnlyCollection<IWebElement> elements = FindElements(element);
                exists = elements.Count > 0 ? true : false;
                logger.Info(String.Format("Does the FileName exist: " + exists.ToString() + ". Elements found: " + elements.Count.ToString()));
            }
            catch
            {
                logger.Error(String.Format("Element invalid: " + element.ToString()));
            }

            return exists;
        }
    }
}
