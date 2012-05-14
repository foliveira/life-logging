using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

            //Stream stream = File.Open("settings.bin", FileMode.Create);
            //BinaryFormatter bFormatter = new BinaryFormatter();
            //bFormatter.Serialize(stream, Program.Settings);
            //stream.Close();

            TextWriter textWriter = new StreamWriter("settings.xml");
            Program.XmlSer.Serialize(textWriter, Program.Settings);
            textWriter.Close();

            this.Hide();
            Program.GetForm<LoggerWindow>().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.GetForm<AddActionWindow>().Show();

        }
    }
}
