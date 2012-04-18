using System.Globalization;

namespace LifeLogger.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Threading.Tasks;

    using Helpers;

    public partial class LoggerWindow : Form
    {
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
                                          var ctrl = new GDocs.Controller("lifelogger2012@gmail.com","fabiogvipjoao");

                                          var se = ctrl.GetSpreadsheet("LifeLogger");
                                          if (se == null) se = ctrl.CreateSpreadsheet("LifeLogger");

                                          var ws = ctrl.GetCurrentWorksheet(se);
                                          if (ws == null) ws = ctrl.CreateWorksheet(se, String.Format("{0} {1}", DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture),
                                                                                                                 DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture)));

                                          ctrl.InsertRecord(ws, DateTime.Now.Day.ToString(CultureInfo.InvariantCulture), String.Format("{0} #{1}#", logTextBox.Text, 
                                                                                                                                                    DateTime.Now.ToLongTimeString()));

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
    }
}
