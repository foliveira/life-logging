namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Windows.Forms;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Drawing;

    using Properties;
    using Parser;
    using Helpers;

    internal class LoggerMediator : AbstractLoggerMediator<LoggerWindow>
    {
        private readonly ParserEngine _parserEngine = new ParserEngine();

        public LoggerMediator()
            : base(new LoggerWindow(), true)
        { }

        public override void RegisterEvents()
        {
            MediatingForm.SendButton.Click += OnSubmitButtonEvent;
            MediatingForm.SettingsButton.Click += OnSettingsButtonEvent;

            MediatingForm.LogTextBox.GotFocus += OnLogTextBoxFocusEvent;
            MediatingForm.LogTextBox.LostFocus += OnLogTextBoxLostFocusEvent;

            MediatingForm.FormClosing += OnFormClosingEvent;
            MediatingForm.Load += OnFormLoadEvent;
        }

        private void OnLogTextBoxLostFocusEvent(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(MediatingForm.LogTextBox.Text))
            {
                SetLogTextBoxContextDate();
            }
        }

        private void OnLogTextBoxFocusEvent(object sender, EventArgs e)
        {
            MediatingForm.LogTextBox.ResetText();
            MediatingForm.LogTextBox.ResetForeColor();
        }

        private void OnFormLoadEvent(object sender, EventArgs e)
        {
            SetLogTextBoxContextDate();
        }

        private void OnSettingsButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.Hide();

            Program.GetForm<SettingsWindow>().Show();
        }

        private void OnFormClosingEvent(object sender, FormClosingEventArgs e)
        {
            WindowsShell.UnregisterHotKey(MediatingForm);
        }

        private void OnSubmitButtonEvent(object sender, EventArgs e)
        {
            MediatingForm.ProgressBar.Visible = true;

            Task.Factory.StartNew(() =>
            {
                var se = Program.Controller.GetSpreadsheet("LifeLogger") ??
                         Program.Controller.CreateSpreadsheet("LifeLogger");

                var ws = Program.Controller.GetCurrentWorksheet(se) ??
                         Program.Controller.CreateWorksheet(se, String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                                                                                DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture)));

                var entry = MediatingForm.LogTextBox.Text;

                if (!String.IsNullOrEmpty(entry) && !String.IsNullOrWhiteSpace(entry))
                {
                    var context = TreatParserOutput(entry);

                    if(context.ContextAction != null)
                    {
                        Program.Controller.InsertRecord(ws,
                                          context.ContextDate.Day.ToString(CultureInfo.InvariantCulture),
                                          context.ToString());
                    }
                }

                /* 
                 * Since the GUI objects can only be changed in their creator thread,
                 * call BeginInvoke on the form to mess with them!
                 */
                MediatingForm.BeginInvoke(new MethodInvoker(() =>
                {
                    SetLogTextBoxContextDate();
                    MediatingForm.ProgressBar.Visible = false;
                    MediatingForm.Visible = false;
                }));
            });
        }

        private void SetLogTextBoxContextDate()
        {
            var date = _parserEngine.CurrentParserContext.ContextDate;
            string dateContext;

            if (date.Equals(DateTime.Today))
            {
                dateContext = "@today";
            }
            else if (date.Equals(DateTime.Today.AddDays(-1)))
            {
                dateContext = "@yesterday";
            }
            else
            {
                dateContext = String.Format("@{0:dd/MM/yyyy}", date);
            }

            MediatingForm.LogTextBox.Text = dateContext;
            MediatingForm.LogTextBox.ForeColor = Color.DarkGray;
        }

        private ParserContext TreatParserOutput(string entry)
        {
            var context = _parserEngine.ParseUserInput(entry);

            if (context.Equals(ParserContext.Empty))
            {
                var actionString = entry.Split(' ')[0];

                var result =
                    MessageBox.Show(String.Format("Action \"{0}\" is not defined. Do you wish do add it?", actionString),
                                    Resources.LoggerMediator_TreatParserOutput_Atention,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var actionToAdd = String.Format("{0}{1}",
                                                    actionString.Substring(0, 1).ToUpper(),
                                                    actionString.Substring(1));

                    var aaw = Program.GetForm<AddActionWindow>();
                    {
                        aaw.ActionTextBox.Text = actionToAdd;
                        aaw.HintTextBox.Text = String.Empty;
                        aaw.KeyWordsTextBox.Text = String.Empty;
                    }

                    EventHandler[] eh = { null };
                    eh[0] = (s, args) =>
                    {
                        if (aaw.Visible)
                            return;

                        aaw.VisibleChanged -= eh[0];
                        context = TreatParserOutput(entry);
                    };
                    aaw.VisibleChanged += eh[0];
                    aaw.ShowDialog();
                }
            }

            return context;
        }
    }
}
