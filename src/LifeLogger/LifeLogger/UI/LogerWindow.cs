namespace LifeLogger.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class LoggerWindow : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        public LoggerWindow()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ctrl = new GDocs.Controller("fabio.an.oliveira@gmail.com", "zjsgphuuqnakawpt");
            var se = ctrl.GetSpreadsheet("LifeLogger");
            var ws = ctrl.GetCurrentWorksheet(se);
            ctrl.InsertRecord(ws, "16", textBox1.Text);
        }
    }
}
