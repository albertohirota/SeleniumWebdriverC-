using Google.Apis.Docs.v1;
using OpenQA.Selenium;
using Google.Apis.Docs.v1.Data;

namespace GoogleFramework
{
    public class DocsPage : CommonFunctions
    {
        public static readonly By ContentArea = By.XPath("//canvas[@class='kix-canvas-tile-content']");

        public static void SendText_DocumentBody(string text) => SendKeyActionBuilder(ContentArea,text);

        /// <summary>
        /// Get a list of strings of the Document Header
        /// </summary>
        /// <param name="share"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>List of document headers</returns>
        public static List<string> GetDocumentHeader(bool share = false)
        {
            LogInfo("Getting Document Header");
            var headerListReturn = new List<string>();
            Document doc = GetDocumentObject(share);
            var headerDictionary = (Dictionary<string, Header>)doc.Headers;
            foreach (KeyValuePair<string, Header> headerList in headerDictionary)
            {
                for (int i = 0; i < headerList.Value.Content.Count; i++)
                {
                    for (int j = 0; j < headerList.Value.Content[i].Paragraph.Elements.Count; j++)
                    {
                        var item = headerList.Value.Content[i].Paragraph.Elements[j].TextRun.Content;
                        headerListReturn.Add(item);
                    }
                }
            }
            return headerListReturn;
        }

        /// <summary>
        /// Get a list of the body of the document
        /// </summary>
        /// <param name="share"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>a list of document body text</returns>
        public static List<string> GetDocumentBody(bool share = false)
        {
            LogInfo("Getting Document body");
            var bodyReturn = new List<string>();
            Document doc = GetDocumentObject(share);
            IList<StructuralElement> bodyList= doc.Body.Content;
            foreach (var body in bodyList)
            {
                for (int i = 0; i < bodyList.Count; i++)
                {
                    if(body.Paragraph != null)
                    {
                        foreach(var element in body.Paragraph.Elements)
                        {
                            if(element.TextRun != null)
                                bodyReturn.Add(element.TextRun.Content);
                        }
                    }
                }
            }
            return bodyReturn;
        }

        /// <summary>
        /// Get Document Object from Google Docs API
        /// </summary>
        /// <param name="share"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>Google Document Object</returns>
        private static Document GetDocumentObject(bool share)
        {
            LogInfo("Getting Document Object");
            DocsService service = GoogleApi.GetDocsServices(share);
            string DocId = GoogleApi.GetCurrentGoogleDocID("docs");
            DocumentsResource.GetRequest request = service.Documents.Get(DocId);
            Document doc = request.Execute();
            return doc;
        }
    }
}
