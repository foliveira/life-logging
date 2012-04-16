namespace LifeLogger.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    public class WindowsShell
    {
        #region fields

        public static int ModAlt = 0x1;
        public static int ModControl = 0x2;
        public static int ModShift = 0x4;
        public static int ModWin = 0x8;
        public static int WmHotkey = 0x312;

        #endregion

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int _keyId;

        public static void RegisterHotKey(Form f, Keys key)
        {
            var modifiers = 0;

            if ((key & Keys.Alt) == Keys.Alt)
                modifiers = modifiers | ModAlt;

            if ((key & Keys.Control) == Keys.Control)
                modifiers = modifiers | ModControl;

            if ((key & Keys.Shift) == Keys.Shift)
                modifiers = modifiers | ModShift;

            var k = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;
            _keyId = f.GetHashCode(); // this should be a key unique ID, modify this if you want more than one hotkey
            RegisterHotKey(f.Handle, _keyId, modifiers, (int) k);
        }

        public static void UnregisterHotKey(Form f)
        {
            try
            {
                UnregisterHotKey(f.Handle, _keyId); // modify this if you want more than one hotkey
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
