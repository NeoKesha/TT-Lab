using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class SubSkin : ITwinSerializable
    {
        public UInt32 Material;
        private Int32 BlobSize;
        private Int32 VertexAmount;
        private Byte[] VifCode;
        public List<List<Vector4>> Data;

        public int GetLength()
        {
            return 12 + VifCode.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            Material = reader.ReadUInt32();
            BlobSize = reader.ReadInt32();
            VertexAmount = reader.ReadInt32();
            VifCode = reader.ReadBytes(BlobSize);
            
        }
        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VifCode);
            Data = interpreter.GetMem();
        }
        public void Write(BinaryWriter writer)
        {
            writer.Write(Material);
            writer.Write(BlobSize);
            writer.Write(VertexAmount);
            writer.Write(VifCode);
        }
    }
}
