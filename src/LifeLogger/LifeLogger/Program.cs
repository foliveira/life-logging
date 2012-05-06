using System.Collections.Generic;
using LifeLogger.UI.Mediators;
using System.Linq;

namespace LifeLogger
{
    using System;
    using System.Windows.Forms;
    using UI;

    static class Program
    {
        private static readonly IList<IMediator> Mediators = new List<IMediator>();

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
            var mediators = Mediators.Where(m => m.MediatingForm.GetType().Equals(typeof(T)));

            if(mediators == null)
                throw new Exception("Mediator with MainForm not found");

            if(mediators.Count() > 1)
                throw new Exception("More than one MainForm found");

            return (T)mediators.First().MediatingForm;
}

    }
}
