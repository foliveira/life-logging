using System;
using System.Windows.Forms;

namespace LifeLogger.UI
{
    public partial class AddActionWindow : Form
    {
        public AddActionWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ua = Program.Settings.AddAction(ActionTextBox.Text);

            ua.Shortcuts = KeyWordsTextBox.Text;
            ua.Hint = HintTextBox.Text;

            ActionTextBox.Text = String.Empty;
            KeyWordsTextBox.Text = String.Empty;
            HintTextBox.Text = String.Empty;

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
