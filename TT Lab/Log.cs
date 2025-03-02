using System;
using System.Linq;
using System.Windows.Controls;

namespace TT_Lab
{
    public static class Log
    {
        private static TextBox? logBox;
        private static readonly String[] _separator = new[] { "\r\n", "\n" };

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
            await logBox.Dispatcher.BeginInvoke(() =>
            {
                logBox.AppendText(DateTime.Now + ": " + text + '\n');
                if (logBox.LineCount >= logBox.MaxLines)
                {
                    var lines = logBox.Text.Split(_separator, StringSplitOptions.None);
                    logBox.Text = string.Join(Environment.NewLine, lines.Skip(lines.Length - logBox.MaxLines));
                    logBox.CaretIndex = logBox.Text.Length;
                }
            });
        }

        public static void Clear()
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            logBox.Dispatcher.BeginInvoke(() => logBox.Clear());
        }
    }
}
