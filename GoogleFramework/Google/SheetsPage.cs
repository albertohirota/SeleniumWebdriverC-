using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace GoogleFramework
{
    public class SheetsPage: CommonFunctions
    {
        static readonly string sheet = "Sheet1";

        /// <summary>
        /// Get entries in the cell
        /// </summary>
        /// <param name="entry">Cell argument, type the cell, ex: C2 or the Range, ex: B1:C3 </param>
        /// <returns>returned the value in the cells</returns>
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

        /// <summary>
        /// Send key to the spreadsheet
        /// </summary>
        /// <param name="entryCellRange">Inform which cell will be send key</param>
        /// <param name="text">string to be sent to the cell</param>
        /// <param name="shares">Should share the file?</param>
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
