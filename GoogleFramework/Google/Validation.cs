using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Linq;
using OpenQA.Selenium;
using static GoogleFramework.Driver;

namespace GoogleFramework
{
    public class Validation : CommonFunctions
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(type: MethodBase.GetCurrentMethod()!.DeclaringType);

        private static bool DoesElementExist(By elem)
        {
            bool exists;
            try
            {
                ReadOnlyCollection<IWebElement> elements = FindElements(elem);
                exists = elements.Count > 0 ? true : false;
                logger.Info(String.Format("Elements found: " + elements.Count().ToString()));
            }
            catch
            {
                exists= false;
                logger.Error(String.Format("Element invalid: " + elem.ToString()));
            }
            return exists;
        }
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
            bool isVisible;
            isVisible = DoesElementExist(by);
            logger.Info(String.Format("Is element visible: " + isVisible.ToString() + ". Element is, XPath: " + by.ToString()));
            
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
            bool exists;
            By element = By.XPath("//"+type+ "[@"+objectName+"='"+text+"']");
            exists = DoesElementExist(element);
            logger.Info(String.Format("Does the object exist: " + exists.ToString()));
            
            return exists;
        }

        public static bool DoesFileInGDriveExists(string fileName)
        {
            bool exists;
            By element = By.XPath("//div[@class='KL4NAf '][contains(text(),'" + fileName + "')]");
            exists = DoesElementExist(element);
            logger.Info(String.Format("Does the FileName exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesCalendarEventExist(string eventName)
        {
            bool exists;
            By element = By.XPath("//span[@class='FAxxKc'][contains(text(),'" + eventName + "')]");
            exists = DoesElementExist(element);    
            logger.Info(String.Format("Does the EventName exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesCalendarTextMessageBodyExist(string textBody)
        {
            bool exists;
            By element = By.XPath("//div[@id='xDetDlgDesc'][contains(text(),'" + textBody + "')]");
            exists = DoesElementExist(element);
            logger.Info(String.Format("Does the Event TextBody exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesGuestExist(string guest)
        {
            bool exists;
            By element = By.XPath("//div[@aria-label='Guests']//span[contains(text(),'" + guest + "')]");
            exists = DoesElementExist(element);
            logger.Info(String.Format("Does the Event TextBody exist: " + exists.ToString()));

            return exists;
        }
    }
}
