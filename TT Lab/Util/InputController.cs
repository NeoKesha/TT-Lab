using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TT_Lab.Util
{
    public class InputController
    {
        public bool Shift { get => leftShift | rightShift; }
        public bool Ctrl { get => leftCtrl | rightCtrl; }
        public bool Alt { get => leftAlt | rightAlt; }
        public void HandleInputs(List<Key> keysPressed)
        {
            //Update control keys
            leftShift = false;
            rightShift = false;
            leftCtrl = false;
            rightCtrl = false;
            leftAlt = false;
            rightAlt = false;
            foreach (var key in keysPressed)
            {
                if (key == Key.LeftAlt) leftAlt = true;
                if (key == Key.RightAlt) rightAlt = true;
                if (key == Key.LeftCtrl) leftCtrl = true;
                if (key == Key.RightCtrl) rightCtrl = true;
                if (key == Key.LeftShift) leftShift = true;
                if (key == Key.RightShift) rightShift = true;
            }
        }
        public void HandleKeyUp(Key key)
        {
            if (key == Key.LeftAlt) leftAlt = false;
            if (key == Key.RightAlt) rightAlt = false;
            if (key == Key.LeftCtrl) leftCtrl = false;
            if (key == Key.RightCtrl) rightCtrl = false;
            if (key == Key.LeftShift) leftShift = false;
            if (key == Key.RightShift) rightShift = false;
        }
        public void HandleKeyDown(Key key)
        {
            if (key == Key.LeftAlt) leftAlt = true;
            if (key == Key.RightAlt) rightAlt = true;
            if (key == Key.LeftCtrl) leftCtrl = true;
            if (key == Key.RightCtrl) rightCtrl = true;
            if (key == Key.LeftShift) leftShift = true;
            if (key == Key.RightShift) rightShift = true;
        }
        private bool leftShift = false;
        private bool rightShift = false;
        private bool leftCtrl = false;
        private bool rightCtrl = false;
        private bool leftAlt = false;
        private bool rightAlt = false;
    }
}
