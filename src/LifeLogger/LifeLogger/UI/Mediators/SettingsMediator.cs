namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    using GDocs;

    internal class SettingsMediator : AbstractLoggerMediator<SettingsWindow>
    {
        public override void RegisterEvents()
        {
            MediatingForm.SaveButton.Click += OnSaveButtonEvent;
            MediatingForm.CancelButton.Click += OnCancelButtonEvent;
            MediatingForm.AddActionButton.Click += OnAddActionButtonEvent;
            MediatingForm.ChartButton.Click += OnChartButtonEvent;
            MediatingForm.RemoveActionButton.Click += OnRemoveActionButton;

            MediatingForm.ActionsList.ItemActivate += OnItemActivateEvent;

            MediatingForm.Shown += OnFormShownEvent;
            MediatingForm.Load += OnFormShownEvent;
            MediatingForm.Activated += OnFormShownEvent;
        }

        private void OnChartButtonEvent(object sender, EventArgs e)
        {
            var chartWnd = Program.GetForm<ChartWindow>();
            {
                var se = Program.Controller.GetSpreadsheet("LifeLogger") ??
                         Program.Controller.CreateSpreadsheet("LifeLogger");

                var ws = Program.Controller.GetCurrentWorksheet(se) ??
                         Program.Controller.CreateWorksheet(se, String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                                                                                DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture)));
                
                var rows = Program.Controller.GetRowsFromWorksheet(ws);
                chartWnd.ActionsChart.Series.Clear();

                foreach (var row in rows)
                {
                    var series = chartWnd.ActionsChart.Series.Add(row.Key);
                    series.Points.Add(row.Value.Count);
                }

            }
            chartWnd.Show();
        }

        private void OnItemActivateEvent(object sender, EventArgs e)
        {
            var ua = Program.Settings.Actions.Join(MediatingForm.ActionsList.SelectedItems.OfType<ListViewItem>(),
                                                   u => u.ActionName,
                                                   lvi => lvi.Text,
                                                   (u, lvi) => u)
                                            .First();

            var updActionWnd = Program.GetForm<UpdateActionWindow>();
            {
                updActionWnd.ActionTextBox.Text = ua.ActionName;
                updActionWnd.HintTextBox.Text = ua.Hint;
                updActionWnd.KeyWordsTextBox.Text = ua.Shortcuts;   
            }
            updActionWnd.Show();
        }

        private void OnRemoveActionButton(object sender, EventArgs e)
        {
            var selection = MediatingForm.ActionsList.SelectedItems.OfType<ListViewItem>();
            var selected = selection.FirstOrDefault();
            
            if (selected == default(ListViewItem)) return;

            MediatingForm.ActionsList.Items.Remove(selected);
            var selectedUa = Program.Settings.Actions.First(ua => ua.ActionName.Equals(selected.Text));
            Program.Settings.Actions.Remove(selectedUa);
        }

        private void OnFormShownEvent(object sender, EventArgs e)
        {
            MediatingForm.ActionsList.Items.Clear();
            
            foreach (var userAction in Program.Settings.Actions)
            {
                MediatingForm.ActionsList.Items.Add(userAction.ActionName);
            }
        }

        private void OnAddActionButtonEvent(object sender, EventArgs e)
        {
            var addActionWnd = Program.GetForm<AddActionWindow>();
            {
                addActionWnd.ActionTextBox.Text = String.Empty;
                addActionWnd.HintTextBox.Text = String.Empty;
                addActionWnd.KeyWordsTextBox.Text = String.Empty;
            }
            addActionWnd.Show();
        }

        private void OnCancelButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }

        private void OnSaveButtonEvent(object sender, EventArgs e)
        {
            Program.Settings.Username = MediatingForm.EmaiTextBox.Text;
            Program.Settings.Password = MediatingForm.PasswordTextBox.Text;

            using (XmlWriter settingsWriter = new XmlTextWriter("settings.xml", Encoding.UTF8))
            {
                Program.XmlSer.Serialize(settingsWriter, Program.Settings);
            }
            Program.Controller = new Controller(Program.Settings.Username, Program.Settings.Password);

            MediatingForm.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }
    }
}
