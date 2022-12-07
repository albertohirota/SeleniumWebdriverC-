using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using OpenQA.Selenium;

namespace GoogleFramework
{
    public class SlidesPage: CommonFunctions
    {
        public static readonly By PresentationArea = By.XPath("//*[@id='editor-i0']");

        public static void SendText_PresentationBody(string text) => SendKeyActionBuilder(PresentationArea, text);

        /// <summary>
        /// Get Slide Object
        /// </summary>
        /// <param name="share">Enter if you need to share the Presentation, so API will be able to access the presentation</param>
        /// <returns>Return the presentation object</returns>
        public static Presentation GetSlideObject(bool share)
        {
            LogInfo("Getting Google Sheets spreadshet");
            SlidesService service = GoogleApi.GetSlidesServices(share);
            string SlideId = GoogleApi.GetCurrentGoogleDocID("slides");

            PresentationsResource.GetRequest request = service.Presentations.Get(SlideId);
            Presentation presentation = request.Execute();

            return presentation;
        }

        /// <summary>
        /// Get Slides texts
        /// </summary>
        /// <param name="share">Enter if you need to share the Presentation, so API will be able to access the presentation</param>
        /// <returns>Return a list of texts found in the presentation</returns>
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
