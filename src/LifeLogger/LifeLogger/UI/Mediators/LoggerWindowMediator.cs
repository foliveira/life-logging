namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Windows.Forms;

    public class LoggerWindowMediator : IMediator
    {
        public LoggerWindowMediator(Boolean isMainForm = false)
            : this(null, isMainForm)
        {
        }

        public LoggerWindowMediator(Form window, Boolean isMainForm = false)
        {
            if (window == null) 
                throw new ArgumentNullException("window");

            IsMainForm = isMainForm;
            MediatingForm = window;
        }

        public bool IsMainForm
        {
            get;
            private set;
        }

        public Form MediatingForm
        {
            get;
            private set;
        }
    }
}
