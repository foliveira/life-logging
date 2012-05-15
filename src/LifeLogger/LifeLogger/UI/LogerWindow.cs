using System.Globalization;
using LifeLogger.Parser;

namespace LifeLogger.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Threading.Tasks;

    using Helpers;

    public partial class LoggerWindow : Form
    {
        private readonly ParserEngine _parserEngine = new ParserEngine();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse
         );

        public LoggerWindow()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            WindowsShell.RegisterHotKey(this, Keys.Control | Keys.Alt | Keys.L);
            
        }

        private void Button1Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            Task.Factory.StartNew(() =>
                                      {
                                          var ctrl = new GDocs.Controller(Program.Settings.Username, Program.Settings.Password);

                                          var se = ctrl.GetSpreadsheet("LifeLogger") ??
                                                   ctrl.CreateSpreadsheet("LifeLogger");

                                          var ws = ctrl.GetCurrentWorksheet(se) ??
                                                   ctrl.CreateWorksheet(se, String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                                                                                                          DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture)));

                                          var entry = logTextBox.Text;

                                          if (!String.IsNullOrEmpty(entry) && !String.IsNullOrWhiteSpace(entry))
                                          {
                                              var context = TreatParserOutput(entry);

                                              ctrl.InsertRecord(ws,
                                                                context.ContextDate.Day.ToString(
                                                                    CultureInfo.InvariantCulture), context.ToString());
                                          }

                                          /* 
                                           * Since the GUI objects can only be changed in their creator thread,
                                           * call BeginInvoke on the form to mess with them!
                                           */
                                          this.BeginInvoke(new MethodInvoker(() =>
                                                                                 {
                                                                                     this.logTextBox.Text = String.Empty;
                                                                                     this.progressBar1.Visible = false;
                                                                                     this.Visible = false;
                                                                                 }));
                                      });
        }

        private ParserContext TreatParserOutput(string entry)
        {
            var context = _parserEngine.ParseUserInput(entry);

            if (context.Equals(ParserContext.Empty))
            {
                var actionString = entry.Split(' ')[0];

                var result =
                    MessageBox.Show(String.Format("Action \"{0}\" is not defined. Do you wish do add it?", actionString),
                                    "Atention",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var actionToAdd = String.Format("{0}{1}",
                                                    actionString.Substring(0, 1).ToUpper(), 
                                                    actionString.Substring(1));
                    var aaw = Program.GetForm<AddActionWindow>();
                    aaw.ActionTextBox.Text = actionToAdd;

                    EventHandler[] eh = {null};
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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WindowsShell.WmHotkey)
                this.Visible = !this.Visible;
        }

        private void LoggerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsShell.UnregisterHotKey(this);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            Program.GetForm<SettingsWindow>().Show();
        }
    }
}
