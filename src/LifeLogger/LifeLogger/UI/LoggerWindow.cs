namespace LifeLogger.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WindowsShell.WmHotkey)
                Visible = !Visible;
        }
    }
}
