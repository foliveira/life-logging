using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace LifeLogger
{
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
            var ua = new UserAction {ActionName = name};
            Actions.Add(ua);
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

    public class UserAction
    {
        public String ActionName { get; set; }
        public String Shortcuts { get; set; }
        public String Hint { get; set; }
    }
}
