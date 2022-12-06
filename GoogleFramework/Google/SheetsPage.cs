using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace GoogleFramework
{
    public class SheetsPage: CommonFunctions
    {
        static readonly string sheet = "Sheet1";
        public static ValueRange GetSheetsEntry(string entry)
        {
            LogInfo("Getting Google Sheets spreadshet");
            SheetsService service = GoogleApi.GetSheetsService();
            string SpreadsheetId = GoogleApi.GetCurrentGoogleDocID("sheets");
            var range = $"{sheet}!"+entry;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetId, range);
            ValueRange response = request.Execute();

            return response;
        }

        public static void SendKeysOnSheet(string entryCellRange, string text, bool shares = false)
        {
            LogInfo("Sending Keys to Spreadsheet");
            SheetsService service = GoogleApi.GetSheetsService(shares);
            string SpreadsheetId = GoogleApi.GetCurrentGoogleDocID("sheets");
            var range = $"{sheet}!"+entryCellRange;
            var valueRange = new ValueRange();
            var objectList = new List<Object>() { text };
            valueRange.Values = new List<IList<Object>> { objectList };
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();
            LogInfo("Send keys to Sheets, append response: "+appendResponse);
        }
    }
}
