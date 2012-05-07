using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeLogger
{
    class Settings
    {
        class UserAction
        {
            private string ActionName;
            private IList<string> Shortcuts;

            public UserAction(string Name){ ActionName = Name; }

            public void AddShortcut(string Shortcut) { Shortcuts.Add(Shortcut); }
            public void RemoveShortcut(string Shortcut){ Shortcuts.Remove(Shortcut); }
        }

        private string Username;
        private string Password;
        private IList<UserAction> Actions;

        public Settings(){ Actions = new List<UserAction>(); }

        public void SetUsername(string User) { Username = User; }
        public void SetPassword(string Pass) { Password = Pass; }
        public string GetUsername() { return Username; }
        public string GetPassword() { return Password; }
        public void AddAction(string Name){ Actions.Add(new UserAction(Name)); }
    }
}
