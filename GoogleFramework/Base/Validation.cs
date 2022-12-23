using System.Collections.ObjectModel;
using System.Reflection;
using OpenQA.Selenium;

namespace GoogleFramework
{
    public class Validation : CommonFunctions
    {
        /// <summary>
        /// Return if element is visible
        /// </summary>
        /// <param name="by">Enter XPath element</param>
        /// <returns>return boolean</returns>
        public static bool IsElementVisible(By by)
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

        /// <summary>
        /// Is the element not visible
        /// </summary>
        /// <param name="by">Enter XPath element</param>
        /// <returns>Returns a boolean if elemente is NOT visible</returns>
        public static bool IsElementNotVisible(By by)
        {
            bool isVisible = DoesElementExist(by);
            LogInfo("Is element visible: " + isVisible.ToString() + ". Element is, XPath: " + by.ToString());

            return isVisible;
        }

        /// <summary>
        /// Is the text element valid?
        /// </summary>
        /// <param name="by">Enter XPath element</param>
        /// <param name="text">Enter the expected text</param>
        /// <returns>Return boolean if text is valid</returns>
        public static bool IsTextElementValid(By by, string text)
        {
            bool isValid = false;
            ReadOnlyCollection<IWebElement> elements = FindElements(by);
            if (elements != null)
            {
                foreach (IWebElement element in elements)
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
            LogInfo("Is text valid: " + isValid.ToString() + ". Text is: " + text);
            return isValid;
        }

        /// <summary>
        /// Does the object exists
        /// </summary>
        /// <param name="text">Enter the Text to be validated</param>
        /// <param name="objectName">Enter the element to be validate, ex: class, id, aria-label...</param>
        /// <param name="type">Enter the tag elemente. Ex, div, button, span, p</param>
        /// <returns>Return boolean if the object was found</returns>
        public static bool DoesObjectExist(string text, string objectName, string type)
        {
            By element = By.XPath("//" + type + "[@" + objectName + "='" + text + "']");
            bool exists = DoesElementExist(element);
            LogInfo("Does the object exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Validate if the file exists in Google Drive
        /// </summary>
        /// <param name="fileName">Enter file name</param>
        /// <returns>Returns if the file exists</returns>
        public static bool DoesFileInGDriveExists(string fileName)
        {
            By element = By.XPath("//div[@class='KL4NAf '][contains(text(),'" + fileName + "')]");
            bool exists = DoesElementExist(element);
            LogInfo("Does the FileName exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Validate if the Calendar event exists in Google Calendar
        /// </summary>
        /// <param name="eventName">Enter the event name</param>
        /// <returns>Return boolean if the element was found</returns>
        public static bool DoesCalendarEventExist(string eventName)
        {
            By element = By.XPath("//span[@class='FAxxKc'][contains(text(),'" + eventName + "')]");
            bool exists = DoesElementExist(element);
            LogInfo("Does the EventName exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Check if the Calendar Body Text Message exists
        /// </summary>
        /// <param name="textBody">Enter the body text</param>
        /// <returns>Return boolean if text exists</returns>
        public static bool DoesCalendarTextMessageBodyExist(string textBody)
        {
            By element = By.XPath("//*[@id='xDetDlgDesc'][contains(text(),'" + textBody + "')]");
            bool exists = DoesElementExist(element);
            LogInfo("Does the Event TextBody exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Does the guest exists in the Calendar event
        /// </summary>
        /// <param name="guest">Enter the event name</param>
        /// <returns>Return boolean if guest in the event exists</returns>
        public static bool DoesGuestExist(string guest)
        {
            By element = By.XPath("//div[@aria-label='Guests']//span[contains(text(),'" + guest + "')]");
            bool exists = DoesElementExist(element);
            LogInfo("Does the Guest exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Does the file exists in Docs, Sheets and Slides
        /// </summary>
        /// <param name="file">Enter file name</param>
        /// <returns>Returns a boolean if the file was found</returns>
        public static bool DoesFileExistDocsSheetsSlides(string file)
        {
            By element = By.XPath("//div[@class='docs-homescreen-list-item-title-value'][contains(text(),'" + file + "')]");
            bool exists = DoesElementExist(element);
            LogInfo("Does the File in Docs/Sheets/Slides exist: " + exists.ToString());

            return exists;
        }

        /// <summary>
        /// Does the text exists in the list
        /// </summary>
        /// <param name="text">Enter the text to be validated</param>
        /// <param name="list">Enter the list with texts</param>
        /// <returns>Returns boolean if the text was found</returns>
        public static bool DoesTextContainsInList(string text, IList<string> list)
        {
            bool exists = false;
            foreach (string fileName in list)
            {
                if (fileName.Contains(text))
                    exists = true;
            }
            LogInfo("Does the Text exist in the list: " + exists.ToString() + ". And text is: " + text);

            return exists;
        }

        /// <summary>
        /// Compare strings and returns if they match or not
        /// </summary>
        /// <param name="textOriginal">Enter the text to be validated</param>
        /// <param name="textExpected">Enter the expected text</param>
        /// <returns>Return boolean if the text was found</returns>
        public static bool DoesTextContainsInString(string textOriginal, string textExpected)
        {
            LogInfo("Original Text : " + textOriginal + ". Expected text: " + textExpected);
            return textOriginal.Contains(textExpected);
        }
    }
}
