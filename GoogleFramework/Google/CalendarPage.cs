using OpenQA.Selenium;

namespace GoogleFramework
{
    public class CalendarPage : CommonFunctions
    {
        public static readonly By Create = By.XPath("//div[@class='mr0WL'][contains(text(),'Create')]");
        public static readonly By Event = By.XPath("//div[@class='jO7h3c'][contains(text(),'Event')]");
        public static readonly By AddTitleSummaryPage = By.XPath("//input[@aria-label='Add title']");
        public static readonly By ButtonSaveSummaryPage = By.XPath("//span[contains(text(),'Save')]");
        public static readonly By ButtonDeleteSummaryPage = By.XPath("//div[@aria-label='Delete event']");

        public static void Click_Create() => Click(Create);
        public static void Click_Event() => Click(Event);
        public static void Click_ButtonSaveSummaryPage() => Click(ButtonSaveSummaryPage);
        public static void Add_Title_SummaryPage(string title) => SendKey(AddTitleSummaryPage, title);
        public static void Click_ExistingEvent(string ev) => Click
            (By.XPath("//span[@class='FAxxKc'][contains(text(),'" + ev + "')]"));
        public static void Click_ButtonDeleteSummaryPage() => Click(ButtonDeleteSummaryPage);


        public static void CreateNewEvent()
        {
            Click_Create();
            Click_Event();
            Delay(2000);
        }

        public static void DeleteEvent(string ev)
        {
            Click_ExistingEvent(ev);
            Click_ButtonDeleteSummaryPage();
        }
    }
}
