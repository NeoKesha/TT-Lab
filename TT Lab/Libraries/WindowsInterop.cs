using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Libraries
{
    public static class WindowsInterop
    {
  
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
  
        [DllImport("user32.dll")]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
  
        [DllImport("user32.dll")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndParent);
  
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
  
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
  
        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
  
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
  
        public const int SW_SHOWNA = 8;
  
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_POPUP = 0x80000000;
  
        public const uint WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WS_EX_NOACTIVATE = 0x08000000;
        public const uint WS_EX_LAYERED = 0x00080000; // supported for child windows too, from Windows 8 further.
        public const uint WS_EX_TRANSPARENT = 0x00000020;
  
        public const uint LWA_ALPHA = 0x00000002;
        public const uint LWA_COLORKEY = 0x00000001;
    }
}
