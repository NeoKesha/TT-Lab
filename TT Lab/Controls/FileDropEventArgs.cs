using System;

namespace TT_Lab.Controls
{
    public class FileDropEventArgs : EventArgs
    {
        public string? File { get; set; }
        public DraggedData? Data { get; set; }
    }
}
