using System;

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
