namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Windows.Forms;

    public class AddActionWindowMediator : IMediator
    {
        public AddActionWindowMediator(Boolean isMainForm = false)
            : this(null, isMainForm)
        {
        }

        public AddActionWindowMediator(Form window, Boolean isMainForm = false)
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
