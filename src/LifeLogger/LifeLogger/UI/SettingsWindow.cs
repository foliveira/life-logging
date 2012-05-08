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
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            Program.Settings.Username = EmaiTextBox.Text;
            Program.Settings.Password = PasswordTextBox.Text;

            this.Hide();

            Program.GetForm<LoggerWindow>().Show();
        }
    }
}
