using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V106.Debugger;

namespace GoogleFramework
{
    public class SlidesPage: CommonFunctions
    {
        public static readonly By PresentationArea = By.XPath("//*[@id='editor-i0']");

        public static void SendText_PresentationBody(string text) => SendKeyActionBuilder(PresentationArea, text);

        public static Presentation GetSlideObject(bool share)
        {
            LogInfo("Getting Google Sheets spreadshet");
            SlidesService service = GoogleApi.GetSlidesServices(share);
            string SlideId = GoogleApi.GetCurrentGoogleDocID("slides");

            PresentationsResource.GetRequest request = service.Presentations.Get(SlideId);
            Presentation presentation = request.Execute();

            return presentation;
        }

        public static List<string> GetSlidesTexts(bool share = false)
        {
            LogInfo("Getting Presentation Texts");
            List<string> presentationTexts = new();
            foreach(var slidesObject in GetSlideObject(share).Slides)
            {
                foreach(var element in slidesObject.PageElements)
                {
                    if (element.Shape.Text == null)
                        break;
                    foreach (var textElement in element.Shape.Text.TextElements)
                    {
                        if (textElement.TextRun != null)
                            presentationTexts.Add(textElement.TextRun.Content);
                    }
                }
            }
            return presentationTexts;
        }
    }
}
