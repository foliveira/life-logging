namespace LifeLogger.Settings
{
    using System;

    [Serializable]
    public class UserAction
    {
        public String ActionName { get; set; }
        public String Shortcuts { get; set; }
        public String Hint { get; set; }
    }
}
