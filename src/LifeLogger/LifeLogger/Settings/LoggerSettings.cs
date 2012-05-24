namespace LifeLogger.Settings
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using System.Collections.Generic;

    using UI;

    [Serializable]
    public class LoggerSettings
    {
        public List<UserAction> Actions { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public LoggerSettings()
        {
            Actions = new List<UserAction>();
        }

        public UserAction AddAction(string name)
        {
            var ua = new UserAction {ActionName = name};
            var settingsForm = Program.GetForm<SettingsWindow>();
            if(settingsForm.InvokeRequired)
            {
                settingsForm.BeginInvoke(new MethodInvoker(() => Actions.Add(ua)));
            }
            else
            {
                Actions.Add(ua);
            }

            ua.Shortcuts = ua.Shortcuts + "," + name;
            return ua;
        }

        public bool HasAction(string name)
        {
            return Actions.Any(a => a.ActionName.Equals(name));
        }

        public UserAction GetActionForShortcut(string shortcut)
        {
            return Actions.FirstOrDefault(ua => ua.Shortcuts.Split(',').Any(s => s.Trim().Equals(shortcut)));
        }
    }
}
