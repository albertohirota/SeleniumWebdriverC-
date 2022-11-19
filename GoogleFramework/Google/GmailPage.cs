using OpenQA.Selenium.DevTools.V105.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GoogleFramework
{
    public class GmailPage : CommonFunctions
    {
        public static readonly By ButtonCompose = By.XPath("//div[@class='T-I T-I-KE L3']");
        public static readonly By ButtonSend = By.XPath("//*[@role='button' and text()='Send']");
        public static readonly By TitleEmail = By.XPath("//*[@class='aYF']");

        public static void Click_NewEmail() => Click(ButtonCompose);
    }
}
