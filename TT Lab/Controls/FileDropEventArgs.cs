using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Controls
{
    public class FileDropEventArgs : EventArgs
    {
        public string? File { get; set; }
        public DraggedData? Data { get; set; }
    }
}
