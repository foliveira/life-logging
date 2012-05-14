using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Program.Settings.AddAction(ActionTextBox.Text);
            Program.Settings.GetActionForShortcut(ActionTextBox.Text).Shortcuts = KeyWordsTextBox.Text;
            Program.Settings.GetActionForShortcut(ActionTextBox.Text).Hint = HintTextBox.Text;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
