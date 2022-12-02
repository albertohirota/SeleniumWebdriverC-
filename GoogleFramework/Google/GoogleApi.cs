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
        static readonly string ApplicationName = "QA-Automation"; //for Domain: brin.tituscloud.com
        static readonly string[] ScopesSheets = { SheetsService.Scope.Spreadsheets };
        static readonly string[] ScopesDocs = { DocsService.Scope.Documents };

        public static IList<Property> RetrieveProperties(DriveService service, String fileId)
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

        public static DriveService LoadDriveApi(bool runShareFile)
        {
            DriveService service;
            GoogleCredential credential;
            if (runShareFile)
                G_OfficeCommon.SharePublic("automation@qa-automation-326813.iam.gserviceaccount.com"); //for Domain: brin.tituscloud.com

            string json = $@"{GetCurrentWorkingDirectory()}\G-Office\qa-automation.json"; //for Domain: brin.tituscloud.com
            using (var stream = new FileStream(json, FileMode.Open, FileAccess.ReadWrite))
            {
                credential = GoogleCredential.FromStream(stream)
                  .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        protected static string GetCurrentWorkingDirectory()
        {
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ?? throw new InvalidOperationException()).LocalPath;
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
            Regex RxSpreadSheetId = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string URL = Driver.Instance!.Url;
            Match MatchSpreadSheetId = RxSpreadSheetId.Match(URL);
            return MatchSpreadSheetId.Groups[1].Value;
        }

        public static SheetsService LoadSheetsApi(bool shares)
        {
            SheetsService service;
            GoogleCredential credential;

            if (shares)
                G_OfficeCommon.SharePublic("automation@qa-automation-326813.iam.gserviceaccount.com"); //for Domain: brin.tituscloud.com

            string json = $@"{GetCurrentWorkingDirectory()}\G-Office\qa-automation.json"; //for Domain: brin.tituscloud.com

            using (var stream = new FileStream(json, FileMode.Open, FileAccess.ReadWrite))
            {
                credential = GoogleCredential.FromStream(stream)
                  .CreateScoped(ScopesSheets);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        public static DocsService LoadDocsApi(bool shares)
        {
            DocsService service;
            GoogleCredential credential;

            if (shares)
                G_OfficeCommon.SharePublic("automation@qa-automation-326813.iam.gserviceaccount.com"); //for Domain: brin.tituscloud.com

            //string json = $@"{GetCurrentWorkingDirectory()}\G-Office\brin-qa.json"; //for Domain: test.tituscloud.com
            string json = $@"{GetCurrentWorkingDirectory()}\G-Office\qa-automation.json"; //for Domain: brin.tituscloud.com

            using (var stream = new FileStream(json, FileMode.Open, FileAccess.ReadWrite))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(ScopesDocs);
            }

            // Create Google Docs API service.
            service = new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }
    }
}


