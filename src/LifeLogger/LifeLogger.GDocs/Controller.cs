namespace LifeLogger.GDocs
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Globalization;

    using Google.GData.Spreadsheets;

    public class Controller
    {
        private readonly SpreadsheetsService _service = new SpreadsheetsService("MySpreadsheetIntegration-v1");

        public Controller(string username, string password)
        {
            _service.setUserCredentials(username, password);
        }


        public SpreadsheetEntry GetSpreadsheet(string name)
        {
            var query = new SpreadsheetQuery();
            var feed = _service.Query(query);
            var spreadsheet = default(SpreadsheetEntry);

            foreach (SpreadsheetEntry entry in feed.Entries.Cast<SpreadsheetEntry>().Where(entry => entry.Title.Text.Equals(name)))
                return entry;

            //TODO: Parece que nao é assim que se cria uma nova Spreadsheet!!!
            Debug.WriteLine("Spreadsheet not found. Creating...");

            spreadsheet = new SpreadsheetEntry {Title = {Text = name}};

            return feed.Insert(spreadsheet);
        }


        public WorksheetEntry GetCurrentWorksheet(SpreadsheetEntry spreadsheet)
        {
            var currentDate = String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                               DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture));
            var wsFeed = spreadsheet.Worksheets;
            var worksheet = default(WorksheetEntry);

            foreach (WorksheetEntry entry in wsFeed.Entries.Cast<WorksheetEntry>().Where(entry => entry.Title.Text.Equals(currentDate)))
                return entry;

            Debug.WriteLine("Worksheet not found. Creating!!!");

            var numberOfDays = (uint)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            worksheet = new WorksheetEntry(2, numberOfDays) {Title = {Text = currentDate}};
            worksheet = _service.Insert(wsFeed, worksheet);

            var cellQuery = new CellQuery(worksheet.CellFeedLink);
            var cellFeed = _service.Query(cellQuery);

            for (var i = 1; i <= numberOfDays; i++)
            {
                cellFeed.Insert(new CellEntry(1, (uint)i, i.ToString(CultureInfo.InvariantCulture)));
            }

            return worksheet;
        }


        public void InsertRecord(WorksheetEntry worksheet, string day, string text)
        {
            var cellQuery = new CellQuery(worksheet.CellFeedLink);
            var cellFeed = _service.Query(cellQuery);

            uint currentColumn = 0; 
            uint currentRow = 0;

            foreach (CellEntry c in cellFeed.Entries)
            {
                if (currentRow == 0 && c.InputValue == day)
                {
                    currentColumn = c.Column;
                    currentRow = c.Row;
                }
                else if (c.Column == currentColumn)
                    currentRow = c.Row;
            }

            if (currentRow >= worksheet.Rows)
            {
                worksheet.Rows += 1;
                worksheet.Update();
            }

            Debug.WriteLine("Inserting on Row:{0}, Column:{1}", currentRow + 1, currentColumn);

            var cell = new CellEntry(currentRow + 1, currentColumn, text);
            cellFeed.Insert(cell);
        }
    }
}
