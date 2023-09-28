using System;
using System.Windows.Controls;

namespace TT_Lab
{
    public static class Log
    {
        private static TextBox? logBox;

        public enum LogType
        {
            Info,
            Warning,
            Error,
            Debug,
            Trace
        }

        public static void SetLogBox(TextBox log)
        {
            logBox = log;
        }

        public static async void WriteLine(string text)
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            await logBox.Dispatcher.BeginInvoke(() => logBox.AppendText(DateTime.Now + ": " + text + '\n'));
        }

        public static void Clear()
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            logBox.Dispatcher.BeginInvoke(() => logBox.Clear());
        }
    }
}
