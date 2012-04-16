using System.Linq;
using Google.GData.Spreadsheets;
using System;
using System.Globalization;

namespace LifeLogger.GDocs
{
    public class Controller
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");
        private readonly SpreadsheetsService _service = new SpreadsheetsService("MySpreadsheetIntegration-v1");

        public Controller(string username, string password)
        {
            _service.setUserCredentials(username, password);
        }
        
        public SpreadsheetEntry GetSpreadsheet(string name)
        {
            var query = new SpreadsheetQuery();
            var feed = _service.Query(query);
            SpreadsheetEntry spreadsheet = null;

            foreach (var entry in feed.Entries.Cast<SpreadsheetEntry>().Where(entry => entry.Title.Text.Equals(name)))
                return entry;

            //TODO: Parece que nao é assim que se cria uma nova Spreadsheet!!!
            Console.WriteLine("Spreadsheet not fround. Creating...");
            spreadsheet = new SpreadsheetEntry {Title = {Text = name}};
            feed.Insert(spreadsheet);
            return spreadsheet;
        }

        
        public WorksheetEntry GetCurrentWorksheet(SpreadsheetEntry spreadsheet)
        {
            string currentDate = String.Format("{0} {1}", DateTime.Now.ToString("MMMM", _culture),
                                               DateTime.Now.ToString("yyyy", _culture));
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = null;

            foreach (var entry in wsFeed.Entries.Cast<WorksheetEntry>().Where(entry => entry.Title.Text.Equals(currentDate)))
                return entry;

            Console.WriteLine("Worksheet not found. Creating!!!");
            var numberOfDays = (uint)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            worksheet = new WorksheetEntry(2, numberOfDays) {Title = {Text = currentDate}};
            wsFeed.Insert(worksheet);
            //TODO Aqui tem que se intrdozir uma row com 1,2,3,4,5,...,31
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

            //Caso nao exista uma linha livre!!
            if (currentRow >= worksheet.Rows)
            {
                worksheet.Rows += 1;
                worksheet.Update();
            }

            Console.WriteLine("Inserting on Row:{0}, Column:{1}", currentRow + 1, currentColumn);
            var cell = new CellEntry(currentRow + 1, currentColumn, text);
            cellFeed.Insert(cell);
        }
    }
}
