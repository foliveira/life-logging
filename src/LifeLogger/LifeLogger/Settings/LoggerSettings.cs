using System.ComponentModel;

namespace LifeLogger.Settings
{
    using System;
    using System.Linq;

    [Serializable]
    public class LoggerSettings
    {
        public BindingList<UserAction> Actions { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public LoggerSettings()
        {
            Actions = new BindingList<UserAction>();
        }

        public UserAction AddAction(string name)
        {
            var ua = Actions.FirstOrDefault(u => u.ActionName.Equals(name)) ?? new UserAction {ActionName = name};
            
            if(!Actions.Any(u => u.ActionName.Equals(ua.ActionName)))
                Actions.Add(ua);

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
