namespace LifeLogger.GDocs
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Globalization;

    using Google.GData.Spreadsheets;
    using System.Collections.Generic;
    using Google.GData.Client;

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

            foreach (SpreadsheetEntry entry in feed.Entries.Cast<SpreadsheetEntry>().Where(entry => entry.Title.Text.Equals(name)))
                return entry;

            Debug.WriteLine("Spreadsheet not found.");
            return null;
        }

        public SpreadsheetEntry CreateSpreadsheet(string name)
        {
            //TODO
            return new SpreadsheetEntry();
        }


        public WorksheetEntry GetCurrentWorksheet(SpreadsheetEntry spreadsheet)
        {
            var currentDate = String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                               DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture));

            var wsFeed = spreadsheet.Worksheets;

            foreach (WorksheetEntry entry in wsFeed.Entries.Cast<WorksheetEntry>().Where(entry => entry.Title.Text.Equals(currentDate)))
                return entry;

            Debug.WriteLine("Worksheet not found.");
            return null;
        }

        public WorksheetEntry CreateWorksheet(SpreadsheetEntry spreadsheet, string name)
        {
            var numberOfDays = (uint)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            var wsFeed = spreadsheet.Worksheets;

            var worksheet = new WorksheetEntry(2, numberOfDays) { Title = { Text = name } };
            worksheet = _service.Insert(wsFeed, worksheet);


            //Insert First Line
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

        public Dictionary<string, List<string>> GetRowsFromWorksheet(WorksheetEntry worksheet)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            ListFeed listFeed = _service.Query(listQuery);

            foreach (ListEntry row in listFeed.Entries)
            {
                foreach (ListEntry.Custom element in row.Elements)
                {
                    if (!String.IsNullOrEmpty(element.Value))
                    {
                        int actionStart = element.Value.IndexOf("(");
                        int actionEnd = element.Value.IndexOf(" ", actionStart);
                        int secondEnd = element.Value.IndexOf(")", actionEnd);
                        string action = element.Value.Substring(actionStart + 1, actionEnd - actionStart - 1);
                        string cena = element.Value.Substring(actionEnd + 1, secondEnd - actionEnd - 1);

                        if (dictionary.ContainsKey(action))
                        {
                            dictionary[action].Add(cena);
                        }
                        else
                        {
                            List<string> list = new List<string>();
                            list.Add(cena);
                            dictionary.Add(action, list);
                        }
                    }
                }
            }
            return dictionary;
        }

    }
}
