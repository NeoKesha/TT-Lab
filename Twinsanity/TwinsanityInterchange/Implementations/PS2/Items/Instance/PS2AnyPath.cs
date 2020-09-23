using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Instance
{
    public class PS2AnyPath : ITwinCamera
    {
        UInt32 id;
        public List<Vector4> PointList { get; private set; }
        public List<Vector2> ParameterList { get; private set; }
        public PS2AnyPath()
        {
            PointList = new List<Vector4>();
            ParameterList = new List<Vector2>();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 8 + Constants.SIZE_VECTOR4 * PointList.Count + Constants.SIZE_VECTOR2 * ParameterList.Count;
        }

        public void Read(BinaryReader reader, int length)
        {
            Int32 points = reader.ReadInt32();
            PointList.Clear();
            for (int i = 0; i < points; ++i)
            {
                Vector4 point = new Vector4();
                point.Read(reader, Constants.SIZE_VECTOR4);
                PointList.Add(point);
            }
            Int32 parameters = reader.ReadInt32();
            ParameterList.Clear();
            for (int i = 0; i < parameters; ++i)
            {
                Vector2 param = new Vector2();
                param.Read(reader, Constants.SIZE_VECTOR2);
                ParameterList.Add(param);
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(PointList.Count);
            foreach(ITwinSerializable e in PointList)
            {
                e.Write(writer);
            }
            writer.Write(ParameterList.Count);
            foreach (ITwinSerializable e in ParameterList)
            {
                e.Write(writer);
            }
        }
    }
}
