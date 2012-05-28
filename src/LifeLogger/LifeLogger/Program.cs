namespace LifeLogger
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Linq;
    using System.Windows.Forms;
    using System.Collections.Generic;

    using Settings;
    using UI.Mediators;
    using LifeLogger.UI;

    static class Program
    {
        private static readonly IList<IMediator<Form>> Mediators = new List<IMediator<Form>>();

        public static LoggerSettings Settings = new LoggerSettings();
        public static System.Xml.Serialization.XmlSerializer XmlSer = new System.Xml.Serialization.XmlSerializer(typeof(LoggerSettings));
        public static GDocs.Controller Controller;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RegisterMediators();
            RegisterEvents();
            LoadSettings();
            Controller = new GDocs.Controller(Settings.Username, Settings.Password);

            Application.Run(GetMainForm());
        }

        private static void LoadSettings()
        {
            if (File.Exists("settings.xml"))
            {
                using (XmlReader settingsReader = new XmlTextReader("settings.xml"))
                {
                    if (XmlSer.CanDeserialize(settingsReader))
                    {
                        Settings = XmlSer.Deserialize(settingsReader) as LoggerSettings;
                        Program.GetForm<SettingsWindow>().EmaiTextBox.Text = Settings.Username;
                        Program.GetForm<SettingsWindow>().PasswordTextBox.Text = Settings.Password;
                    }
                }
            }
            else
            {
                using (XmlWriter settingsWriter = new XmlTextWriter("settings.xml", Encoding.UTF8))
                {
                    XmlSer.Serialize(settingsWriter, Settings);
                }
            }
        }

        private static void RegisterEvents()
        {
            foreach (var mediator in Mediators)
            {
                mediator.RegisterEvents();
            }
        }

        private static void RegisterMediators()
        {
            Mediators.Add(new LoggerMediator());
            Mediators.Add(new SettingsMediator());
            Mediators.Add(new AddActionMediator());
            Mediators.Add(new UpdateActionMediator());
            Mediators.Add(new ChartMediator());
        }

        private static Form GetMainForm()
        {
            var mediators = Mediators.Where(m => m.IsMainForm).ToArray();

            if(mediators == null)
                throw new Exception("Mediator with MainForm not found");

            if(mediators.Length > 1)
                throw new Exception("More than one MainForm found");

            return mediators.First().MediatingForm;
        }

        public static T GetForm<T>() where T : Form
        {
            var mediators = Mediators.Where(m => m.MediatingForm.GetType() == typeof(T));

            if(mediators == null)
                throw new Exception("Mediator with MainForm not found");

            var form = mediators.FirstOrDefault();
            
            return (form != null) ? (T)form.MediatingForm : null;
        }

    }
}
