using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Docs.v1;
using Google.Apis.Services;
using System.Text.RegularExpressions;
using Login;

namespace GoogleFramework
{
    public class GoogleApi : CommonFunctions
    {
        static readonly string ApplicationName = "GoogleAutomation"; 

        public static IList<Property>? RetrieveProperties(DriveService service, String fileId)
        {
            LogInfo("Retriving document properties");
            try
            {
                PropertyList properties = service.Properties.List(fileId).Execute();
                return properties.Items;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            return null;
        }

        /// <summary>
        /// Function to get the current Google Doc/Sheet/Slide ID that is current opened in the browser
        /// </summary>
        /// <param name="app">Need the app name</param>
        /// <returns>Return Google ID for the document opened in the browser</returns>
        public static string GetCurrentGoogleDocID(string app)
        {
            string regex = "";
            LogInfo("Getting Google Document Id for app: "+ app);
            switch (app.ToLower())
            {
                case "docs":
                    regex = @"/document/d/([a-zA-Z0-9-_]+)";
                    break;

                case "sheets":
                    regex = @"/spreadsheets/d/([a-zA-Z0-9-_]+)";
                    break;
                case "slides":
                    regex = @"/presentation/d/([a-zA-Z0-9-_]+)";
                    break;
            }
            Regex RxSpreadSheetId = new(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string URL = Driver.Instance!.Url;
            Match MatchSpreadSheetId = RxSpreadSheetId.Match(URL);
            string id = MatchSpreadSheetId.Groups[1].Value;
            LogInfo("Ïd found is: " + id);
            return id;
        }

        /// <summary>
        /// Get GoogleCredential to access Google Files. Remember, file need to be shared with Google API, 
        /// if you want to modify the file using Google APIs
        /// </summary>
        /// <param name="runShareFile">Bool Argument if the user needs to Share the file</param>
        /// <returns>Return Google Credential</returns>
        public static GoogleCredential GetCredential(bool runShareFile)
        {
            LogInfo("Getting Google Credential");
            if (runShareFile)
                GOfficePage.SharePublic("automation@southern-bonsai-370413.iam.gserviceaccount.com");

            return _ = GoogleLogin.GetCredential();
        }

        /// <summary>
        /// Method to get Drive Service API
        /// </summary>
        /// <param name="runShareFile"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>Google Drive Service</returns>
        public static DriveService GetDriveService(bool runShareFile)
        {
            LogInfo("Getting Google Drive Service");
            DriveService service;
            GoogleCredential credential = GetCredential(runShareFile);

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        /// <summary>
        /// Get Google Sheets Services
        /// </summary>
        /// <param name="runShareFile"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>Google Sheets Service</returns>
        public static SheetsService GetSheetsService(bool runShareFile = false)
        {
            LogInfo("Getting Google Sheets Service");
            SheetsService service;
            GoogleCredential credential = GetCredential(runShareFile);

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        /// <summary>
        /// Gets Google Docs Service
        /// </summary>
        /// <param name="runShareFile"> Does the user needs to share the file, so can be accessed by Google APIs</param>
        /// <returns>Google Docs Service</returns>
        public static DocsService GetDocsServices(bool runShareFile)
        {
            LogInfo("Getting Google Docs Service");
            DocsService service;
            GoogleCredential credential = GetCredential(runShareFile);

            service = new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }
    }
}


