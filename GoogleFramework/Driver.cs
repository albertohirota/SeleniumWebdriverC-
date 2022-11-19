using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace GoogleFramework
{
    public class Driver
    {

        public enum Browsers
        {
            Firefox,
            Chrome,
            Edge
        }

        public static IWebDriver? Instance { get; set; }

        private static readonly ILog logger = LogManager.GetLogger(type: System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

        public static void Initialize(Browsers browser)
        {
            Instance?.Quit();

            switch(browser)
            {
                case Browsers.Firefox:
                    FirefoxOptions fOptions = new()
                    {
                        AcceptInsecureCertificates= true
                    };
                    Instance = new FirefoxDriver(fOptions);
                    break; 

                case Browsers.Chrome:
                    ChromeOptions cOptions = new();
                    Instance = new ChromeDriver(cOptions);
                    break; 

                case Browsers.Edge:
                    EdgeOptions eOptions = new();
                    Instance = new EdgeDriver(eOptions);
                    break;
            }
            logger.Info(String.Format("Browser started: "+ browser.ToString()));
        }

        public static void CloseBrowser() 
        {
            Instance!.Close();
            Instance.Dispose();
            logger.Info(String.Format("Browser Closed."));
        }


    }
}