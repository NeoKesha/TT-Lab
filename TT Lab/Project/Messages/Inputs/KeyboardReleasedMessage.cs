﻿using System.Windows.Input;

namespace TT_Lab.Project.Messages.Inputs
{
    public class KeyboardReleasedMessage
    {
        public KeyboardReleasedMessage(KeyEventArgs keyEvent)
        {
            Key = keyEvent;
        }

        public KeyEventArgs Key { get; set; }
    }
}
