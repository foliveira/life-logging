using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeLogger.UI.Mediators
{
    using System;
    using System.Windows.Forms;

    public class SettingsWindowMediator : IMediator
    {
        public SettingsWindowMediator(Boolean isMainForm = false)
            : this(null, isMainForm)
        {
        }

        public SettingsWindowMediator(Form window, Boolean isMainForm = false)
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
