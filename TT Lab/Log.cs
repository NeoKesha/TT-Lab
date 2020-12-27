using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

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
            logBox.Dispatcher.BeginInvoke((Action)(() => logBox.AppendText(DateTime.Now + ": " + text + '\n')));
        }

        public static void Clear()
        {
            if (logBox == null) throw new ArgumentNullException("logBox was not set to write the logs in!");
            logBox.Dispatcher.BeginInvoke((Action)(() => logBox.Clear()));
        }
    }
}
