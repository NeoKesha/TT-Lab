using System;
using System.Windows.Controls;

namespace TT_Lab
{
    public static class Log
    {
        private static TextBox logBox;

        public static void SetLogBox(TextBox log)
        {
            logBox = log;
        }

        public static void WriteLine(string text)
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            logBox.Dispatcher.BeginInvoke(() => logBox.AppendText(DateTime.Now + ": " + text + '\n'));
        }

        public static void Clear()
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            logBox.Dispatcher.BeginInvoke(() => logBox.Clear());
        }
    }
}
