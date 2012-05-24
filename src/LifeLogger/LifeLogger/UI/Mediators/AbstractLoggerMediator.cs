namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Windows.Forms;

    public abstract class AbstractLoggerMediator<T> 
        : IMediator<T> where T : Form, new()
    {
        protected AbstractLoggerMediator(Boolean isMainForm = false)
            : this(new T(), isMainForm)
        {
        }

        protected AbstractLoggerMediator(T window, Boolean isMainForm = false)
        {
            if (window == null) 
                throw new ArgumentNullException("window");

            IsMainForm = isMainForm;
            MediatingForm = window;
        }

        public bool IsMainForm { get; private set; }
        public T MediatingForm { get; private set; }

        public abstract void RegisterEvents();
    }
}