using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using GoogleFramework;
using OpenQA.Selenium;

namespace GoogleFramework
{
    public class GoogleApi : CommonFunctions
    {
        static readonly string[] Scopes = { DriveService.Scope.DriveReadonly };
        static readonly string ApplicationName = "GoogleAutomation"; 
        // readonly string[] ScopesSheets = { SheetsService.Scope.Spreadsheets };
        //static readonly string[] ScopesDocs = { DocsService.Scope.Documents };

        private static string GetGoogleJson() => AppDomain.CurrentDomain.BaseDirectory + "\\Google\\Google.json";

        public static IList<Property>? RetrieveProperties(DriveService service, String fileId)
        {
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

        public static string GetCurrentGoogleDocID(string app)
        {
            string regex = "";

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
            return MatchSpreadSheetId.Groups[1].Value;
        }

        public static GoogleCredential GetCredential(bool runShareFile)
        {
            GoogleCredential credential;
            
            if (runShareFile)
                GOfficePage.SharePublic("automation@southern-bonsai-370413.iam.gserviceaccount.com");

            using (var stream = new FileStream(GetGoogleJson(), FileMode.Open, FileAccess.ReadWrite))
            {
                return credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
        }

        public static DriveService LoadDriveApi(bool runShareFile)
        {
            DriveService service;
            GoogleCredential credential = GetCredential(runShareFile);

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        public static SheetsService LoadSheetsApi(bool runShareFile)
        {
            SheetsService service;
            GoogleCredential credential = GetCredential(runShareFile);

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        public static DocsService LoadDocsApi(bool runShareFile)
        {
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


