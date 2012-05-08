using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LifeLogger
{
    public class LoggerSettings
    {
        public LoggerSettings()
        {
            Actions = new BindingList<UserAction>();
        }

        public BindingList<UserAction> Actions { get; set; }

        public String Username { get; set; }
        public String Password { get; set; }

        public UserAction AddAction(string name)
        {
            var ua = new UserAction {ActionName = name};
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

        public class UserAction
        {
            public String ActionName { get; set; }
            public String Shortcuts { get; set; }
        }
    }
}
