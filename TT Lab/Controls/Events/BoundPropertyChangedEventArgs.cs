using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Controls.Events
{
    public class BoundPropertyChangedEventArgs : EventArgs
    {
        public string PropName { get; set; }
        public object Target { get; set; } = null;
        public object OldValue { get; internal set; }
        public object NewValue { get; internal set; }
    }
}
