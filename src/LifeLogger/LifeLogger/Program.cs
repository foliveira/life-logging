using System.Collections.Generic;
using LifeLogger.Properties;
using LifeLogger.UI.Mediators;
using System.Linq;

namespace LifeLogger
{
    using System;
    using System.Windows.Forms;
    using UI;
    using System.Runtime.Serialization;
    using System.IO;

    static class Program
    {
        private static readonly IList<IMediator> Mediators = new List<IMediator>();

        public static LoggerSettings Settings = new LoggerSettings();
        public static System.Xml.Serialization.XmlSerializer XmlSer = new System.Xml.Serialization.XmlSerializer(Settings.GetType());

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegisterMediators();
            Application.Run(GetMainFormFromMediators());

            
            TextReader textReader = new StreamReader("settings.xml");
            Settings = (LoggerSettings)XmlSer.Deserialize(textReader);
            textReader.Close();

        }

        private static void RegisterMediators()
        {
            Mediators.Add(new LoggerWindowMediator(new LoggerWindow(), true));
            Mediators.Add(new SettingsWindowMediator(new SettingsWindow(), false));
        }

        private static Form GetMainFormFromMediators()
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
