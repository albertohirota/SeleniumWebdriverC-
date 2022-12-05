using System.Collections.ObjectModel;
using System.Reflection;
using OpenQA.Selenium;

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
            bool isVisible = CommonFunctions.DoesElementExist(by);
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
            By element = By.XPath("//"+type+ "[@"+objectName+"='"+text+"']");
            bool exists = CommonFunctions.DoesElementExist(element);
            logger.Info(String.Format("Does the object exist: " + exists.ToString()));
            
            return exists;
        }

        public static bool DoesFileInGDriveExists(string fileName)
        {
            By element = By.XPath("//div[@class='KL4NAf '][contains(text(),'" + fileName + "')]");
            bool exists = CommonFunctions.DoesElementExist(element);
            logger.Info(String.Format("Does the FileName exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesCalendarEventExist(string eventName)
        {
            By element = By.XPath("//span[@class='FAxxKc'][contains(text(),'" + eventName + "')]");
            bool exists = CommonFunctions.DoesElementExist(element);    
            logger.Info(String.Format("Does the EventName exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesCalendarTextMessageBodyExist(string textBody)
        {
            By element = By.XPath("//*[@id='xDetDlgDesc'][contains(text(),'" + textBody + "')]");
            bool exists = CommonFunctions.DoesElementExist(element);
            logger.Info(String.Format("Does the Event TextBody exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesGuestExist(string guest)
        {
            By element = By.XPath("//div[@aria-label='Guests']//span[contains(text(),'" + guest + "')]");
            bool exists = CommonFunctions.DoesElementExist(element);
            logger.Info(String.Format("Does the Event TextBody exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesFileExistDocsSheetsSlides(string file)
        {
            By element = By.XPath("//div[@class='docs-homescreen-list-item-title-value'][contains(text(),'" + file + "')]");
            bool exists = CommonFunctions.DoesElementExist(element);
            logger.Info(String.Format("Does the Event TextBody exist: " + exists.ToString()));

            return exists;
        }

        public static bool DoesTextContainsInList(string text, IList<string> list)
        {
            bool exists = false;
            foreach (string fileName in list)
            {
                if (fileName.Contains(text))
                    exists = true;
            }
            logger.Info(String.Format("Does the Text exist in the list: " +exists.ToString() +". And text is: " + text));

            return exists;
        }

        public static bool DoesTextContainsInString(string textOriginal, string textExpected)
        {
            logger.Info(String.Format("Original Text : " + textOriginal + ". Expected text: "+ textExpected));
            return textOriginal.Contains(textExpected);
        }
    }
}
