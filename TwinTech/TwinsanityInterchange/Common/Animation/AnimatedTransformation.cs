using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.Animation
{
    public class AnimatedTransformation : ITwinSerializable
    {
        List<Int16> transformValues;

        public Int32 Count { get => transformValues.Count; }

        public AnimatedTransformation(UInt16 amount)
        {
            transformValues = new List<Int16>(amount);
        }

        public Int16 this[int key]
        {
            get { return transformValues[key]; }
            set { transformValues[key] = value; }
        }

        public float GetFloat(int key)
        {
            return this[key] / 4096f;
        }

        public void SetFloat(int key, float value)
        {
            this[key] = (Int16)(value * 4096);
        }

        public Int32 GetLength()
        {
            return transformValues.Count * 2;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            for (Int32 i = 0; i < transformValues.Capacity; i++)
            {
                transformValues.Add(reader.ReadInt16());
            }
        }

        public void Write(BinaryWriter writer)
        {
            foreach (var offset in transformValues)
            {
                writer.Write(offset);
            }
        }
    }
}
