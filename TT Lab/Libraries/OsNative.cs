﻿using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace TT_Lab.Libraries
{
    public static class OsNative
    {
        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public int X;
            public int Y;
        };
        
        [DllImport("user32.dll")]
        private static extern void ClipCursor(ref Win32Rect lpRect);

        [DllImport("user32.dll")]
        private static extern void ClipCursor(IntPtr lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Rect
        {
            public int Left, Top, Right, Bottom;
        }

        private static Boolean _cursorClipped = false;

        public static void RestrictCursorToWindow(Window window)
        {
            if (_cursorClipped)
            {
                return;
            }
            
            var windowBounds = new Win32Rect
            {
                Left = (int)window.Left,
                Top = (int)window.Top,
                Right = (int)(window.Left + window.Width),
                Bottom = (int)(window.Top + window.Height)
            };

            ClipCursor(ref windowBounds);
            _cursorClipped = true;
        }

        public static void FreeCursor()
        {
            if (!_cursorClipped)
            {
                return;
            }
            
            // Have to pass null to free cursor
            ClipCursor(IntPtr.Zero);

            _cursorClipped = false;
        }
        
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}
