﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyPosition : BaseTwinItem, ITwinPosition
    {
        public Vector4 Position { get; private set; }
        
        public PS2AnyPosition()
        {
            Position = new Vector4();
        }

        public override int GetLength()
        {
            return Constants.SIZE_VECTOR4;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Position.Read(reader, Constants.SIZE_VECTOR4);
        }

        public override void Write(BinaryWriter writer)
        {
            Position.Write(writer);
        }

        public override String GetName()
        {
            return $"Position {id:X}";
        }
    }
}
