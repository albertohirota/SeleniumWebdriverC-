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
        public static readonly By ButtonMoreOptionsSummaryPage = By.XPath
            ("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-dgl2Hf LjDxcd XhPA0b LQeN7 nYqxP']");
        public static readonly By AddTextCalendarBody = By.XPath("//div[@aria-label='Description']");
        public static readonly By AddGuest = By.XPath("//input[@aria-label='Guests']");
        public static readonly By ButtonSave = By.XPath("//input[@aria-label='Title']/following::span[text()='Save'][1]");
        public static readonly By ButtonSend = By.XPath("//div[@role='button']/following::span[text()='Send']");

        public static void Click_Create() => Click(Create);
        public static void Click_Event() => Click(Event);
        public static void Click_ButtonSaveSummaryPage() => Click(ButtonSaveSummaryPage);
        public static void Click_ButtonSave() => Click(ButtonSave);
        public static void Click_ExistingEvent(string ev) => Click
            (By.XPath("//span[@class='FAxxKc'][contains(text(),'" + ev + "')]"));
        public static void Click_ButtonDeleteSummaryPage() => Click(ButtonDeleteSummaryPage);
        public static void Click_ButtonMoreOptionsSummaryPage() => Click(ButtonMoreOptionsSummaryPage);
        public static void Add_TextCalendarBody(string text) => SendKey(AddTextCalendarBody, text);
        public static void Add_Title_SummaryPage(string title) => SendKey(AddTitleSummaryPage, title);
        public static void Add_Guest(string guest) => SendKeyAndEnter(AddGuest, guest);

        /// <summary>
        /// Method to create a new Calendar event
        /// </summary>
        public static void CreateNewEvent()
        {
            Click_Create();
            Delay(1000);
            Click_Event();
            Delay(2000);
        }

        /// <summary>
        /// Method to delete a existing event
        /// </summary>
        /// <param name="ev">Event name</param>
        public static void DeleteEvent(string ev)
        {
            Click_ExistingEvent(ev);
            Click_ButtonDeleteSummaryPage();
        }

        /// <summary>
        /// Click Button Send invitation if available to be clicked
        /// </summary>
        public static void Click_ButtonSend()
        {
            if(Validation.IsElementVisible(ButtonSend))
                Click(ButtonSend);
        } 

        
    }
}
