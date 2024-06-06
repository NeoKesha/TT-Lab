using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TT_Lab.Project.Messages.Inputs;

namespace TT_Lab.Util
{
    public class InputController
    {
        public bool Shift { get => leftShift | rightShift; }
        public bool Ctrl { get => leftCtrl | rightCtrl; }
        public bool Alt { get => leftAlt | rightAlt; }

        public InputController(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public bool IsKeyPressed(Key key)
        {
            return pressedKeys.Contains(key);
        }

        private void HandleKeyUp(Object sender, KeyEventArgs e)
        {
            var key = e.Key;
            if (pressedKeys.Contains(key))
            {
                KeyReleased(key);
                eventAggregator.PublishOnUIThreadAsync(new KeyboardPressedMessage(e));
                pressedKeys.Remove(key);
            }
        }

        private void HandleKeyDown(Object sender, KeyEventArgs e)
        {
            var key = e.Key;
            if (!pressedKeys.Contains(key))
            {
                KeyPressed(key);
                eventAggregator.PublishOnUIThreadAsync(new KeyboardReleasedMessage(e));
                pressedKeys.Add(key);
            }
        }

        private void KeyReleased(Key key)
        {
            if (key == Key.LeftAlt) leftAlt = false;
            if (key == Key.RightAlt) rightAlt = false;
            if (key == Key.LeftCtrl) leftCtrl = false;
            if (key == Key.RightCtrl) rightCtrl = false;
            if (key == Key.LeftShift) leftShift = false;
            if (key == Key.RightShift) rightShift = false;
        }

        private void KeyPressed(Key key)
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

        private readonly List<Key> pressedKeys = new();
        private readonly IEventAggregator eventAggregator;
    }
}
